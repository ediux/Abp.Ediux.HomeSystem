using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Ediux.ABP.Features.SmartAdminUI.EntityFrameworkCore
{
    public class SmartAdminUIHttpApiHostMigrationsDbContext : AbpDbContext<SmartAdminUIHttpApiHostMigrationsDbContext>
    {
        public SmartAdminUIHttpApiHostMigrationsDbContext(DbContextOptions<SmartAdminUIHttpApiHostMigrationsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureSmartAdminUI();
        }
    }
}
