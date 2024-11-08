using ECom.BLogic.Services.Authentication;
using ECom.BLogic.Services.EmailService;
using ECom.BLogic.Services.EmailService.Settings;
using ECom.BLogic.Services.Interfaces;
using ECom.BLogic.Services.Profile;
using ECom.Data;
using ECom.Data.Account;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace ECom.Configuration.Extenstions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureAndValidate<T>(this IServiceCollection @this,
        IConfiguration config) where T : class
        => @this
            .Configure<T>(config.GetSection(typeof(T).Name))
            .PostConfigure<T>(settings =>
            {
                var configErrors = new List<ValidationResult>();
                Validator.TryValidateObject(settings, new ValidationContext(settings), configErrors);
                if (configErrors.Any())
                {
                    var aggrErrors = string.Join(",", configErrors);
                    var count = configErrors.Count;
                    var configType = typeof(T).Name;
                    Log.Error($"Failed validation for {nameof(T)} object");
                    throw new ApplicationException(
                        $"Found {count} configuration error(s) in {configType}: {aggrErrors}");
                }
            });
        public static IServiceCollection AddServerLogic(this IServiceCollection services, IConfiguration config)
        {
            services.ConfigureAndValidate<SmtpServerSettings>(config);
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddTransient<SmtpClient>();
            return services;
        }

        public static IServiceCollection AddDataConfigurations(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddHealthChecks().AddSqlServer(connectionString);
            return services;
        }

        public static IServiceCollection AddIdentityConfigurations(this IServiceCollection services)
        {
            services.AddDefaultIdentity<EComUser>(options => options.SignIn.RequireConfirmedAccount = true)
               .AddRoles<EComRole>()
           .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
            })
            .AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                options.LoginPath = "/api/auth/signIn";
                options.Cookie.HttpOnly = true;
                options.SlidingExpiration = true;
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });


            return services;
        }


    }
}
