using System.Text.Json.Serialization;

namespace RIDC.Schema;

public record Character
{
    [JsonPropertyName("characterId")] public string CharacterId { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("description")] public string Description { get; set; }
    [JsonPropertyName("canUseGeneralPotentialItem")] public bool CanUseGeneralPotentialItem { get; set; }
    [JsonPropertyName("potentialItemId")] public string PotentialItemId { get; set; }
    [JsonPropertyName("nation")] public Power Nation { get; set; }
    [JsonPropertyName("groupId")] public string GroupId { get; set; }
    [JsonPropertyName("teamId")] public string TeamId { get; set; }
    [JsonPropertyName("displayNumber")] public string DisplayNumber { get; set; }
    [JsonPropertyName("tokenKey")] public string TokenKey { get; set; }
    [JsonPropertyName("appellation")] public string Appellation { get; set; }
    [JsonPropertyName("position")] public string Position { get; set; }
    [JsonPropertyName("tagList")] public ICollection<string> TagList { get; set; }
    [JsonPropertyName("itemUsage")] public string ItemUsage { get; set; }
    [JsonPropertyName("itemDesc")] public string ItemDesc { get; set; }
    [JsonPropertyName("itemObtainApproach")] public string ItemObtainApproach { get; set; }
    [JsonPropertyName("isNotObtainable")] public bool IsNotObtainable { get; set; }
    [JsonPropertyName("isSpChar")] public bool IsSpChar { get; set; }
    [JsonPropertyName("maxPotentialLevel")] public int MaxPotentialLevel { get; set; }
    [JsonPropertyName("rarity")] public int Rarity { get; set; }
    [JsonPropertyName("profession")] public string Profession { get; set; }
    [JsonPropertyName("subProfessionId")] public string SubProfessionId { get; set; }
    [JsonPropertyName("skills")] public ICollection<Skill> Skills { get; set; }
    [JsonPropertyName("skins")] public ICollection<Skin> Skins { get; set; }
}
