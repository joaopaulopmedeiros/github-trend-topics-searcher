using Application.Dtos;
using Infra.Data.Repositories;
using Serilog;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class GetSpecificationFileService
    {
        private readonly FileRepository _repository;

        public GetSpecificationFileService(FileRepository repository)
        {
            _repository = repository;
        }

        public async Task<SpecificationFileDto> RunAsync()
        {
            string content = await _repository.GetContentAsync();

            Log.Information($">>> Content: {content}");

            string[] columns = content.Split(';');

            return MapFromFileToObject(columns);
        }

        private SpecificationFileDto MapFromFileToObject(string[] columns)
        {
            return new SpecificationFileDto()
            {
                SearchTerm = columns[0],
                Recipients = columns[1].Split(',').ToList(),
            };
        }
    }
}
