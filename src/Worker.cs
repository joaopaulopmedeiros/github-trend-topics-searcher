using Application.Services;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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

        /// <summary>
        /// Get content from text file.
        /// Search for github trend topics related to term found on file.
        /// Send e-mail with result.
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Log.Information($"Worker running at: {DateTimeOffset.Now}");

                try
                {
                    var specification = await _getSpecificationFileService.RunAsync();

                    var topics = await _searchService.RunAsync(specification.SearchTerm);

                    if (topics.Any())
                    {
                        _emailService.Send(
                            specification.Recipients,
                            EmailUtil.GetDefaultEmailSubject(),
                            EmailUtil.GetEmailBodyFromTopics(specification.SearchTerm, topics)
                         );
                    }
                    else
                    {
                        _emailService.Send(
                            specification.Recipients,
                            EmailUtil.GetDefaultEmailSubject(),
                            EmailUtil.GetTermNotFoundMessage(specification.SearchTerm)
                       );
                    }
                }
                catch (Exception ex)
                {
                    Log.Error($"Unexpected error: {ex}");
                }

                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
