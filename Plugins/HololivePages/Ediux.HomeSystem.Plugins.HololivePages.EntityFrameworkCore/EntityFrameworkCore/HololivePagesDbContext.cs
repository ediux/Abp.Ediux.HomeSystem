using Ediux.HomeSystem.Plugins.HololivePages.Data;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Ediux.HomeSystem.Plugins.HololivePages.EntityFrameworkCore
{
    [ConnectionStringName(HololivePagesDbProperties.ConnectionStringName)]
    public class HololivePagesDbContext : AbpDbContext<HololivePagesDbContext>, IHololivePagesDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */
        public DbSet<Groups> Hololive_Groups { get; set; }

        public DbSet<Company> Hololive_Companies { get; set; }

        public DbSet<Departments> Hololive_Departments { get; set; }

        public DbSet<Branches> Hololive_Branches { get; set; }

        public DbSet<Members> Hololive_Members { get; set; }

        public DbSet<MemberEvents> Hololive_MemberEvents { get; set; }

        public DbSet<PhotosRefence> Hololive_PhotosRefence { get; set; }

        public DbSet<YTuberVideoRefence> Hololive_YTuberVideoRefence { get; set; }

        public HololivePagesDbContext(DbContextOptions<HololivePagesDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
         
            builder.ConfigureHololivePages();
        }


    }
}