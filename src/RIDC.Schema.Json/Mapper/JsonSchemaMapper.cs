using System.Security.Cryptography;
using System.Text;

namespace RIDC.Schema.Json.Mapper;

public static class JsonSchemaMapper
{
    public static Character ToCharacter(this JsonCharacter jsonCharacter, string characterId,
        IEnumerable<Power> nations, IEnumerable<Skill> skills, IEnumerable<Skin> skins)
    {
        var character = jsonCharacter as Character;
        character.CharacterId = characterId;
        character.Nation = nations.FirstOrDefault(x => x.PowerId == jsonCharacter.NationId);
        character.Skills = skills.Where(x =>
            jsonCharacter.Skills.Any(y =>
                x.SkillId == y.GetProperty("skillId").GetString()))
            .ToList();
        character.Skins = skins.Where(x => x.CharacterId == characterId).ToList();
        return character;
    }

    public static Charm ToCharm(this JsonCharm jsonCharm)
    {
        var charm = jsonCharm as Charm;
        charm.CharmId = jsonCharm.CharmId;
        return charm;
    }

    public static Item ToItem(this JsonItem jsonItem)
    {
        var item = jsonItem as Item;
        return item;
    }

    public static Power ToPower(this JsonPower jsonPower)
    {
        var power = jsonPower as Power;
        return power;
    }

    public static Skill ToSkill(this JsonSkill jsonSkill)
    {
        var skill = jsonSkill as Skill;
        if (jsonSkill.Levels.Count == 0)
        {
            skill.Name = "";
            return skill;
        }
        var level = jsonSkill.Levels.FirstOrDefault();
        skill.Name = level.GetProperty("name").GetString();
        return skill;
    }

    public static Stage ToStage(this JsonStage jsonStage)
    {
        var stage = jsonStage as Stage;
        stage.StoryLineProgress = jsonStage.StoryLineProgress;
        stage.HighLightMark = jsonStage.HighLightMark;
        return stage;
    }

    public static Tip ToTip(this JsonTip jsonTip)
    {
        var tip = jsonTip as Tip;
        var tipHashBytes = Encoding.UTF8.GetBytes($"{tip.Category}|{tip.Weight.ToString("F2")}|{tip.TipContent}");
        tip.TipId = BitConverter.ToString(SHA1.HashData(tipHashBytes)).Replace("-", "").ToLower();
        return tip;
    }

    public static Zone ToZone(this JsonZone jsonZone, IEnumerable<Stage> stages)
    {
        var zone = jsonZone as Zone;
        zone.ZoneId = jsonZone.ZoneId;
        zone.Stages = stages.Where(x => x.ZoneId == zone.ZoneId).ToList();
        return zone;
    }

    public static Enemy ToEnemy(this JsonEnemy jsonEnemy)
    {
        var enemy = jsonEnemy as Enemy;
        return enemy;
    }

    public static Skin ToSkin(this JsonSkin jsonSkin)
    {
        var skin = jsonSkin as Skin;
        skin.SkinName = jsonSkin.DisplaySkin.SkinName;
        skin.ModelName = jsonSkin.DisplaySkin.ModelName;
        skin.DrawerName = jsonSkin.DisplaySkin.DrawerName;
        skin.SkinGroupId = jsonSkin.DisplaySkin.SkinGroupId;
        skin.SkinGroupName = jsonSkin.DisplaySkin.SkinGroupName;
        skin.SkinGroupSortIndex = jsonSkin.DisplaySkin.SkinGroupSortIndex;
        skin.Content = jsonSkin.DisplaySkin.Content;
        skin.Dialog = jsonSkin.DisplaySkin.Dialog;
        skin.Usage = jsonSkin.DisplaySkin.Usage;
        skin.Description = jsonSkin.DisplaySkin.Description;
        skin.ObtainApproach = jsonSkin.DisplaySkin.ObtainApproach;
        skin.SortId = jsonSkin.DisplaySkin.SortId;
        return skin;
    }
}
