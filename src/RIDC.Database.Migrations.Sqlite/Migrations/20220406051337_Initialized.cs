using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RIDC.Database.Migrations.Sqlite.Migrations
{
    public partial class Initialized : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Charms",
                columns: table => new
                {
                    CharmId = table.Column<string>(type: "TEXT", nullable: false),
                    Sort = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Icon = table.Column<string>(type: "TEXT", nullable: true),
                    ItemUsage = table.Column<string>(type: "TEXT", nullable: true),
                    ItemDesc = table.Column<string>(type: "TEXT", nullable: true),
                    ItemObtainApproach = table.Column<string>(type: "TEXT", nullable: true),
                    Rarity = table.Column<int>(type: "INTEGER", nullable: false),
                    Desc = table.Column<string>(type: "TEXT", nullable: true),
                    Price = table.Column<int>(type: "INTEGER", nullable: false),
                    SpecialObtainApproach = table.Column<string>(type: "TEXT", nullable: true),
                    CharmType = table.Column<string>(type: "TEXT", nullable: true),
                    ObtainInRandom = table.Column<bool>(type: "INTEGER", nullable: false),
                    CharmEffect = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Charms", x => x.CharmId);
                });

            migrationBuilder.CreateTable(
                name: "Enemies",
                columns: table => new
                {
                    EnemyId = table.Column<string>(type: "TEXT", nullable: false),
                    EnemyIndex = table.Column<string>(type: "TEXT", nullable: true),
                    EnemyTags = table.Column<string>(type: "TEXT", nullable: true),
                    SortId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    EnemyRace = table.Column<string>(type: "TEXT", nullable: true),
                    EnemyLevel = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    AttackType = table.Column<string>(type: "TEXT", nullable: true),
                    Endure = table.Column<string>(type: "TEXT", nullable: true),
                    Attack = table.Column<string>(type: "TEXT", nullable: true),
                    Defence = table.Column<string>(type: "TEXT", nullable: true),
                    Resistance = table.Column<string>(type: "TEXT", nullable: true),
                    Ability = table.Column<string>(type: "TEXT", nullable: true),
                    IsInvalidKilled = table.Column<bool>(type: "INTEGER", nullable: false),
                    HideInHandbook = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enemies", x => x.EnemyId);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemId = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Rarity = table.Column<int>(type: "INTEGER", nullable: false),
                    IconId = table.Column<string>(type: "TEXT", nullable: true),
                    StackIconId = table.Column<string>(type: "TEXT", nullable: true),
                    SortId = table.Column<int>(type: "INTEGER", nullable: false),
                    Usage = table.Column<string>(type: "TEXT", nullable: true),
                    ObtainApproach = table.Column<string>(type: "TEXT", nullable: true),
                    ClassifyType = table.Column<string>(type: "TEXT", nullable: true),
                    ItemType = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemId);
                });

            migrationBuilder.CreateTable(
                name: "Miscellaneous",
                columns: table => new
                {
                    Key = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Miscellaneous", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Powers",
                columns: table => new
                {
                    PowerId = table.Column<string>(type: "TEXT", nullable: false),
                    OrderNum = table.Column<int>(type: "INTEGER", nullable: false),
                    PowerLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    PowerName = table.Column<string>(type: "TEXT", nullable: true),
                    PowerCode = table.Column<string>(type: "TEXT", nullable: true),
                    Color = table.Column<string>(type: "TEXT", nullable: true),
                    IsLimited = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsRaw = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Powers", x => x.PowerId);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    SkillId = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.SkillId);
                });

            migrationBuilder.CreateTable(
                name: "Tips",
                columns: table => new
                {
                    TipId = table.Column<string>(type: "TEXT", nullable: false),
                    TipContent = table.Column<string>(type: "TEXT", nullable: true),
                    Category = table.Column<string>(type: "TEXT", nullable: true),
                    Weight = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tips", x => x.TipId);
                });

            migrationBuilder.CreateTable(
                name: "Zones",
                columns: table => new
                {
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    ZoneIndex = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    ZoneNameFirst = table.Column<string>(type: "TEXT", nullable: true),
                    ZoneNameSecond = table.Column<string>(type: "TEXT", nullable: true),
                    ZoneNameThird = table.Column<string>(type: "TEXT", nullable: true),
                    ZoneNameTitleCurrent = table.Column<string>(type: "TEXT", nullable: true),
                    ZoneNameTitleUnCurrent = table.Column<string>(type: "TEXT", nullable: true),
                    ZoneNameTitleEx = table.Column<string>(type: "TEXT", nullable: true),
                    LockedText = table.Column<string>(type: "TEXT", nullable: true),
                    CanPreview = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zones", x => x.ZoneId);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    CharacterId = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    CanUseGeneralPotentialItem = table.Column<bool>(type: "INTEGER", nullable: false),
                    PotentialItemId = table.Column<string>(type: "TEXT", nullable: true),
                    NationPowerId = table.Column<string>(type: "TEXT", nullable: true),
                    GroupId = table.Column<string>(type: "TEXT", nullable: true),
                    TeamId = table.Column<string>(type: "TEXT", nullable: true),
                    DisplayNumber = table.Column<string>(type: "TEXT", nullable: true),
                    TokenKey = table.Column<string>(type: "TEXT", nullable: true),
                    Appellation = table.Column<string>(type: "TEXT", nullable: true),
                    Position = table.Column<string>(type: "TEXT", nullable: true),
                    TagList = table.Column<string>(type: "TEXT", nullable: true),
                    ItemUsage = table.Column<string>(type: "TEXT", nullable: true),
                    ItemDesc = table.Column<string>(type: "TEXT", nullable: true),
                    ItemObtainApproach = table.Column<string>(type: "TEXT", nullable: true),
                    IsNotObtainable = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsSpChar = table.Column<bool>(type: "INTEGER", nullable: false),
                    MaxPotentialLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    Rarity = table.Column<int>(type: "INTEGER", nullable: false),
                    Profession = table.Column<string>(type: "TEXT", nullable: true),
                    SubProfessionId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.CharacterId);
                    table.ForeignKey(
                        name: "FK_Characters_Powers_NationPowerId",
                        column: x => x.NationPowerId,
                        principalTable: "Powers",
                        principalColumn: "PowerId");
                });

            migrationBuilder.CreateTable(
                name: "Stages",
                columns: table => new
                {
                    StageId = table.Column<string>(type: "TEXT", nullable: false),
                    LevelId = table.Column<string>(type: "TEXT", nullable: true),
                    ZoneId = table.Column<string>(type: "TEXT", nullable: true),
                    Code = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    StageType = table.Column<string>(type: "TEXT", nullable: true),
                    Difficulty = table.Column<string>(type: "TEXT", nullable: true),
                    PerformanceStageFlag = table.Column<string>(type: "TEXT", nullable: true),
                    HardStagedId = table.Column<string>(type: "TEXT", nullable: true),
                    DangerLevel = table.Column<string>(type: "TEXT", nullable: true),
                    DangerPoint = table.Column<float>(type: "REAL", nullable: false),
                    CanPractice = table.Column<bool>(type: "INTEGER", nullable: false),
                    CanBattleReplay = table.Column<bool>(type: "INTEGER", nullable: false),
                    ApCost = table.Column<int>(type: "INTEGER", nullable: false),
                    ApFailReturn = table.Column<int>(type: "INTEGER", nullable: false),
                    EtItemId = table.Column<string>(type: "TEXT", nullable: true),
                    EtCost = table.Column<int>(type: "INTEGER", nullable: false),
                    EtFailReturn = table.Column<int>(type: "INTEGER", nullable: false),
                    PracticeTicketCost = table.Column<int>(type: "INTEGER", nullable: false),
                    DailyStageDifficulty = table.Column<int>(type: "INTEGER", nullable: false),
                    ExpGain = table.Column<int>(type: "INTEGER", nullable: false),
                    GoldGain = table.Column<int>(type: "INTEGER", nullable: false),
                    LoseExpGain = table.Column<int>(type: "INTEGER", nullable: false),
                    LoseGoldGain = table.Column<int>(type: "INTEGER", nullable: false),
                    PassFavor = table.Column<int>(type: "INTEGER", nullable: false),
                    CompleteFavor = table.Column<int>(type: "INTEGER", nullable: false),
                    StoryLineProgress = table.Column<int>(type: "INTEGER", nullable: false),
                    HighLightMark = table.Column<bool>(type: "INTEGER", nullable: false),
                    BossMark = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsPredefined = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsHardPredefined = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsSkillSelectablePredefined = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsStoryOnly = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stages", x => x.StageId);
                    table.ForeignKey(
                        name: "FK_Stages_Zones_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zones",
                        principalColumn: "ZoneId");
                });

            migrationBuilder.CreateTable(
                name: "relation_CharacterSkill",
                columns: table => new
                {
                    CharactersCharacterId = table.Column<string>(type: "TEXT", nullable: false),
                    SkillsSkillId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_relation_CharacterSkill", x => new { x.CharactersCharacterId, x.SkillsSkillId });
                    table.ForeignKey(
                        name: "FK_relation_CharacterSkill_Characters_CharactersCharacterId",
                        column: x => x.CharactersCharacterId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_relation_CharacterSkill_Skills_SkillsSkillId",
                        column: x => x.SkillsSkillId,
                        principalTable: "Skills",
                        principalColumn: "SkillId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skins",
                columns: table => new
                {
                    SkinId = table.Column<string>(type: "TEXT", nullable: false),
                    CharacterId = table.Column<string>(type: "TEXT", nullable: true),
                    AvatarId = table.Column<string>(type: "TEXT", nullable: true),
                    PortraitId = table.Column<string>(type: "TEXT", nullable: true),
                    DynamicPortraitId = table.Column<string>(type: "TEXT", nullable: true),
                    IllustId = table.Column<string>(type: "TEXT", nullable: true),
                    DynamicIllustId = table.Column<string>(type: "TEXT", nullable: true),
                    SkinName = table.Column<string>(type: "TEXT", nullable: true),
                    ModelName = table.Column<string>(type: "TEXT", nullable: true),
                    DrawerName = table.Column<string>(type: "TEXT", nullable: true),
                    SkinGroupId = table.Column<string>(type: "TEXT", nullable: true),
                    SkinGroupName = table.Column<string>(type: "TEXT", nullable: true),
                    SkinGroupSortIndex = table.Column<int>(type: "INTEGER", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: true),
                    Dialog = table.Column<string>(type: "TEXT", nullable: true),
                    Usage = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    ObtainApproach = table.Column<string>(type: "TEXT", nullable: true),
                    SortId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skins", x => x.SkinId);
                    table.ForeignKey(
                        name: "FK_Skins_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characters_NationPowerId",
                table: "Characters",
                column: "NationPowerId");

            migrationBuilder.CreateIndex(
                name: "IX_relation_CharacterSkill_SkillsSkillId",
                table: "relation_CharacterSkill",
                column: "SkillsSkillId");

            migrationBuilder.CreateIndex(
                name: "IX_Skins_CharacterId",
                table: "Skins",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_Stages_ZoneId",
                table: "Stages",
                column: "ZoneId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Charms");

            migrationBuilder.DropTable(
                name: "Enemies");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Miscellaneous");

            migrationBuilder.DropTable(
                name: "relation_CharacterSkill");

            migrationBuilder.DropTable(
                name: "Skins");

            migrationBuilder.DropTable(
                name: "Stages");

            migrationBuilder.DropTable(
                name: "Tips");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Zones");

            migrationBuilder.DropTable(
                name: "Powers");
        }
    }
}
