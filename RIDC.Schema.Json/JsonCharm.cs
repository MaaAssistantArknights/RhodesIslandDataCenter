using System.Text.Json.Serialization;
using RIDC.Shared.Attributes;

namespace RIDC.Schema.Json;

public record JsonCharm : Charm
{
    [JsonPropertyName("id")] [IgnoreCompare] public new string CharmId { get; set; }
}
