using Ediux.ABP.Features.SmartAdminUI.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Ediux.ABP.Features.SmartAdminUI
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(SmartAdminUIEntityFrameworkCoreTestModule)
        )]
    public class SmartAdminUIDomainTestModule : AbpModule
    {
        
    }
}
