using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Ediux.ABP.Features.SmartAdminUI.EntityFrameworkCore
{
    [ConnectionStringName(SmartAdminUIDbProperties.ConnectionStringName)]
    public class SmartAdminUIDbContext : AbpDbContext<SmartAdminUIDbContext>, ISmartAdminUIDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */

        public SmartAdminUIDbContext(DbContextOptions<SmartAdminUIDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureSmartAdminUI();
        }
    }
}