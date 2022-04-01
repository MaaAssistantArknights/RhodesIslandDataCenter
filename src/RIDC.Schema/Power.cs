using System.Text.Json.Serialization;

namespace RIDC.Schema;

public record Power
{
    [JsonPropertyName("powerId")] public string PowerId { get; set; }
    [JsonPropertyName("orderNum")] public int OrderNum { get; set; }
    [JsonPropertyName("powerLevel")] public int PowerLevel { get; set; }
    [JsonPropertyName("powerName")] public string PowerName { get; set; }
    [JsonPropertyName("powerCode")] public string PowerCode { get; set; }
    [JsonPropertyName("color")] public string Color { get; set; }
    [JsonPropertyName("isLimited")] public bool IsLimited { get; set; }
    [JsonPropertyName("isRaw")] public bool IsRaw { get; set; }
}
