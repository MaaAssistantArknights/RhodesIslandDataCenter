// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RIDC.Database;

#nullable disable

namespace RIDC.Database.Migrations.Sqlite.Migrations
{
    [DbContext(typeof(RhodesIslandDbContext))]
    [Migration("20220406051337_Initialized")]
    partial class Initialized
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.3");

            modelBuilder.Entity("CharacterSkill", b =>
                {
                    b.Property<string>("CharactersCharacterId")
                        .HasColumnType("TEXT");

                    b.Property<string>("SkillsSkillId")
                        .HasColumnType("TEXT");

                    b.HasKey("CharactersCharacterId", "SkillsSkillId");

                    b.HasIndex("SkillsSkillId");

                    b.ToTable("relation_CharacterSkill", (string)null);
                });

            modelBuilder.Entity("RIDC.Schema.Character", b =>
                {
                    b.Property<string>("CharacterId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Appellation")
                        .HasColumnType("TEXT");

                    b.Property<bool>("CanUseGeneralPotentialItem")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("DisplayNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("GroupId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsNotObtainable")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsSpChar")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ItemDesc")
                        .HasColumnType("TEXT");

                    b.Property<string>("ItemObtainApproach")
                        .HasColumnType("TEXT");

                    b.Property<string>("ItemUsage")
                        .HasColumnType("TEXT");

                    b.Property<int>("MaxPotentialLevel")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("NationPowerId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Position")
                        .HasColumnType("TEXT");

                    b.Property<string>("PotentialItemId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Profession")
                        .HasColumnType("TEXT");

                    b.Property<int>("Rarity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SubProfessionId")
                        .HasColumnType("TEXT");

                    b.Property<string>("TagList")
                        .HasColumnType("TEXT");

                    b.Property<string>("TeamId")
                        .HasColumnType("TEXT");

                    b.Property<string>("TokenKey")
                        .HasColumnType("TEXT");

                    b.HasKey("CharacterId");

                    b.HasIndex("NationPowerId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("RIDC.Schema.Charm", b =>
                {
                    b.Property<string>("CharmId")
                        .HasColumnType("TEXT");

                    b.Property<string>("CharmEffect")
                        .HasColumnType("TEXT");

                    b.Property<string>("CharmType")
                        .HasColumnType("TEXT");

                    b.Property<string>("Desc")
                        .HasColumnType("TEXT");

                    b.Property<string>("Icon")
                        .HasColumnType("TEXT");

                    b.Property<string>("ItemDesc")
                        .HasColumnType("TEXT");

                    b.Property<string>("ItemObtainApproach")
                        .HasColumnType("TEXT");

                    b.Property<string>("ItemUsage")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<bool>("ObtainInRandom")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Price")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Rarity")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Sort")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SpecialObtainApproach")
                        .HasColumnType("TEXT");

                    b.HasKey("CharmId");

                    b.ToTable("Charms");
                });

            modelBuilder.Entity("RIDC.Schema.Enemy", b =>
                {
                    b.Property<string>("EnemyId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Ability")
                        .HasColumnType("TEXT");

                    b.Property<string>("Attack")
                        .HasColumnType("TEXT");

                    b.Property<string>("AttackType")
                        .HasColumnType("TEXT");

                    b.Property<string>("Defence")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Endure")
                        .HasColumnType("TEXT");

                    b.Property<string>("EnemyIndex")
                        .HasColumnType("TEXT");

                    b.Property<string>("EnemyLevel")
                        .HasColumnType("TEXT");

                    b.Property<string>("EnemyRace")
                        .HasColumnType("TEXT");

                    b.Property<string>("EnemyTags")
                        .HasColumnType("TEXT");

                    b.Property<bool>("HideInHandbook")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsInvalidKilled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Resistance")
                        .HasColumnType("TEXT");

                    b.Property<int>("SortId")
                        .HasColumnType("INTEGER");

                    b.HasKey("EnemyId");

                    b.ToTable("Enemies");
                });

            modelBuilder.Entity("RIDC.Schema.Item", b =>
                {
                    b.Property<string>("ItemId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClassifyType")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("IconId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ItemType")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("ObtainApproach")
                        .HasColumnType("TEXT");

                    b.Property<int>("Rarity")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SortId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("StackIconId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Usage")
                        .HasColumnType("TEXT");

                    b.HasKey("ItemId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("RIDC.Schema.Miscellaneous", b =>
                {
                    b.Property<string>("Key")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("Key");

                    b.ToTable("Miscellaneous");
                });

            modelBuilder.Entity("RIDC.Schema.Power", b =>
                {
                    b.Property<string>("PowerId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Color")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsLimited")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsRaw")
                        .HasColumnType("INTEGER");

                    b.Property<int>("OrderNum")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PowerCode")
                        .HasColumnType("TEXT");

                    b.Property<int>("PowerLevel")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PowerName")
                        .HasColumnType("TEXT");

                    b.HasKey("PowerId");

                    b.ToTable("Powers");
                });

            modelBuilder.Entity("RIDC.Schema.Skill", b =>
                {
                    b.Property<string>("SkillId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("SkillId");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("RIDC.Schema.Skin", b =>
                {
                    b.Property<string>("SkinId")
                        .HasColumnType("TEXT");

                    b.Property<string>("AvatarId")
                        .HasColumnType("TEXT");

                    b.Property<string>("CharacterId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Dialog")
                        .HasColumnType("TEXT");

                    b.Property<string>("DrawerName")
                        .HasColumnType("TEXT");

                    b.Property<string>("DynamicIllustId")
                        .HasColumnType("TEXT");

                    b.Property<string>("DynamicPortraitId")
                        .HasColumnType("TEXT");

                    b.Property<string>("IllustId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ModelName")
                        .HasColumnType("TEXT");

                    b.Property<string>("ObtainApproach")
                        .HasColumnType("TEXT");

                    b.Property<string>("PortraitId")
                        .HasColumnType("TEXT");

                    b.Property<string>("SkinGroupId")
                        .HasColumnType("TEXT");

                    b.Property<string>("SkinGroupName")
                        .HasColumnType("TEXT");

                    b.Property<int>("SkinGroupSortIndex")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SkinName")
                        .HasColumnType("TEXT");

                    b.Property<int>("SortId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Usage")
                        .HasColumnType("TEXT");

                    b.HasKey("SkinId");

                    b.HasIndex("CharacterId");

                    b.ToTable("Skins");
                });

            modelBuilder.Entity("RIDC.Schema.Stage", b =>
                {
                    b.Property<string>("StageId")
                        .HasColumnType("TEXT");

                    b.Property<int>("ApCost")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ApFailReturn")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("BossMark")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("CanBattleReplay")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("CanPractice")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .HasColumnType("TEXT");

                    b.Property<int>("CompleteFavor")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DailyStageDifficulty")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DangerLevel")
                        .HasColumnType("TEXT");

                    b.Property<float>("DangerPoint")
                        .HasColumnType("REAL");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Difficulty")
                        .HasColumnType("TEXT");

                    b.Property<int>("EtCost")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EtFailReturn")
                        .HasColumnType("INTEGER");

                    b.Property<string>("EtItemId")
                        .HasColumnType("TEXT");

                    b.Property<int>("ExpGain")
                        .HasColumnType("INTEGER");

                    b.Property<int>("GoldGain")
                        .HasColumnType("INTEGER");

                    b.Property<string>("HardStagedId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("HighLightMark")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsHardPredefined")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsPredefined")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsSkillSelectablePredefined")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsStoryOnly")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LevelId")
                        .HasColumnType("TEXT");

                    b.Property<int>("LoseExpGain")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LoseGoldGain")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("PassFavor")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PerformanceStageFlag")
                        .HasColumnType("TEXT");

                    b.Property<int>("PracticeTicketCost")
                        .HasColumnType("INTEGER");

                    b.Property<string>("StageType")
                        .HasColumnType("TEXT");

                    b.Property<int>("StoryLineProgress")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ZoneId")
                        .HasColumnType("TEXT");

                    b.HasKey("StageId");

                    b.HasIndex("ZoneId");

                    b.ToTable("Stages");
                });

            modelBuilder.Entity("RIDC.Schema.Tip", b =>
                {
                    b.Property<string>("TipId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Category")
                        .HasColumnType("TEXT");

                    b.Property<string>("TipContent")
                        .HasColumnType("TEXT");

                    b.Property<float>("Weight")
                        .HasColumnType("REAL");

                    b.HasKey("TipId");

                    b.ToTable("Tips");
                });

            modelBuilder.Entity("RIDC.Schema.Zone", b =>
                {
                    b.Property<string>("ZoneId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("CanPreview")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LockedText")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.Property<int>("ZoneIndex")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ZoneNameFirst")
                        .HasColumnType("TEXT");

                    b.Property<string>("ZoneNameSecond")
                        .HasColumnType("TEXT");

                    b.Property<string>("ZoneNameThird")
                        .HasColumnType("TEXT");

                    b.Property<string>("ZoneNameTitleCurrent")
                        .HasColumnType("TEXT");

                    b.Property<string>("ZoneNameTitleEx")
                        .HasColumnType("TEXT");

                    b.Property<string>("ZoneNameTitleUnCurrent")
                        .HasColumnType("TEXT");

                    b.HasKey("ZoneId");

                    b.ToTable("Zones");
                });

            modelBuilder.Entity("CharacterSkill", b =>
                {
                    b.HasOne("RIDC.Schema.Character", null)
                        .WithMany()
                        .HasForeignKey("CharactersCharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RIDC.Schema.Skill", null)
                        .WithMany()
                        .HasForeignKey("SkillsSkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RIDC.Schema.Character", b =>
                {
                    b.HasOne("RIDC.Schema.Power", "Nation")
                        .WithMany()
                        .HasForeignKey("NationPowerId");

                    b.Navigation("Nation");
                });

            modelBuilder.Entity("RIDC.Schema.Skin", b =>
                {
                    b.HasOne("RIDC.Schema.Character", null)
                        .WithMany("Skins")
                        .HasForeignKey("CharacterId");
                });

            modelBuilder.Entity("RIDC.Schema.Stage", b =>
                {
                    b.HasOne("RIDC.Schema.Zone", null)
                        .WithMany("Stages")
                        .HasForeignKey("ZoneId");
                });

            modelBuilder.Entity("RIDC.Schema.Character", b =>
                {
                    b.Navigation("Skins");
                });

            modelBuilder.Entity("RIDC.Schema.Zone", b =>
                {
                    b.Navigation("Stages");
                });
#pragma warning restore 612, 618
        }
    }
}
