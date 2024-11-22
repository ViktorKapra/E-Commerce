using AutoMapper;
using ECom.API.Filters;
using ECom.API.Mapper;
using ECom.Configuration.Extenstions;
using ECom.Configuration.JSONformater;
using ECom.Configuration.Middleware;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;

namespace ECom.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
              .MinimumLevel.Information()
              .WriteTo.File(".\\Logs\\log_all.txt", rollingInterval: RollingInterval.Day)
              .WriteTo.Logger(l => l
                  .Filter.ByIncludingOnly(x => x.Level == Serilog.Events.LogEventLevel.Information)
                  .WriteTo.File(".\\Logs\\log_info.txt"))
              .WriteTo.Logger(l => l
                  .Filter.ByIncludingOnly(x => x.Level == Serilog.Events.LogEventLevel.Warning)
                  .WriteTo.File(".\\Logs\\log_warn.txt", rollingInterval: RollingInterval.Day))
              .WriteTo.Logger(l => l
                  .Filter.ByIncludingOnly(x => x.Level == Serilog.Events.LogEventLevel.Fatal)
                  .WriteTo.File(".\\Logs\\log_fatal.txt", rollingInterval: RollingInterval.Day))
              .WriteTo.Logger(l => l
                  .Filter.ByIncludingOnly(x => x.Level == Serilog.Events.LogEventLevel.Fatal)
                  .WriteTo.File(".\\Logs\\log_fatal.txt", rollingInterval: RollingInterval.Day))
              .WriteTo.TestCorrelator()
              .CreateLogger();

            services.AddSerilog();
            var connectionString = Configuration["ConnectionStrings:DefaultConnection"] ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDataConfigurations(connectionString);

            services.AddIdentityConfigurations();

            services.AddServerLogic(Configuration);

            services.AddScoped<ValidationProductFilterAttribute>();

            services.AddScoped<ValidationNonNegativeInteger>();

            services.AddSwaggerGen(c =>
            {
                {
                    c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "My API - V1",
                        Version = "v1"
                    });

                    c.IncludeXmlComments(Assembly.GetExecutingAssembly());
                }
            });

            services.AddSingleton(new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            }).CreateMapper());

            services.AddControllers(options =>
            {
                options.InputFormatters.Insert(0, JSONFormater.GetJsonPatchInputFormatter());
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionHandler>();
            if (env.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                //app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

            }
            app.UseHttpsRedirection();
            app.UseHealthChecks("/api/health", new HealthCheckOptions
            { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            //Configures endpoint matching to rely on attribute routing
            app.UseEndpoints(endpoints =>
            {
                { endpoints.MapDefaultControllerRoute(); }
            });
        }
    }
}
