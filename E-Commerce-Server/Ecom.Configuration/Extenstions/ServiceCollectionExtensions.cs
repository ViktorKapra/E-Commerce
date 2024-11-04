using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.ComponentModel.DataAnnotations;

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
    }
}
