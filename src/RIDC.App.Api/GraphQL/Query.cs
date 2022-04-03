using Microsoft.EntityFrameworkCore;
using RIDC.Schema;

namespace RIDC.App.Api.GraphQL;

public class Query
{
    [UsePaging(DefaultPageSize = 10, MaxPageSize = 100)]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Character> GetCharacter([Service] RhodesIslandDbContextBase db)
        => db.Characters.Include(x => x.Nation).Include(x => x.Skills);

    [UsePaging(DefaultPageSize = 10, MaxPageSize = 100)]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Charm> GetCharm([Service] RhodesIslandDbContextBase db)
        => db.Charms;

    [UsePaging(DefaultPageSize = 10, MaxPageSize = 100)]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Item> GetItem([Service] RhodesIslandDbContextBase db)
        => db.Items;

    [UsePaging(DefaultPageSize = 10, MaxPageSize = 100)]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Power> GetPower([Service] RhodesIslandDbContextBase db)
        => db.Powers;

    [UsePaging(DefaultPageSize = 10, MaxPageSize = 100)]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Skill> GetSkill([Service] RhodesIslandDbContextBase db)
        => db.Skills;

    [UsePaging(DefaultPageSize = 10, MaxPageSize = 100)]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Stage> GetStage([Service] RhodesIslandDbContextBase db)
        => db.Stages;

    [UsePaging(DefaultPageSize = 10, MaxPageSize = 100)]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Tip> GetTip([Service] RhodesIslandDbContextBase db)
        => db.Tips;

    [UsePaging(DefaultPageSize = 10, MaxPageSize = 100)]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Zone> GetZone([Service] RhodesIslandDbContextBase db)
        => db.Zones.Include(x => x.Stages);
}
