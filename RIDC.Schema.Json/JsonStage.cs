using System.Text.Json.Serialization;

namespace RIDC.Schema.Json;

public record JsonStage
{
    [JsonPropertyName("slProgress")] public int StoryLineProgress { get; set; }
    [JsonPropertyName("hilightMark")] public bool HighLightMark { get; set; }
}
