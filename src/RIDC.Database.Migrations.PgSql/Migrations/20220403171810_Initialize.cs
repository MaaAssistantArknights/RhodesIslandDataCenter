using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RIDC.Database.Migrations.PgSql.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Charms",
                columns: table => new
                {
                    CharmId = table.Column<string>(type: "text", nullable: false),
                    Sort = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Icon = table.Column<string>(type: "text", nullable: true),
                    ItemUsage = table.Column<string>(type: "text", nullable: true),
                    ItemDesc = table.Column<string>(type: "text", nullable: true),
                    ItemObtainApproach = table.Column<string>(type: "text", nullable: true),
                    Rarity = table.Column<int>(type: "integer", nullable: false),
                    Desc = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    SpecialObtainApproach = table.Column<string>(type: "text", nullable: true),
                    CharmType = table.Column<string>(type: "text", nullable: true),
                    ObtainInRandom = table.Column<bool>(type: "boolean", nullable: false),
                    CharmEffect = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Charms", x => x.CharmId);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemId = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Rarity = table.Column<int>(type: "integer", nullable: false),
                    IconId = table.Column<string>(type: "text", nullable: true),
                    StackIconId = table.Column<string>(type: "text", nullable: true),
                    SortId = table.Column<int>(type: "integer", nullable: false),
                    Usage = table.Column<string>(type: "text", nullable: true),
                    ObtainApproach = table.Column<string>(type: "text", nullable: true),
                    ClassifyType = table.Column<string>(type: "text", nullable: true),
                    ItemType = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemId);
                });

            migrationBuilder.CreateTable(
                name: "Miscellaneous",
                columns: table => new
                {
                    Key = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Miscellaneous", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Powers",
                columns: table => new
                {
                    PowerId = table.Column<string>(type: "text", nullable: false),
                    OrderNum = table.Column<int>(type: "integer", nullable: false),
                    PowerLevel = table.Column<int>(type: "integer", nullable: false),
                    PowerName = table.Column<string>(type: "text", nullable: true),
                    PowerCode = table.Column<string>(type: "text", nullable: true),
                    Color = table.Column<string>(type: "text", nullable: true),
                    IsLimited = table.Column<bool>(type: "boolean", nullable: false),
                    IsRaw = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Powers", x => x.PowerId);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    SkillId = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.SkillId);
                });

            migrationBuilder.CreateTable(
                name: "Tips",
                columns: table => new
                {
                    TipId = table.Column<string>(type: "text", nullable: false),
                    TipContent = table.Column<string>(type: "text", nullable: true),
                    Category = table.Column<string>(type: "text", nullable: true),
                    Weight = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tips", x => x.TipId);
                });

            migrationBuilder.CreateTable(
                name: "Zones",
                columns: table => new
                {
                    ZoneId = table.Column<string>(type: "text", nullable: false),
                    ZoneIndex = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    ZoneNameFirst = table.Column<string>(type: "text", nullable: true),
                    ZoneNameSecond = table.Column<string>(type: "text", nullable: true),
                    ZoneNameThird = table.Column<string>(type: "text", nullable: true),
                    ZoneNameTitleCurrent = table.Column<string>(type: "text", nullable: true),
                    ZoneNameTitleUnCurrent = table.Column<string>(type: "text", nullable: true),
                    ZoneNameTitleEx = table.Column<string>(type: "text", nullable: true),
                    LockedText = table.Column<string>(type: "text", nullable: true),
                    CanPreview = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zones", x => x.ZoneId);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    CharacterId = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CanUseGeneralPotentialItem = table.Column<bool>(type: "boolean", nullable: false),
                    PotentialItemId = table.Column<string>(type: "text", nullable: true),
                    NationPowerId = table.Column<string>(type: "text", nullable: true),
                    GroupId = table.Column<string>(type: "text", nullable: true),
                    TeamId = table.Column<string>(type: "text", nullable: true),
                    DisplayNumber = table.Column<string>(type: "text", nullable: true),
                    TokenKey = table.Column<string>(type: "text", nullable: true),
                    Appellation = table.Column<string>(type: "text", nullable: true),
                    Position = table.Column<string>(type: "text", nullable: true),
                    TagList = table.Column<string>(type: "text", nullable: true),
                    ItemUsage = table.Column<string>(type: "text", nullable: true),
                    ItemDesc = table.Column<string>(type: "text", nullable: true),
                    ItemObtainApproach = table.Column<string>(type: "text", nullable: true),
                    IsNotObtainable = table.Column<bool>(type: "boolean", nullable: false),
                    IsSpChar = table.Column<bool>(type: "boolean", nullable: false),
                    MaxPotentialLevel = table.Column<int>(type: "integer", nullable: false),
                    Rarity = table.Column<int>(type: "integer", nullable: false),
                    Profession = table.Column<string>(type: "text", nullable: true),
                    SubProfessionId = table.Column<string>(type: "text", nullable: true)
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
                    StageId = table.Column<string>(type: "text", nullable: false),
                    LevelId = table.Column<string>(type: "text", nullable: true),
                    ZoneId = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    StageType = table.Column<string>(type: "text", nullable: true),
                    Difficulty = table.Column<string>(type: "text", nullable: true),
                    PerformanceStageFlag = table.Column<string>(type: "text", nullable: true),
                    HardStagedId = table.Column<string>(type: "text", nullable: true),
                    DangerLevel = table.Column<string>(type: "text", nullable: true),
                    DangerPoint = table.Column<float>(type: "real", nullable: false),
                    CanPractice = table.Column<bool>(type: "boolean", nullable: false),
                    CanBattleReplay = table.Column<bool>(type: "boolean", nullable: false),
                    ApCost = table.Column<int>(type: "integer", nullable: false),
                    ApFailReturn = table.Column<int>(type: "integer", nullable: false),
                    EtItemId = table.Column<string>(type: "text", nullable: true),
                    EtCost = table.Column<int>(type: "integer", nullable: false),
                    EtFailReturn = table.Column<int>(type: "integer", nullable: false),
                    PracticeTicketCost = table.Column<int>(type: "integer", nullable: false),
                    DailyStageDifficulty = table.Column<int>(type: "integer", nullable: false),
                    ExpGain = table.Column<int>(type: "integer", nullable: false),
                    GoldGain = table.Column<int>(type: "integer", nullable: false),
                    LoseExpGain = table.Column<int>(type: "integer", nullable: false),
                    LoseGoldGain = table.Column<int>(type: "integer", nullable: false),
                    PassFavor = table.Column<int>(type: "integer", nullable: false),
                    CompleteFavor = table.Column<int>(type: "integer", nullable: false),
                    StoryLineProgress = table.Column<int>(type: "integer", nullable: false),
                    HighLightMark = table.Column<bool>(type: "boolean", nullable: false),
                    BossMark = table.Column<bool>(type: "boolean", nullable: false),
                    IsPredefined = table.Column<bool>(type: "boolean", nullable: false),
                    IsHardPredefined = table.Column<bool>(type: "boolean", nullable: false),
                    IsSkillSelectablePredefined = table.Column<bool>(type: "boolean", nullable: false),
                    IsStoryOnly = table.Column<bool>(type: "boolean", nullable: false)
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
                    CharactersCharacterId = table.Column<string>(type: "text", nullable: false),
                    SkillsSkillId = table.Column<string>(type: "text", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Characters_NationPowerId",
                table: "Characters",
                column: "NationPowerId");

            migrationBuilder.CreateIndex(
                name: "IX_relation_CharacterSkill_SkillsSkillId",
                table: "relation_CharacterSkill",
                column: "SkillsSkillId");

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
                name: "Items");

            migrationBuilder.DropTable(
                name: "Miscellaneous");

            migrationBuilder.DropTable(
                name: "relation_CharacterSkill");

            migrationBuilder.DropTable(
                name: "Stages");

            migrationBuilder.DropTable(
                name: "Tips");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Zones");

            migrationBuilder.DropTable(
                name: "Powers");
        }
    }
}
