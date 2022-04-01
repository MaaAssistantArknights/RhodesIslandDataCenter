using System.Text.Json.Serialization;

namespace RIDC.Schema;

public record Zone
{
    [JsonPropertyName("zoneId")] public string ZoneId { get; set; }
    [JsonPropertyName("zoneIndex")] public int ZoneIndex { get; set; }
    [JsonPropertyName("type")] public string Type { get; set; }
    [JsonPropertyName("zoneNameFirst")] public string ZoneNameFirst { get; set; }
    [JsonPropertyName("zoneNameSecond")] public string ZoneNameSecond { get; set; }
    [JsonPropertyName("zoneNameThird")] public string ZoneNameThird { get; set; }
    [JsonPropertyName("zoneNameTitleCurrent")] public string ZoneNameTitleCurrent { get; set; }
    [JsonPropertyName("zoneNameTitleUnCurrent")] public string ZoneNameTitleUnCurrent { get; set; }
    [JsonPropertyName("zoneNameTitleEx")] public string ZoneNameTitleEx { get; set; }
    [JsonPropertyName("lockedText")] public string LockedText { get; set; }
    [JsonPropertyName("canPreview")] public bool CanPreview { get; set; }
    [JsonPropertyName("stages")] public ICollection<Stage> Stages { get; set; }
}
