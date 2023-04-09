using IPAustralia.Models;

namespace IPAustralia.ServiceAbstractions
{
    public interface ISearchService
    {
        Task<SearchResultDto> Search(IFilterable filter, CancellationToken ct);
    }
}
