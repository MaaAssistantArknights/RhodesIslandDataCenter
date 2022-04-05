﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RIDC.Database;

#nullable disable

namespace RIDC.Database.Migrations.MariaDb.Migrations
{
    [DbContext(typeof(RhodesIslandDbContext))]
    [Migration("20220405115621_AddEnemyTable")]
    partial class AddEnemyTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CharacterSkill", b =>
                {
                    b.Property<string>("CharactersCharacterId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("SkillsSkillId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("CharactersCharacterId", "SkillsSkillId");

                    b.HasIndex("SkillsSkillId");

                    b.ToTable("relation_CharacterSkill", (string)null);
                });

            modelBuilder.Entity("RIDC.Schema.Character", b =>
                {
                    b.Property<string>("CharacterId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Appellation")
                        .HasColumnType("longtext");

                    b.Property<bool>("CanUseGeneralPotentialItem")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("DisplayNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("GroupId")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsNotObtainable")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsSpChar")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ItemDesc")
                        .HasColumnType("longtext");

                    b.Property<string>("ItemObtainApproach")
                        .HasColumnType("longtext");

                    b.Property<string>("ItemUsage")
                        .HasColumnType("longtext");

                    b.Property<int>("MaxPotentialLevel")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("NationPowerId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Position")
                        .HasColumnType("longtext");

                    b.Property<string>("PotentialItemId")
                        .HasColumnType("longtext");

                    b.Property<string>("Profession")
                        .HasColumnType("longtext");

                    b.Property<int>("Rarity")
                        .HasColumnType("int");

                    b.Property<string>("SubProfessionId")
                        .HasColumnType("longtext");

                    b.Property<string>("TagList")
                        .HasColumnType("longtext");

                    b.Property<string>("TeamId")
                        .HasColumnType("longtext");

                    b.Property<string>("TokenKey")
                        .HasColumnType("longtext");

                    b.HasKey("CharacterId");

                    b.HasIndex("NationPowerId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("RIDC.Schema.Charm", b =>
                {
                    b.Property<string>("CharmId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CharmEffect")
                        .HasColumnType("longtext");

                    b.Property<string>("CharmType")
                        .HasColumnType("longtext");

                    b.Property<string>("Desc")
                        .HasColumnType("longtext");

                    b.Property<string>("Icon")
                        .HasColumnType("longtext");

                    b.Property<string>("ItemDesc")
                        .HasColumnType("longtext");

                    b.Property<string>("ItemObtainApproach")
                        .HasColumnType("longtext");

                    b.Property<string>("ItemUsage")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<bool>("ObtainInRandom")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Rarity")
                        .HasColumnType("int");

                    b.Property<int>("Sort")
                        .HasColumnType("int");

                    b.Property<string>("SpecialObtainApproach")
                        .HasColumnType("longtext");

                    b.HasKey("CharmId");

                    b.ToTable("Charms");
                });

            modelBuilder.Entity("RIDC.Schema.Enemy", b =>
                {
                    b.Property<string>("EnemyId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Ability")
                        .HasColumnType("longtext");

                    b.Property<string>("Attack")
                        .HasColumnType("longtext");

                    b.Property<string>("AttackType")
                        .HasColumnType("longtext");

                    b.Property<string>("Defence")
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Endure")
                        .HasColumnType("longtext");

                    b.Property<string>("EnemyIndex")
                        .HasColumnType("longtext");

                    b.Property<string>("EnemyLevel")
                        .HasColumnType("longtext");

                    b.Property<string>("EnemyRace")
                        .HasColumnType("longtext");

                    b.Property<string>("EnemyTags")
                        .HasColumnType("longtext");

                    b.Property<string>("HideInHandbook")
                        .HasColumnType("longtext");

                    b.Property<string>("IsInvalidKilled")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Resistance")
                        .HasColumnType("longtext");

                    b.Property<int>("SortId")
                        .HasColumnType("int");

                    b.HasKey("EnemyId");

                    b.ToTable("Enemies");
                });

            modelBuilder.Entity("RIDC.Schema.Item", b =>
                {
                    b.Property<string>("ItemId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ClassifyType")
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("IconId")
                        .HasColumnType("longtext");

                    b.Property<string>("ItemType")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("ObtainApproach")
                        .HasColumnType("longtext");

                    b.Property<int>("Rarity")
                        .HasColumnType("int");

                    b.Property<int>("SortId")
                        .HasColumnType("int");

                    b.Property<string>("StackIconId")
                        .HasColumnType("longtext");

                    b.Property<string>("Usage")
                        .HasColumnType("longtext");

                    b.HasKey("ItemId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("RIDC.Schema.Miscellaneous", b =>
                {
                    b.Property<string>("Key")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("Key");

                    b.ToTable("Miscellaneous");
                });

            modelBuilder.Entity("RIDC.Schema.Power", b =>
                {
                    b.Property<string>("PowerId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Color")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsLimited")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsRaw")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("OrderNum")
                        .HasColumnType("int");

                    b.Property<string>("PowerCode")
                        .HasColumnType("longtext");

                    b.Property<int>("PowerLevel")
                        .HasColumnType("int");

                    b.Property<string>("PowerName")
                        .HasColumnType("longtext");

                    b.HasKey("PowerId");

                    b.ToTable("Powers");
                });

            modelBuilder.Entity("RIDC.Schema.Skill", b =>
                {
                    b.Property<string>("SkillId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("SkillId");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("RIDC.Schema.Stage", b =>
                {
                    b.Property<string>("StageId")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("ApCost")
                        .HasColumnType("int");

                    b.Property<int>("ApFailReturn")
                        .HasColumnType("int");

                    b.Property<bool>("BossMark")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("CanBattleReplay")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("CanPractice")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Code")
                        .HasColumnType("longtext");

                    b.Property<int>("CompleteFavor")
                        .HasColumnType("int");

                    b.Property<int>("DailyStageDifficulty")
                        .HasColumnType("int");

                    b.Property<string>("DangerLevel")
                        .HasColumnType("longtext");

                    b.Property<float>("DangerPoint")
                        .HasColumnType("float");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Difficulty")
                        .HasColumnType("longtext");

                    b.Property<int>("EtCost")
                        .HasColumnType("int");

                    b.Property<int>("EtFailReturn")
                        .HasColumnType("int");

                    b.Property<string>("EtItemId")
                        .HasColumnType("longtext");

                    b.Property<int>("ExpGain")
                        .HasColumnType("int");

                    b.Property<int>("GoldGain")
                        .HasColumnType("int");

                    b.Property<string>("HardStagedId")
                        .HasColumnType("longtext");

                    b.Property<bool>("HighLightMark")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsHardPredefined")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsPredefined")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsSkillSelectablePredefined")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsStoryOnly")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LevelId")
                        .HasColumnType("longtext");

                    b.Property<int>("LoseExpGain")
                        .HasColumnType("int");

                    b.Property<int>("LoseGoldGain")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int>("PassFavor")
                        .HasColumnType("int");

                    b.Property<string>("PerformanceStageFlag")
                        .HasColumnType("longtext");

                    b.Property<int>("PracticeTicketCost")
                        .HasColumnType("int");

                    b.Property<string>("StageType")
                        .HasColumnType("longtext");

                    b.Property<int>("StoryLineProgress")
                        .HasColumnType("int");

                    b.Property<string>("ZoneId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("StageId");

                    b.HasIndex("ZoneId");

                    b.ToTable("Stages");
                });

            modelBuilder.Entity("RIDC.Schema.Tip", b =>
                {
                    b.Property<string>("TipId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Category")
                        .HasColumnType("longtext");

                    b.Property<string>("TipContent")
                        .HasColumnType("longtext");

                    b.Property<float>("Weight")
                        .HasColumnType("float");

                    b.HasKey("TipId");

                    b.ToTable("Tips");
                });

            modelBuilder.Entity("RIDC.Schema.Zone", b =>
                {
                    b.Property<string>("ZoneId")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("CanPreview")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LockedText")
                        .HasColumnType("longtext");

                    b.Property<string>("Type")
                        .HasColumnType("longtext");

                    b.Property<int>("ZoneIndex")
                        .HasColumnType("int");

                    b.Property<string>("ZoneNameFirst")
                        .HasColumnType("longtext");

                    b.Property<string>("ZoneNameSecond")
                        .HasColumnType("longtext");

                    b.Property<string>("ZoneNameThird")
                        .HasColumnType("longtext");

                    b.Property<string>("ZoneNameTitleCurrent")
                        .HasColumnType("longtext");

                    b.Property<string>("ZoneNameTitleEx")
                        .HasColumnType("longtext");

                    b.Property<string>("ZoneNameTitleUnCurrent")
                        .HasColumnType("longtext");

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

            modelBuilder.Entity("RIDC.Schema.Stage", b =>
                {
                    b.HasOne("RIDC.Schema.Zone", null)
                        .WithMany("Stages")
                        .HasForeignKey("ZoneId");
                });

            modelBuilder.Entity("RIDC.Schema.Zone", b =>
                {
                    b.Navigation("Stages");
                });
#pragma warning restore 612, 618
        }
    }
}
