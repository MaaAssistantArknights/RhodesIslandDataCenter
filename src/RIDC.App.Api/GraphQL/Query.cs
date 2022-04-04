using Microsoft.EntityFrameworkCore;
using RIDC.Database;
using RIDC.Schema;

namespace RIDC.App.Api.GraphQL;

public class Query
{
    [UsePaging(DefaultPageSize = 10, MaxPageSize = 100)]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Character> GetCharacter([Service] RhodesIslandDbContext db)
        => db.Characters.Include(x => x.Nation).Include(x => x.Skills);

    [UsePaging(DefaultPageSize = 10, MaxPageSize = 100)]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Charm> GetCharm([Service] RhodesIslandDbContext db)
        => db.Charms;

    [UsePaging(DefaultPageSize = 10, MaxPageSize = 100)]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Item> GetItem([Service] RhodesIslandDbContext db)
        => db.Items;

    [UsePaging(DefaultPageSize = 10, MaxPageSize = 100)]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Power> GetPower([Service] RhodesIslandDbContext db)
        => db.Powers;

    [UsePaging(DefaultPageSize = 10, MaxPageSize = 100)]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Skill> GetSkill([Service] RhodesIslandDbContext db)
        => db.Skills;

    [UsePaging(DefaultPageSize = 10, MaxPageSize = 100)]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Stage> GetStage([Service] RhodesIslandDbContext db)
        => db.Stages;

    [UsePaging(DefaultPageSize = 10, MaxPageSize = 100)]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Tip> GetTip([Service] RhodesIslandDbContext db)
        => db.Tips;

    [UsePaging(DefaultPageSize = 10, MaxPageSize = 100)]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Zone> GetZone([Service] RhodesIslandDbContext db)
        => db.Zones.Include(x => x.Stages);

    public Miscellaneous GetVersion([Service] RhodesIslandDbContext db)
    {
        var v = db.Miscellaneous.FirstOrDefault(x => x.Key == "version");
        return v ?? new Miscellaneous { Key = "version", Value = "" };
    }
}
