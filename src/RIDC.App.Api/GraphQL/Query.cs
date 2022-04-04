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
        => db.Characters.Include(x => x.Nation).Include(x => x.Skills);

    [UseApplicationDbContext]
    [UsePaging(DefaultPageSize = 10, MaxPageSize = 100)]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Charm> GetCharm([ScopedService] RhodesIslandDbContext db)
        => db.Charms;

    [UseApplicationDbContext]
    [UsePaging(DefaultPageSize = 10, MaxPageSize = 100)]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Item> GetItem([ScopedService] RhodesIslandDbContext db)
        => db.Items;

    [UseApplicationDbContext]
    [UsePaging(DefaultPageSize = 10, MaxPageSize = 100)]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Power> GetPower([ScopedService] RhodesIslandDbContext db)
        => db.Powers;

    [UseApplicationDbContext]
    [UsePaging(DefaultPageSize = 10, MaxPageSize = 100)]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Skill> GetSkill([ScopedService] RhodesIslandDbContext db)
        => db.Skills;

    [UseApplicationDbContext]
    [UsePaging(DefaultPageSize = 10, MaxPageSize = 100)]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Stage> GetStage([ScopedService] RhodesIslandDbContext db)
        => db.Stages;

    [UseApplicationDbContext]
    [UsePaging(DefaultPageSize = 10, MaxPageSize = 100)]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Tip> GetTip([ScopedService] RhodesIslandDbContext db)
        => db.Tips;

    [UseApplicationDbContext]
    [UsePaging(DefaultPageSize = 10, MaxPageSize = 100)]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Zone> GetZone([ScopedService] RhodesIslandDbContext db)
        => db.Zones.Include(x => x.Stages);

    [UseApplicationDbContext]
    public Miscellaneous GetVersion([ScopedService] RhodesIslandDbContext db)
    {
        var v = db.Miscellaneous.FirstOrDefault(x => x.Key == "version");
        return v ?? new Miscellaneous { Key = "version", Value = "" };
    }
}
