using System.Text.Json;
using System.Text.Json.Serialization;
using RIDC.Shared.Attributes;

namespace RIDC.Schema.Json;

public record JsonCharacter : Character
{
    [JsonPropertyName("nationId")] [IgnoreCompare] public string NationId { get; set; }
    [JsonPropertyName("skills")] [IgnoreCompare] public new ICollection<JsonElement> Skills { get; set; }
}
