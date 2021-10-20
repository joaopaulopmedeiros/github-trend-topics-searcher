using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;

namespace Application.Configurations
{
    public static class AppLoggerExtensions
    {
        public static void ConfigureSerilogLoggers(HostBuilderContext context, LoggerConfiguration configuration)
        {
            configuration
                    .MinimumLevel.Information()
                    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
                    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                    .ReadFrom.Configuration(context.Configuration)
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .WriteTo.File($"logs//{DateTime.Now.ToString("yyyyMMdd")}__log.txt");
        }
    }
}
