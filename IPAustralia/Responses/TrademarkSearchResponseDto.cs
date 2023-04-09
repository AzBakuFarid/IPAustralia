using Newtonsoft.Json;

namespace IPAustralia.Responses
{
    public class TrademarkSearchResponseDto
    {
        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("logo_url")]
        public string LogoUrl { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("classes")]
        public string Classes { get; set; }

        [JsonProperty("status1")]
        public string Status1 { get; set; }

        [JsonProperty("status2")]
        public string Status2 { get; set; }

        [JsonProperty("details_page_url")]
        public string DetailsUrl { get; set; }
    }
}
