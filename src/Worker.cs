using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Application
{
    public class Worker : BackgroundService
    {
        private readonly GetSpecificationFileService _getSpecificationFileService;
        private readonly EmailService _emailService;
        public Worker
        (
            GetSpecificationFileService getSpecificationFileService,
            EmailService emailService
        )
        {
            _getSpecificationFileService = getSpecificationFileService;
            _emailService = emailService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Log.Information($"Worker running at: {DateTimeOffset.Now}");

                var specification = await _getSpecificationFileService.RunAsync();

                var content = "mock content";

                _emailService.Send(specification.Recipients, "New search delivered", content);

                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
