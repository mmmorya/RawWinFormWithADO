

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RawWinFormWithADO.App.Extensions.Startup;

namespace RawWinFormWithADO.App
{
    internal static class Program
    {
        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)          // Base configuration for the Host
                .ConfigureAppConfiguration()         // Load appsettings.json
                .ConfigureServices();                // Register services (Dependency Injection)

        [STAThread]
        static void Main()
        {
            var host = CreateHostBuilder(null).Build();

            // Start the Main Form using DI
            ApplicationConfiguration.Initialize();
            var mainForm = host.Services.GetRequiredService<Form1>();
            Application.Run(mainForm);
        }
    }
}