using System.Text.Json;
using System.Text.Json.Serialization;

namespace RIDC.Schema.Json;

public record JsonSkill : Skill
{
    [JsonPropertyName("levels")] public ICollection<JsonElement> Levels { get; set; }
}
