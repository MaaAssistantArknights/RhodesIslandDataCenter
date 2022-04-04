using System.Reflection;
using HotChocolate.Types.Descriptors;
using RIDC.App.Api.Extensions;
using RIDC.Database;

namespace RIDC.App.Api.Attributes;

public class UseApplicationDbContextAttribute : ObjectFieldDescriptorAttribute
{
    public override void OnConfigure(
        IDescriptorContext context,
        IObjectFieldDescriptor descriptor,
        MemberInfo member)
    {
        descriptor.UseAppDbContext<RhodesIslandDbContext>();
    }
}
