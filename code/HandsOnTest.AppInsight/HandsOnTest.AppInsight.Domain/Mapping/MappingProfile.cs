using AutoMapper;
using HandsOnTest.ETracker.CsvParser.Model;
using HandsOnTest.AppInsight.Domain.DTO;

namespace HandsOnTest.AppInsight.Domain.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DealerTrackText, DealerTrackerDTO>().
                ForMember(destination => destination.Price, source => source.MapFrom(x => FormatPrice(x.Price)));
        }

        private string FormatPrice(string price)
        {
            return $"CAD${price}";
        }
    }
}
