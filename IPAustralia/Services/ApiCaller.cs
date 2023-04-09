using IPAustralia.Models;
using Newtonsoft.Json;
using IPAustralia.Extensions;
using IPAustralia.ServiceAbstractions;

namespace IPAustralia.Services
{
    public class ApiCaller : IApiCaller
    {
        private readonly HttpClient _httpClient;

        public ApiCaller(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiCallResult<TResponse>> Get<TResponse>(string url, CancellationToken ct = default) where TResponse : class
        {

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Get,
            };

            var response = await _httpClient.SendAsync(request, ct);
            var responseAsString = await response.Content.ReadAsStringAsync(ct);

            return new ApiCallResult<TResponse>(
                response.IsSuccessStatusCode,
                response.IsSuccessStatusCode && !responseAsString.IsMissing() ? JsonConvert.DeserializeObject<TResponse>(responseAsString) : default,
                response.IsSuccessStatusCode ? default : responseAsString);
        }

        public async Task<ApiCallResult<string>> GetHtml(string url, CancellationToken ct = default)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Get,
            };

            var response = await _httpClient.SendAsync(request, ct);
            var responseAsString = await response.Content.ReadAsStringAsync(ct);
            return new ApiCallResult<string>(
                response.IsSuccessStatusCode,
                response.IsSuccessStatusCode ? responseAsString : default,
                response.IsSuccessStatusCode ? default : responseAsString);
        }

        public async Task<ApiCallResult<string>> PostFormAndReturnHtml(string url, Dictionary<string, string> formData, CancellationToken ct)
        {
            using var content = new FormUrlEncodedContent(formData);

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Post,
                Content = content,
            };

            var response = await _httpClient.SendAsync(request, ct);
            var responseAsString = await response.Content.ReadAsStringAsync(ct);
            return new ApiCallResult<string>(
                response.IsSuccessStatusCode,
                response.IsSuccessStatusCode ? responseAsString : default,
                response.IsSuccessStatusCode ? default : responseAsString);
        }
    }
}
