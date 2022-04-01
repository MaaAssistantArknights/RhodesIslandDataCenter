using System.Text.Json.Serialization;
using RIDC.Shared.Attributes;

namespace RIDC.Schema;

public record Skill
{
    [JsonPropertyName("skillId")] public string SkillId { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonIgnore] [IgnoreCompare] public ICollection<Character> Characters { get; set; }
}
