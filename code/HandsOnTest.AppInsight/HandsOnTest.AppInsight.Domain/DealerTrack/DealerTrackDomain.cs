using AutoMapper;
using HandsOnTest.ETracker.CsvParser;
using HandsOnTest.ETracker.CsvParser.Repository;
using HandsOnTest.AppInsight.Domain.DTO;
using HandsOnTest.AppInsight.Domain.Validators;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandsOnTest.AppInsight.Domain.DealerTrack
{
    public class DealerTrackDomain : IDealerTrackDomain
    {
        private readonly ICsvFileValidator _csvFileValidator;
        private readonly IDealerTrackCsvReaderRepository _dealerTrackCsvReaderRepository;
        private readonly IMapper _mapper;

        public DealerTrackDomain(ICsvFileValidator csvFileValidator, IDealerTrackCsvReaderRepository dealerTrackCsvReaderRepository, IMapper mapper)
        {
            _csvFileValidator               = csvFileValidator;
            _dealerTrackCsvReaderRepository = dealerTrackCsvReaderRepository;
            _mapper                         = mapper;
        }

        private async Task<string> ReadAsString(IFormFile file)
        {
            var result = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                //TODO: Fix Ansi encoding
                return await reader.ReadToEndAsync();
            }
        }

        public async Task<IEnumerable<DealerTrackerDTO>> ProcessFile(IFormFile file)
        {
            var fileContent = await ReadAsString(file);
            var lista = await _dealerTrackCsvReaderRepository.ProcessFile(fileContent);

            return _mapper.Map<IEnumerable<DealerTrackerDTO>>(lista);
        }
    }
}
