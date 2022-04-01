using System.Text.Json.Serialization;

namespace RIDC.Schema.Json;

public record JsonCharacter : Character
{
    [JsonPropertyName("nationId")] public string NationId { get; set; }
    [JsonPropertyName("skills")] public new ICollection<string> Skills { get; set; }
}
