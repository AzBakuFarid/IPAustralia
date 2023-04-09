using Newtonsoft.Json;

namespace IPAustralia.Models
{
    public class AustraliaSearchCountResponseDto
    {
        [JsonProperty("count")] public int Count { get; set; }
        [JsonProperty("errors")] public string Errors { get; set; }
    }
}
