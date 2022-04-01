using System.Text.Json;
using System.Text.Json.Serialization;
using RIDC.Shared.Attributes;

namespace RIDC.Schema.Json;

public record JsonSkill : Skill
{
    [JsonPropertyName("levels")] [IgnoreCompare] public ICollection<JsonElement> Levels { get; set; }
}
