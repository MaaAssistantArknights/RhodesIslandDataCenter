using System.Text.Json.Serialization;
using RIDC.Shared.Attributes;

namespace RIDC.Schema.Json;

public record JsonZone : Zone
{
    [JsonPropertyName("zoneID")] [IgnoreCompare] public new string ZoneId { get; set; }
}
