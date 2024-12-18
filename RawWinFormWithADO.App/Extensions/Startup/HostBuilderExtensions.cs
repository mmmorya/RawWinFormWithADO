using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RawWinFormWithADO.BusinessLogic.Contracts;
using RawWinFormWithADO.BusinessLogic.Services;
using RawWinFormWithADO.DataAccess.Contracts;
using RawWinFormWithADO.DataAccess.DataAccessLayer;

namespace RawWinFormWithADO.App.Extensions.Startup
{
    public static class HostBuilderExtensions
    {
        /// <summary>
        /// Configures application configuration to load appsettings.json and environment variables.
        /// </summary>
        public static IHostBuilder ConfigureAppConfiguration(this IHostBuilder builder)
        {
            return builder.ConfigureAppConfiguration((context, config) =>
            {
                config.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                      .AddEnvironmentVariables(); // Optional: Load environment variables
            });
        }

        /// <summary>
        /// Configures services and registers them in the DI container.
        /// </summary>
        public static IHostBuilder ConfigureServices(this IHostBuilder builder)
        {
            return builder.ConfigureServices((context, services) =>
            {
                // Bind AppSettings section from appsettings.json to a strongly-typed class
                var appSettingsSection = context.Configuration.GetSection("AppSettings");
                //services.Configure<AppSettings>(appSettingsSection);

                // Register other services
                services.AddScoped<IDAL, DAL>();       // Example DAL registration
                services.AddScoped<ISampleBusinessService, SampleBusinessService>();
                services.AddTransient<Form1>();    // Register the main form
            });
        }
    }
}
