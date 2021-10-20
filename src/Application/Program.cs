using Application.Configurations;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog(AppLoggerExtensions.ConfigureSerilogLoggers)
                .UseWindowsService()
                .ConfigureServices((hostContext, services) => { Startup.ConfigureServices(hostContext, services); });
    }
}