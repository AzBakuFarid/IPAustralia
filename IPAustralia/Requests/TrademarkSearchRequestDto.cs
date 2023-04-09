using IPAustralia.Models;

namespace IPAustralia.Requests
{
    public class TrademarkSearchRequestDto : IFilterable
    {
        public string TrademarkName { get; set; }
    }
}
