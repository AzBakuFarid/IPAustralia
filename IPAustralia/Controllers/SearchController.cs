using AutoMapper;
using IPAustralia.Requests;
using IPAustralia.Responses;
using IPAustralia.ServiceAbstractions;
using Microsoft.AspNetCore.Mvc;

namespace IPAustralia.Controllers
{
    [ApiController]
    [Route("api/v1/search")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService searchService;
        private readonly IMapper _mapper;

        public SearchController(ISearchService searchService, IMapper mapper)
        {
            this.searchService = searchService;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] TrademarkSearchRequestDto request, CancellationToken ct) => 
            Ok(_mapper.Map<SearchResponseDto>(await searchService.Search(request, ct)));
        
    }
}