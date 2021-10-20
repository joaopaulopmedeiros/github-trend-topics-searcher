using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Application
{
    public class Startup
    {
        public static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                    .SetBasePath(hostContext.HostingEnvironment.ContentRootPath)
                    .AddJsonFile("appsettings.json", false, true)
                    .AddEnvironmentVariables()
                    .Build();

            services.AddSingleton(configuration);
            services.AddHostedService<Worker>();
        }
    }
}