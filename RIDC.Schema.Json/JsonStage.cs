using System.Text.Json.Serialization;

namespace RIDC.Schema.Json;

public record JsonStage : Stage
{
    [JsonPropertyName("slProgress")] public new int StoryLineProgress { get; set; }
    [JsonPropertyName("hilightMark")] public new bool HighLightMark { get; set; }
}
