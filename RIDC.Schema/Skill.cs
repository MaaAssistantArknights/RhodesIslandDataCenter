using System.Text.Json.Serialization;

namespace RIDC.Schema;

public record Skill
{
    [JsonPropertyName("skillId")] public string SkillId { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; }
}
