using Microsoft.EntityFrameworkCore;

namespace RIDC.App.Api.Extensions;

public static class ObjectFieldDescriptorExtensions
{
    public static IObjectFieldDescriptor UseAppDbContext<TDbContext>(
        this IObjectFieldDescriptor descriptor)
        where TDbContext : DbContext
    {
        return descriptor.UseScopedService(
            s => s.GetRequiredService<IDbContextFactory<TDbContext>>().CreateDbContext());
    }
}
