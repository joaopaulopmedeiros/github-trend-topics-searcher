﻿using Application.Dtos;
using Infra.Data.Repositories;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            return new SpecificationFileDto() {
                SearchTerms = columns[0].Split(',').ToList(),
                Recipients = columns[1].Split(',').ToList(),
            };
        }
    }
}
