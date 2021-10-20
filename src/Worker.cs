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
        public Worker
        (
            GetSpecificationFileService getSpecificationFileService
        )
        {
            _getSpecificationFileService = getSpecificationFileService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Log.Information($"Worker running at: {DateTimeOffset.Now}");

                var specification = await _getSpecificationFileService.RunAsync();

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
