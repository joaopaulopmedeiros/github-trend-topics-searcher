using AngleSharp;
using Application.Dtos;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SearchService
    {
        private IBrowsingContext context { get; set; }
        private string container = "article.border.rounded.color-shadow-small.color-bg-subtle.my-4";
        private string anchor = "h3.f3.color-fg-muted.text-normal.lh-condensed";

        public SearchService()
        {
            var config = Configuration.Default.WithDefaultLoader();
            context = BrowsingContext.New(config);
        }

        /// <summary>
        /// Scrap trend topics on github related to a given term
        /// </summary>
        /// <param name="term">Search Term</param>
        /// <returns></returns>
        public async Task<IEnumerable<TopicDto>> RunAsync(string term)
        {
            var url = $"https://github.com/topics/{term}";

            Log.Information($">>> Searching form term {term} on {url}");

            var document = await context.OpenAsync(url);

            var html = document.QuerySelectorAll(container);

            var results = new List<TopicDto>();

            foreach (var div in html)
            {
                var item = div.QuerySelector(anchor).LastElementChild;

                var title = Sanitize(item);

                if (!string.IsNullOrEmpty(title))
                {
                    results.Add(new TopicDto { Title = title });
                }
            }

            return results;
        }

        private string Sanitize(AngleSharp.Dom.IElement item)
        {
            return item?.InnerHtml.Trim().Replace("\n", "");
        }
    }
}