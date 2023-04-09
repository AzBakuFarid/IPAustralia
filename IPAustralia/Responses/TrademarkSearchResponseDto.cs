using System.Text.Json.Serialization;

namespace IPAustralia.Responses
{
    public class TrademarkSearchResponseDto
    {
        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("logo_url")]
        public string LogoUrl { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("classes")]
        public string Classes { get; set; }

        [JsonPropertyName("status1")]
        public string Status1 { get; set; }

        [JsonPropertyName("status2")]
        public string Status2 { get; set; }

        [JsonPropertyName("details_page_url")]
        public string DetailsUrl { get; set; }
    }
}
