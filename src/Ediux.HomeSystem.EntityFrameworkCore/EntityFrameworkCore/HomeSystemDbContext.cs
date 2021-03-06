using Ediux.HomeSystem.AdditionalSystemFunctions4Users;
using Ediux.HomeSystem.Features.Blogs;
using Ediux.HomeSystem.Features.CMS;
using Ediux.HomeSystem.Features.Common;
using Ediux.HomeSystem.SystemManagement;

using Microsoft.EntityFrameworkCore;

using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

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

        #region Commons
        /// <summary>
        /// 財團/集團資料表
        /// </summary>
        public DbSet<Consortiums> Consortiums { get; set; }
        #endregion

        #region Blogs
        public DbSet<BlogPosts> BlogPosts { get; set; }

        public DbSet<Blogs> Blogs { get; set; }

        public DbSet<Comments> Comments { get; set; }

        public DbSet<EntityTags> EntityTags { get; set; }

        public DbSet<Ratings> Ratings { get; set; }

        public DbSet<Tags> Tags { get; set; }
        #endregion

        #region CMS
        public DbSet<Pages> Pages { get; set; }
        #endregion

        #region 額外使用者系統功能
        public DbSet<PersonalCalendar> Calendars { get; set; }

        public DbSet<ProductKeys> ProductKeyStores { get; set; }

        public DbSet<UserPasswordStore> UserPasswordStores { get; set; }
        #endregion

        #region 系統管理
        //系統管理
        public DbSet<AbpPlugins> Plugins { get; set; }

        public DbSet<DashboardWidgets> DashboardWidgets { get; set; }

        public DbSet<DashboardWidgetUsers> DashboardWidgetUsers { get; set; }

        public DbSet<File_Store> Files { get; set; }

        public DbSet<FileStoreClassification> Classifications { get; set; }

        public DbSet<MIMEType> MIMETypes { get; set; }

        public DbSet<InternalSystemMessages> SystemMessages { get; set; }

        public DbSet<AttachFile> AttachFiles { get; set; }

        public DbSet<MenuItems> MenuItems { get; set; }
        #endregion

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

            /* Configure your own tables/entities inside here */
            builder.ConfigureSystemManagement();
            builder.ConfigureAdditionalSystemFunctions4Users();
            builder.ConfigureBlogs();
            builder.ConfigureCMS();
            builder.ConfigureCommons();
        }
    }
}
