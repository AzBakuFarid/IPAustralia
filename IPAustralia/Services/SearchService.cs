using IPAustralia.Domain;
using IPAustralia.Models;
using IPAustralia.ServiceAbstractions;
using Microsoft.Extensions.Options;
using IPAustralia.Exceptions;

namespace IPAustralia.Services
{
    public class SearchService : ISearchService
    {
        private readonly IApiCaller _apiCaller;
        private readonly Config _config;
        private readonly IScrapper _scrapper;
        private string _csrf;
        private string _searchId;


        private List<Trademark> _foundItems = new ();

        public static object _lockObject = new object();

        public SearchService(IApiCaller apiCaller,
            IOptions<Config> configOptions,
            IScrapper scrapper)
        {
            _apiCaller = apiCaller;
            _config = configOptions.Value;
            _scrapper = scrapper;
        }

        public async Task<SearchResultDto> Search(IFilterable filter, CancellationToken ct)
        {
            var result = new SearchResultDto();
            var countUrl = string.Format(this._config.CountFullAddress, filter.TrademarkName);
            var countRequestResult = await _apiCaller.Get<AustraliaSearchCountResponseDto>(countUrl, ct);
            int totalCount = countRequestResult.Response.Count;
            if (totalCount == 0) return result;

            var firstPageHtml = await PerformInitialSearch(filter.TrademarkName, ct);
            var scrapResults = _scrapper.ScrapForTrademarks(new TrademarkScrapData(firstPageHtml, _config.Domain));
            _foundItems.AddRange(scrapResults);

            var firstPageSize = _foundItems.Count;
            if (firstPageSize == 0) return result;

            var remainingTotalPages = Math.Ceiling((double)(totalCount - firstPageSize) / (double)firstPageSize);
            
            var tasks = new Task[3];
            for (int i = 0; i < tasks.Length; i++)
            {
                var index = i;
                tasks[i] = Task.Run(async () =>
                {
                    int pageNumber = 1 + index;
                    while (pageNumber <= remainingTotalPages)
                    {
                        await PerformSearch(this._searchId, pageNumber, ct);
                        pageNumber += tasks.Length;
                    };

                }, ct);
            }
            Task.WaitAll(tasks, ct);

            if (ct.IsCancellationRequested) return result;

            return new SearchResultDto { Total = totalCount, Items = _foundItems };
        }

        private async Task PerformSearch(string searchId, int pageNumber, CancellationToken ct)
        {
            var searchUrl = string.Format(this._config.SearchFullAddress, searchId, pageNumber);
            var searchPageHtmlRequestResult = await _apiCaller.GetHtml(searchUrl, ct);

            if(!searchPageHtmlRequestResult.Succeeded) return;

            var scrapResults = _scrapper.ScrapForTrademarks(new TrademarkScrapData(searchPageHtmlRequestResult.Response, _config.Domain));
            if (ct.IsCancellationRequested) return;

            lock (_lockObject)
            {
                _foundItems.AddRange(scrapResults);
            }
        }

        private async Task<string> PerformInitialSearch(string keyword, CancellationToken ct)
        {
            var formData = new Dictionary<string, string>()
            {
                { "_csrf", await GetCsrf(ct) },
                { "wv[0]", keyword },
            };
            
            var firstPageHtml = await _apiCaller.PostFormAndReturnHtml(_config.SearchPostFormFullAddress, formData, ct);
            this._searchId = _scrapper.FindSearchId(firstPageHtml.Response);

            if (this._searchId is null) throw new SearchCustomException("Error. searchId is null, further reqeusts are impossible");

            return firstPageHtml.Response;
        }

        private async Task<string> GetCsrf(CancellationToken ct)
        {
            if (this._csrf is null)
            {
                var requestForCsrf = await _apiCaller.GetHtml(_config.AdvancedSearchFullAddress, ct);
                this._csrf = _scrapper.FindCsrf(requestForCsrf.Response);
            }
            if (this._csrf is null) throw new SearchCustomException("Error. Could not fetch csrf token, further reqeusts are impossible");
            return this._csrf;
        }
    }
}
