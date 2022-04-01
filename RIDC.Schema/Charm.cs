using System.Text.Json.Serialization;

namespace RIDC.Schema;

public record Charm
{
    [JsonPropertyName("charmId")] public string CharmId { get; set; }
    [JsonPropertyName("sort")] public string Sort { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("icon")] public string Icon { get; set; }
    [JsonPropertyName("itemUsage")] public string ItemUsage { get; set; }
    [JsonPropertyName("itemDesc")] public string ItemDesc { get; set; }
    [JsonPropertyName("itemObtainApproach")] public string ItemObtainApproach { get; set; }
    [JsonPropertyName("rarity")] public int Rarity { get; set; }
    [JsonPropertyName("desc")] public string Desc { get; set; }
    [JsonPropertyName("price")] public int Price { get; set; }
    [JsonPropertyName("specialObtainApproach")] public string SpecialObtainApproach { get; set; }
    [JsonPropertyName("charmType")] public string CharmType { get; set; }
    [JsonPropertyName("obtainInRandom")] public bool ObtainInRandom { get; set; }
    [JsonPropertyName("charmEffect")] public string CharmEffect { get; set; }
}
