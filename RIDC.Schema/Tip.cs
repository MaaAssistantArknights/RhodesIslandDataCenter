using System.Text.Json.Serialization;

namespace RIDC.Schema;

public record Tip
{
    [JsonPropertyName("tip")] public string TipContent { get; set; }
    [JsonPropertyName("category")] public string Category { get; set; }
    [JsonPropertyName("weight")] public float Weight { get; set; }
}
