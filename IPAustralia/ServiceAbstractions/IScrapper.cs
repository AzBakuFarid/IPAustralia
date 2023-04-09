using IPAustralia.Domain;
using IPAustralia.Models;

namespace IPAustralia.ServiceAbstractions
{
    public interface IScrapper
    {
        List<Trademark> ScrapForTrademarks(TrademarkScrapData data);
        string FindCsrf(string html);
        string FindSearchId(string html);
    }
}
