namespace IPAustralia.Responses
{
    public class SearchResponseDto
    {
        public int Total { get; set; }
        public IEnumerable<TrademarkSearchResponseDto> Items { get; set; }
        public SearchResponseDto()
        {
            Items = new List<TrademarkSearchResponseDto>();
        }
    }
}
