using System.Text.Json.Serialization;

namespace IPAustralia.Models
{
    public class AustraliaSearchCountResponseDto
    {
        [JsonPropertyName("count")] public int Count { get; set; }
        [JsonPropertyName("errors")] public string Errors { get; set; }
    }
}
