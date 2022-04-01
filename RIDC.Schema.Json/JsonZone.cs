using System.Text.Json.Serialization;

namespace RIDC.Schema.Json;

public record JsonZone : Zone
{
    [JsonPropertyName("zoneID")] public new string ZoneId { get; set; }
}
