using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SearchService
    {
        public async Task<HttpResponseMessage> RunAsync(List<string> Keywords)
        {
            var client = new HttpClient();

            return await client.SendAsync(CreateRequestMessage(), CreateCancellationToken());
        }

        private HttpRequestMessage CreateRequestMessage()
        {
            return new HttpRequestMessage();
        }
        
        private CancellationToken CreateCancellationToken()
        {
            return new CancellationToken();
        }
    }
}
