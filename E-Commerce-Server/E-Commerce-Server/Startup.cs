﻿using AutoMapper;
using ECom.API.Mapper;
using ECom.Data;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

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
              .CreateLogger();

            services.AddSerilog();

            var connectionString = Configuration["ConnectionStrings:DefaultConnection"] ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

           
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddHealthChecks().AddSqlServer(connectionString);

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddSwaggerGen();

            services.AddSingleton(new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            }).CreateMapper());

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

            }

            app.UseHttpsRedirection();

            app.UseHealthChecks("/health", new HealthCheckOptions
            { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            
            //Configures endpoint matching to rely on attribute routing
            app.UseEndpoints(endpoints =>
            {
                { endpoints.MapControllers(); }
            });


        }
    }
}
