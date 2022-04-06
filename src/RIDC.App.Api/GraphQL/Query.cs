using Microsoft.EntityFrameworkCore;
using RIDC.App.Api.Attributes;
using RIDC.Database;
using RIDC.Schema;

namespace RIDC.App.Api.GraphQL;

public class Query
{
    [UseApplicationDbContext]
    [UsePaging(DefaultPageSize = 10, MaxPageSize = 100)]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Character> GetCharacter([ScopedService] RhodesIslandDbContext db)
        => db.Characters
            .OrderBy(x => x.CharacterId)
            .Include(x => x.Nation)
            .Include(x => x.Skills)
            .Include(x => x.Skins);

    [UseApplicationDbContext]
    [UsePaging(DefaultPageSize = 10, MaxPageSize = 100)]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Charm> GetCharm([ScopedService] RhodesIslandDbContext db)
        => db.Charms.OrderBy(x => x.CharmId);

    [UseApplicationDbContext]
    [UsePaging(DefaultPageSize = 10, MaxPageSize = 100)]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Enemy> GetEnemy([ScopedService] RhodesIslandDbContext db)
        => db.Enemies.OrderBy(x => x.EnemyId);

    [UseApplicationDbContext]
    [UsePaging(DefaultPageSize = 10, MaxPageSize = 100)]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Item> GetItem([ScopedService] RhodesIslandDbContext db)
        => db.Items.OrderBy(x => x.ItemId);

    [UseApplicationDbContext]
    [UsePaging(DefaultPageSize = 10, MaxPageSize = 100)]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Power> GetPower([ScopedService] RhodesIslandDbContext db)
        => db.Powers.OrderBy(x => x.PowerId);

    [UseApplicationDbContext]
    [UsePaging(DefaultPageSize = 10, MaxPageSize = 100)]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Skill> GetSkill([ScopedService] RhodesIslandDbContext db)
        => db.Skills.OrderBy(x => x.SkillId);

    [UseApplicationDbContext]
    [UsePaging(DefaultPageSize = 10, MaxPageSize = 100)]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Skin> GetSkins([ScopedService] RhodesIslandDbContext db)
        => db.Skins.OrderBy(x => x.SkinId);

    [UseApplicationDbContext]
    [UsePaging(DefaultPageSize = 10, MaxPageSize = 100)]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Stage> GetStage([ScopedService] RhodesIslandDbContext db)
        => db.Stages.OrderBy(x => x.StageId);

    [UseApplicationDbContext]
    [UsePaging(DefaultPageSize = 10, MaxPageSize = 100)]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Tip> GetTip([ScopedService] RhodesIslandDbContext db)
        => db.Tips.OrderBy(x => x.TipId);

    [UseApplicationDbContext]
    [UsePaging(DefaultPageSize = 10, MaxPageSize = 100)]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Zone> GetZone([ScopedService] RhodesIslandDbContext db)
        => db.Zones
            .OrderBy(x => x.ZoneId)
            .Include(x => x.Stages);

    [UseApplicationDbContext]
    public Miscellaneous GetVersion([ScopedService] RhodesIslandDbContext db)
    {
        var v = db.Miscellaneous.FirstOrDefault(x => x.Key == "version");
        return v ?? new Miscellaneous { Key = "version", Value = "" };
    }
}
