using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Ediux.HomeSystem.Plugins.HololivePages.EntityFrameworkCore
{
    [ConnectionStringName(HololivePagesDbProperties.ConnectionStringName)]
    public interface IHololivePagesDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
    }
}