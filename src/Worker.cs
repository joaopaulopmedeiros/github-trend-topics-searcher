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
        private readonly SearchService _searchService;
        private readonly EmailService _emailService;
        public Worker
        (
            GetSpecificationFileService getSpecificationFileService,
            SearchService searchService,
            EmailService emailService
        )
        {
            _getSpecificationFileService = getSpecificationFileService;
            _searchService = searchService;
            _emailService = emailService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Log.Information($"Worker running at: {DateTimeOffset.Now}");

                try
                {
                    var specification = await _getSpecificationFileService.RunAsync();

                    var response = await _searchService.RunAsync(specification.SearchTerms);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        _emailService.Send(specification.Recipients, "New search delivered", content);
                    }
                } catch (Exception ex)
                {
                    Log.Error($"Unexpected error: {ex}");
                }

                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
