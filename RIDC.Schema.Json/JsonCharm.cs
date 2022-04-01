using System.Text.Json.Serialization;

namespace RIDC.Schema.Json;

public record JsonCharm : Charm
{
    [JsonPropertyName("id")] public new string CharmId { get; set; }
}
