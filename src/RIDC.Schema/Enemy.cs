using System.Text.Json.Serialization;

namespace RIDC.Schema;

public record Enemy
{
    [JsonPropertyName("enemyId")] public string EnemyId { get; set; }
    [JsonPropertyName("enemyIndex")] public string EnemyIndex { get; set; }
    [JsonPropertyName("enemyTags")] public ICollection<string> EnemyTags { get; set; }
    [JsonPropertyName("sortId")] public int SortId { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("enemyRace")] public string EnemyRace { get; set; }
    [JsonPropertyName("enemyLevel")] public string EnemyLevel { get; set; }
    [JsonPropertyName("description")] public string Description { get; set; }
    [JsonPropertyName("attackType")] public string AttackType { get; set; }
    [JsonPropertyName("endure")] public string Endure { get; set; }
    [JsonPropertyName("attack")] public string Attack { get; set; }
    [JsonPropertyName("defence")] public string Defence { get; set; }
    [JsonPropertyName("resistance")] public string Resistance { get; set; }
    [JsonPropertyName("ability")] public string Ability { get; set; }
    [JsonPropertyName("isInvalidKilled")] public string IsInvalidKilled { get; set; }
    [JsonPropertyName("hideInHandbook")] public string HideInHandbook { get; set; }
}
