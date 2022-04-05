using System.Text.Json.Serialization;
using RIDC.Schema.Json.Embed;
using RIDC.Shared.Attributes;

namespace RIDC.Schema.Json;

public record JsonSkin : Skin
{
    [JsonPropertyName("displaySkin")] [IgnoreCompare] public JsonSkinDisplaySkin DisplaySkin { get; set; }
}
