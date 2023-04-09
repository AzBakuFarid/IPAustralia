using AutoMapper;
using IPAustralia.Domain;
using IPAustralia.Models;
using IPAustralia.Responses;

namespace IPAustralia.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Trademark, TrademarkSearchResponseDto>();
            CreateMap<SearchResultDto, SearchResponseDto>();
        }
    }
}
