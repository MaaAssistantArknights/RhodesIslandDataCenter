using Microsoft.EntityFrameworkCore;
using RIDC.Schema;

namespace RIDC.Database;

public abstract class RhodesIslandDbContextBase : DbContext
{
    public DbSet<Character> Characters { get; set; }
    public DbSet<Charm> Charms { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Power> Powers { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<Stage> Stages { get; set; }
    public DbSet<Tip> Tips { get; set; }
    public DbSet<Zone> Zones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Character

        Func<ICollection<string>, string> stringCollectionToStringConvertor = string(s) => string.Join("$$", s);
        Func<string, ICollection<string>> stringToStringCollectionConvertor = ICollection<string>(s) => s.Split("$$");

        modelBuilder.Entity<Character>(p =>
        {
            p.HasKey(x => x.CharacterId);
            p.HasOne(x => x.Nation);
            p.HasMany(x => x.Skills)
                .WithMany(x => x.Characters)
                .UsingEntity(builder => builder.ToTable("relation_CharacterSkill"));
            p.Property(x => x.TagList)
                .HasConversion(
                    s => stringCollectionToStringConvertor.Invoke(s),
                    s => stringToStringCollectionConvertor.Invoke(s));
        });

        #endregion

        #region Charm

        modelBuilder.Entity<Charm>(p =>
        {
            p.HasKey(x => x.CharmId);
        });

        #endregion

        #region Item

        modelBuilder.Entity<Item>(p =>
        {
            p.HasKey(x => x.ItemId);
        });

        #endregion

        #region Power

        modelBuilder.Entity<Power>(p =>
        {
            p.HasKey(x => x.PowerId);
        });

        #endregion

        #region Skill

        modelBuilder.Entity<Skill>(p =>
        {
            p.HasKey(x => x.SkillId);
        });

        #endregion

        #region Stage

        modelBuilder.Entity<Stage>(p =>
        {
            p.HasKey(x => x.StageId);
        });

        #endregion

        #region Tip

        modelBuilder.Entity<Tip>(p =>
        {
            p.HasKey(x => x.TipId);
        });

        #endregion

        #region Zone

        modelBuilder.Entity<Zone>(p =>
        {
            p.HasKey(x => x.ZoneId);
            p.HasMany(x => x.Stages);
        });

        #endregion
    }
}
