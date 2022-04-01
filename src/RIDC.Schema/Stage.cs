using System.Text.Json.Serialization;

namespace RIDC.Schema;

public record Stage
{
    [JsonPropertyName("stageId")] public string StageId { get; set; }
    [JsonPropertyName("levelId")] public string LevelId { get; set; }
    [JsonPropertyName("zoneId")] public string ZoneId { get; set; }
    [JsonPropertyName("code")] public string Code { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("description")] public string Description { get; set; }
    [JsonPropertyName("stageType")] public string StageType { get; set; }
    [JsonPropertyName("difficulty")] public string Difficulty { get; set; }
    [JsonPropertyName("performanceStageFlag")] public string PerformanceStageFlag { get; set; }
    [JsonPropertyName("hardStagedId")] public string HardStagedId { get; set; }
    [JsonPropertyName("dangerLevel")] public string DangerLevel { get; set; }
    [JsonPropertyName("dangerPoint")] public float DangerPoint { get; set; }
    [JsonPropertyName("canPractice")] public bool CanPractice { get; set; }
    [JsonPropertyName("canBattleReplay")] public bool CanBattleReplay { get; set; }
    [JsonPropertyName("apCost")] public int ApCost { get; set; }
    [JsonPropertyName("apFailReturn")] public int ApFailReturn { get; set; }
    [JsonPropertyName("etItemId")] public string EtItemId { get; set; }
    [JsonPropertyName("etCost")] public int EtCost { get; set; }
    [JsonPropertyName("etFailReturn")] public int EtFailReturn { get; set; }
    [JsonPropertyName("practiceTicketCost")] public int PracticeTicketCost { get; set; }
    [JsonPropertyName("dailyStageDifficulty")] public int DailyStageDifficulty { get; set; }
    [JsonPropertyName("expGain")] public int ExpGain { get; set; }
    [JsonPropertyName("goldGain")] public int GoldGain { get; set; }
    [JsonPropertyName("loseExpGain")] public int LoseExpGain { get; set; }
    [JsonPropertyName("loseGoldGain")] public int LoseGoldGain { get; set; }
    [JsonPropertyName("passFavor")] public int PassFavor { get; set; }
    [JsonPropertyName("completeFavor")] public int CompleteFavor { get; set; }
    [JsonPropertyName("storyLineProgress")] public int StoryLineProgress { get; set; }
    [JsonPropertyName("highLightMark")] public bool HighLightMark { get; set; }
    [JsonPropertyName("bossMark")] public bool BossMark { get; set; }
    [JsonPropertyName("isPredefined")] public bool IsPredefined { get; set; }
    [JsonPropertyName("isHardPredefined")] public bool IsHardPredefined { get; set; }
    [JsonPropertyName("isSkillSelectablePredefined")] public bool IsSkillSelectablePredefined { get; set; }
    [JsonPropertyName("isStoryOnly")] public bool IsStoryOnly { get; set; }
}
