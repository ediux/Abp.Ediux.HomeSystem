using Ediux.HomeSystem.Data;

using Microsoft.EntityFrameworkCore;

using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.ObjectExtending;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using Volo.Docs.EntityFrameworkCore;
using Volo.CmsKit.EntityFrameworkCore;

namespace Ediux.HomeSystem.EntityFrameworkCore
{
    [ReplaceDbContext(typeof(IIdentityDbContext))]
    [ReplaceDbContext(typeof(ITenantManagementDbContext))]
    [ConnectionStringName("Default")]
    public class HomeSystemDbContext :
        AbpDbContext<HomeSystemDbContext>,
        IIdentityDbContext,
        ITenantManagementDbContext
    {
        /* Add DbSet properties for your Aggregate Roots / Entities here. */
        public DbSet<AbpPlugins> AbpPlugins { get; set; }

        public DbSet<PersonalCalendar> PersonalCalendars { get; set; }

        public DbSet<File_Store> Files { get; set; }

        public DbSet<MIMEType> MIMETypes { get; set; }

        public DbSet<ProductKeys> ProductKeys { get; set; }

        public DbSet<UserPasswordStore> UserPasswordStores { get; set; }

        public DbSet<DashboardWidgets> DashboardWidgets { get; set; }

        public DbSet<DashboardWidgetUsers> DashboardWidgetUsers { get; set; }

        public DbSet<ComponentsRegistration> ComponentsRegistrations { get; set; }

        public DbSet<GCMUserTokenMapping> GCMUserTokenMappings { get; set; }    

        #region Entities from the modules

        /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
         * and replaced them for this DbContext. This allows you to perform JOIN
         * queries for the entities of these modules over the repositories easily. You
         * typically don't need that for other modules. But, if you need, you can
         * implement the DbContext interface of the needed module and use ReplaceDbContext
         * attribute just like IIdentityDbContext and ITenantManagementDbContext.
         *
         * More info: Replacing a DbContext of a module ensures that the related module
         * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
         */

        //Identity
        public DbSet<IdentityUser> Users { get; set; }
        public DbSet<IdentityRole> Roles { get; set; }
        public DbSet<IdentityClaimType> ClaimTypes { get; set; }
        public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
        public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
        public DbSet<IdentityLinkUser> LinkUsers { get; set; }

        // Tenant Management
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

        #endregion

        public HomeSystemDbContext(DbContextOptions<HomeSystemDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Include modules to your migration db context */

            builder.ConfigurePermissionManagement();
            builder.ConfigureSettingManagement();
            builder.ConfigureBackgroundJobs();
            builder.ConfigureAuditLogging();
            builder.ConfigureIdentity();
            builder.ConfigureIdentityServer();
            builder.ConfigureFeatureManagement();
            builder.ConfigureTenantManagement();
            builder.ConfigureDocs();

            /* Configure your own tables/entities inside here */
            #region 系統內建功能資料表
            ConfigurePlugins(builder);
            ConfigureCalendar(builder);
            ConfigureFileStore(builder);
            ConfigureMIMEType(builder);
            ConfigureProductKeys(builder);
            ConfigureUserPasswordStore(builder);
            ConfigureDashboardWidgets(builder);
            ConfigureComponentsRegistrations(builder);
            ConfigurePushNotifition(builder);   
            #endregion

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(HomeSystemConsts.DbTablePrefix + "YourEntities", HomeSystemConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});
            builder.ConfigureDocs();
            builder.ConfigureCmsKit();
        }
        private void ConfigurePushNotifition(in ModelBuilder builder)
        {
            builder.Entity<GCMUserTokenMapping>(d => {
                d.ToTable(HomeSystemConsts.DbTablePrefix + "_GCMUserTokenMapping");
                
                d.Property(p=>p.user_token)
                .HasMaxLength(2048)
                .IsUnicode(true)
                .IsRequired();  
            });
        }
        private void ConfigureComponentsRegistrations(in ModelBuilder builder)
        {
            builder.Entity<ComponentsRegistration>(d =>
            {
                d.ToTable(HomeSystemConsts.DbTablePrefix + "_ComponentsRegistration", HomeSystemConsts.DbSchema);

                d.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(256)
                .IsUnicode(true);

                d.Property(p => p.SettingName)
                .HasMaxLength(256)
                .IsUnicode(true);

                d.Property(p => p.PermissionName)
                .HasMaxLength(256)
                .IsUnicode(true);

                d.Property(p => p.HasOption)
                .IsRequired()
                .HasDefaultValue(false);

                d.Property(p => p.AllowUserSetting)
                .IsRequired()
                .HasDefaultValue(true);
            });
        }

        private void ConfigureDashboardWidgets(in ModelBuilder builder)
        {
            builder.Entity<DashboardWidgets>(d =>
            {
                d.ToTable(HomeSystemConsts.DbTablePrefix + "_DashboardWidgets", HomeSystemConsts.DbSchema);

                d.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(256)
                .IsUnicode(true);

                d.Property(p => p.DisplayName)
                  .HasMaxLength(256)
                  .IsUnicode(true);

               

                d.Property(p => p.PermissionName)
                .HasMaxLength(256)
                .IsUnicode(true);

                d.Property(p => p.HasOption)
                .IsRequired()
                .HasDefaultValue(false);

                d.Property(p => p.AllowMulti)
                .IsRequired()
                .HasDefaultValue(false);

                d.Property(p => p.AllowUserSetting)
                .IsRequired()
                .HasDefaultValue(true);

                d.Property(p => p.IsDefault)
                .HasDefaultValue(false)
                .IsRequired();

                d.Property(p => p.Order)
                .HasDefaultValue(-1)
                .IsRequired();

                d.HasMany(p => p.AssginedUsers)
                    .WithOne(p => p.DashboardWidget)
                    .HasForeignKey(p => p.DashboardWidgetId);
            });

            builder.Entity<DashboardWidgetUsers>(d =>
            {
                d.ToTable(HomeSystemConsts.DbTablePrefix + "_DashboardWidgetUsers", HomeSystemConsts.DbSchema);

                d.HasKey(p => new { p.Id, p.DashboardWidgetId });

                d.HasOne(p => p.DashboardWidget)
                .WithMany(p => p.AssginedUsers)
                .HasForeignKey(p => p.DashboardWidgetId)
                .IsRequired();

                d.Property(p => p.UserSettings)
                .HasMaxLength(int.MaxValue)
                .IsUnicode(true);
            });
        }

        private void ConfigureUserPasswordStore(in ModelBuilder builder)
        {
            builder.Entity<UserPasswordStore>(d =>
            {
                d.ToTable(HomeSystemConsts.DbTablePrefix + "_UserPasswordStore", HomeSystemConsts.DbSchema);

                d.Property(p => p.Account)
                .HasMaxLength(256)
                .IsUnicode(true);

                d.Property(p => p.IsHistory)
                .IsRequired();


                d.Property(p => p.Password)
              .HasMaxLength(256)
              .IsUnicode(true)
              .IsRequired();

                d.Property(p => p.Site)
                .HasMaxLength(2048)
                .IsUnicode(true);

                d.Property(p => p.SiteName)
                .HasMaxLength(50)
                .IsUnicode(true)
                .IsRequired();

                d.ConfigureByConvention();
            });
        }

        private void ConfigureProductKeys(in ModelBuilder builder)
        {
            builder.Entity<ProductKeys>(d =>
            {
                d.ToTable(HomeSystemConsts.DbTablePrefix + "_ProductKeys", HomeSystemConsts.DbSchema);

                d.Property(p => p.ProductKey)
                .HasMaxLength(256)
                .IsUnicode(true)
                .IsRequired();

                d.Property(p => p.ProductName)
                .HasMaxLength(256)
                .IsUnicode(true)
                .IsRequired();

                d.Property(p => p.Shared)
                .IsRequired();

                d.ConfigureByConvention();
            });
        }

        private void ConfigureMIMEType(in ModelBuilder builder)
        {
            builder.Entity<MIMEType>(d =>
            {
                d.ToTable(HomeSystemConsts.DbTablePrefix + "_MIMETypes", HomeSystemConsts.DbSchema);

                d.Property(p => p.MIME)
                .HasMaxLength(256)
                .IsUnicode(true)
                .IsRequired();

                d.Property(p => p.RefenceExtName)
                .HasMaxLength(256)
                .IsUnicode(true);

                d.Property(p => p.Description)
                .IsUnicode(true);

                d.ConfigureByConvention();

                d.HasMany(p => p.Files)
                .WithOne(p => p.MIME)
                .HasForeignKey(x => x.MIMETypeId)
                .IsRequired();

            });
        }

        private void ConfigureFileStore(in ModelBuilder builder)
        {
            builder.Entity<File_Store>(d =>
            {
                d.ToTable(HomeSystemConsts.DbTablePrefix + "_File_Store", HomeSystemConsts.DbSchema);

                d.Property(p => p.ExtName)
                .HasMaxLength(256)
                .IsUnicode(true);

                d.Property(p => p.InRecycle)
                .IsRequired();

                d.Property(p => p.IsCrypto)
                .IsRequired();

                d.Property(p => p.MIMETypeId)
                .IsRequired();

                d.Property(p => p.Name)
                .HasMaxLength(256)
                .IsUnicode(true);

                d.Property(p => p.OriginFullPath)
                .HasMaxLength(2048)
                .IsUnicode(true);

                d.Property(p => p.Size)
                .IsRequired();

                d.Property(p => p.SMBFullPath)
                .HasMaxLength(2048)
                .IsUnicode(true);

                d.Property(p => p.SMBLoginId)
                .HasMaxLength(50)
                .IsUnicode(true);

                d.Property(p => p.SMBPassword)
                .HasMaxLength(2048)
                .IsUnicode(true);
            });
        }

        private void ConfigurePlugins(in ModelBuilder builder)
        {
            builder.Entity<AbpPlugins>(d =>
            {
                d.ToTable(HomeSystemConsts.DbTablePrefix + "Plugins", HomeSystemConsts.DbSchema);

                d.Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(true)
                .HasComment("模組名稱");

                d.Property(p => p.Path)
                .HasMaxLength(2048)
                .IsRequired(true)
                .IsUnicode(true)
                .HasComment("載入路徑");

                d.Property(p => p.Disabled)
                .HasDefaultValue(false)
                .HasComment("啟用/停用");

                d.ConfigureByConvention();
            });
        }

        private void ConfigureCalendar(in ModelBuilder builder)
        {
            builder.Entity<PersonalCalendar>(
                           d =>
                           {
                               d.ToTable(HomeSystemConsts.DbTablePrefix + "_Calendars", HomeSystemConsts.DbSchema);

                               d.Property(p => p.allDay)
                               .IsRequired(true);

                               d.Property(p => p.classNames)
                               .IsUnicode(true);

                               d.Property(p => p.description)
                               .IsUnicode(true);

                               d.Property(p => p.durationEditable)
                               .IsRequired();

                               d.Property(p => p.editable)
                               .IsRequired();

                               d.Property(p => p.EventId)
                               .HasMaxLength(256)
                               .IsUnicode(true);

                               d.Property(p => p.groupId)
                               .HasMaxLength(256)
                               .IsUnicode(true);

                               d.Property(p => p.icon)
                               .HasMaxLength(50)
                               .IsUnicode(true);

                               d.Property(p => p.IsAdded)
                               .IsRequired();

                               d.Property(p => p.resourceEditable)
                                .IsRequired();

                               d.Property(p => p.startEditable)
                               .IsRequired();

                               d.Property(p => p.title)
                               .HasMaxLength(500)
                               .IsUnicode(true)
                               .IsRequired(true);

                               d.Property(p => p.t_end)
                               .HasMaxLength(20)
                               .IsUnicode(true);

                               d.Property(p => p.t_start)
                               .HasMaxLength(20)
                               .IsUnicode(true);

                               d.Property(p => p.url)
                               .IsUnicode(true);

                               d.ConfigureByConvention();
                           });
        }
    }
}
