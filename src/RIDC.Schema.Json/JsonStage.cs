using System.Text.Json.Serialization;
using RIDC.Shared.Attributes;

namespace RIDC.Schema.Json;

public record JsonStage : Stage
{
    [JsonPropertyName("slProgress")] [IgnoreCompare] public new int StoryLineProgress { get; set; }
    [JsonPropertyName("hilightMark")] [IgnoreCompare] public new bool HighLightMark { get; set; }
}
