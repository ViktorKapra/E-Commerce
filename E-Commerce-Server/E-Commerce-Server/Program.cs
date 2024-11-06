using ECom.Configuration.Seeding;
using Microsoft.AspNetCore;
using Serilog;

namespace ECom.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration() // this loger configuration is created to cover the case
                .WriteTo.Console().CreateLogger();// when ConfigureService() throw exception beffore actual
                                                  // definition of the loger
            try
            {
                Log.Information("Application is staring up");
                var host = CreateWebHostBuilder(args).Build();
                Seeder.Seed(host);

                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "The application failed to start correctly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
