using System.IO;
using System.Threading.Tasks;

namespace Infra.Data.Repositories
{
    public class FileRepository
    {
        private readonly string file = "C:\\Users\\indra\\Desktop\\search.txt";

        public async Task<string> GetContentAsync()
        {
            string content = null;

            using (var sr = new StreamReader(file))
            {
                content = await sr.ReadToEndAsync();
            }

            return content;
        }
    }
}
