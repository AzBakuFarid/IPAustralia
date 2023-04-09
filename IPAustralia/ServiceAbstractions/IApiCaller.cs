using IPAustralia.Models;

namespace IPAustralia.ServiceAbstractions
{
    public interface IApiCaller
    {
        Task<ApiCallResult<string>> GetHtml(string url, CancellationToken ct = default);
        Task<ApiCallResult<TResponse>> Get<TResponse>(string url, CancellationToken ct = default) where TResponse : class;
        Task<ApiCallResult<string>> PostFormAndReturnHtml(string url, Dictionary<string, string> formData, CancellationToken ct);
    }
}
