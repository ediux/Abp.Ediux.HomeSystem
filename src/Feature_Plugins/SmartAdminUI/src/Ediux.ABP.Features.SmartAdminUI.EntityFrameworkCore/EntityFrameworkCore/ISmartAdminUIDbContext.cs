using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Ediux.ABP.Features.SmartAdminUI.EntityFrameworkCore
{
    [ConnectionStringName(SmartAdminUIDbProperties.ConnectionStringName)]
    public interface ISmartAdminUIDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
    }
}