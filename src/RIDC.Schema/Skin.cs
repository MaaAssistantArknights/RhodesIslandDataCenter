using System.Text.Json.Serialization;

namespace RIDC.Schema;

public record Skin
{
    [JsonPropertyName("skinId")] public string SkinId { get; set; }
    [JsonPropertyName("charId")] public string CharacterId { get; set; }
    [JsonPropertyName("avatarId")] public string AvatarId { get; set; }
    [JsonPropertyName("portraitId")] public string PortraitId { get; set; }
    [JsonPropertyName("dynPortraitId")] public string DynamicPortraitId { get; set; }
    [JsonPropertyName("illustId")] public string IllustId { get; set; }
    [JsonPropertyName("dynIllustId")] public string DynamicIllustId { get; set; }
    [JsonPropertyName("skinName")] public string SkinName { get; set; }
    [JsonPropertyName("modelName")] public string ModelName { get; set; }
    [JsonPropertyName("drawerName")] public string DrawerName { get; set; }
    [JsonPropertyName("skinGroupId")] public string SkinGroupId { get; set; }
    [JsonPropertyName("skinGroupName")] public string SkinGroupName { get; set; }
    [JsonPropertyName("skinGroupSortIndex")] public int SkinGroupSortIndex { get; set; }
    [JsonPropertyName("content")] public string Content { get; set; }
    [JsonPropertyName("dialog")] public string Dialog { get; set; }
    [JsonPropertyName("usage")] public string Usage { get; set; }
    [JsonPropertyName("description")] public string Description { get; set; }
    [JsonPropertyName("obtainApproach")] public string ObtainApproach { get; set; }
    [JsonPropertyName("sortId")] public int SortId { get; set; }
}
