using System.Text.Json.Serialization;

namespace RIDC.Schema;

public record Item
{
    [JsonPropertyName("itemId")] public string ItemId { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("description")] public string Description { get; set; }
    [JsonPropertyName("rarity")] public string Rarity { get; set; }
    [JsonPropertyName("iconId")] public string IconId { get; set; }
    [JsonPropertyName("stackIconId")] public string StackIconId { get; set; }
    [JsonPropertyName("sortId")] public int SortId { get; set; }
    [JsonPropertyName("usage")] public string Usage { get; set; }
    [JsonPropertyName("obtainApproach")] public string ObtainApproach { get; set; }
    [JsonPropertyName("classifyType")] public string ClassifyType { get; set; }
    [JsonPropertyName("itemType")] public string ItemType { get; set; }
}
