using Ediux.HomeSystem.AdditionalSystemFunctions4Users;
using Ediux.HomeSystem.Features.Blogs;
using Ediux.HomeSystem.Features.CMS;
using Ediux.HomeSystem.Features.Common;
using Ediux.HomeSystem.SystemManagement;

using Microsoft.EntityFrameworkCore;

using System;

using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Ediux.HomeSystem
{
    public static class HomeSystemDbContextModelBuilderExtensions
    {
        public static void ConfigureSystemManagement(this ModelBuilder builder)
        {
            builder.Entity<AbpPlugins>(b =>
            {
                b.ToTable(HomeSystemConsts.DbTablePrefix + "Plugins", HomeSystemConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(p => p.Name)
                .HasMaxLength(1024)
                .IsUnicode()
                .IsRequired();

                b.Property(p => p.AssemblyName)
                .HasMaxLength(2048)
                .IsUnicode()
                .IsRequired();

                b.Property(p => p.Disabled)
                .HasDefaultValue(true)
                .IsRequired();

                b.HasMany(p => p.DependencyAssembly)
                  .WithOne()
                  .HasForeignKey("Id")
                  .IsRequired(false);


            });

            builder.Entity<DashboardWidgets>(b =>
            {
                b.ToTable(HomeSystemConsts.DbTablePrefix + "DashboardWidgets", HomeSystemConsts.DbSchema);

                b.Property(p => p.Name)
                .HasMaxLength(512)
                .IsUnicode()
                .IsRequired();

                b.Property(p => p.DisplayName)
                .HasMaxLength(1024)
                .IsUnicode()
                .IsRequired();

                b.Property(p => p.HasOption)
                .HasDefaultValue(false)
                .IsRequired();

                b.Property(p => p.AllowMulti)
                .HasDefaultValue(false)
                .IsRequired();

                b.Property(p => p.IsDefault)
                .HasDefaultValue(false)
                .IsRequired();

                b.Property(p => p.Order)
                .HasDefaultValue(0)
                .IsRequired();
            });

            builder.Entity<DashboardWidgetUsers>(b =>
            {
                b.ToTable(HomeSystemConsts.DbTablePrefix + "DashboardWidgetUsers", HomeSystemConsts.DbSchema);
                b.ConfigureByConvention();
                b.HasKey(p => new { p.UserId, p.DashboardWidgetId });

                b.HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(sc => sc.UserId);

                b.HasOne(p => p.DashboardWidget)
                .WithMany(p => p.AssginedUsers)
                .HasForeignKey(p => p.DashboardWidgetId);

            });

            builder.Entity<File_Store>(b =>
            {
                b.ToTable(HomeSystemConsts.DbTablePrefix + "Files", HomeSystemConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(p => p.Name)
                .HasMaxLength(512)
                .IsUnicode(true)
                .IsRequired();

                b.Property(p => p.Size)
                .HasDefaultValue(0)
                .IsRequired();

                b.Property(p => p.IsPublic)
                .HasDefaultValue(false)
                .IsRequired();

                b.Property(p => p.BlobContainerName)
                .HasMaxLength(2048)
                .IsUnicode()
                .IsRequired();

                b.HasOne(p => p.Plugins)
                .WithOne(p => p.RefFileInstance)
                .HasForeignKey<AbpPlugins>(p => p.RefFileStoreId);

                b.HasOne(p => p.Classification)
                .WithMany(p => p.Files)
                .HasForeignKey(p => p.FileClassificationId);

                b.HasOne(p => p.MIME)
                .WithMany(p => p.Files)
                .HasForeignKey(p => p.MIMETypeId);

            });

            builder.Entity<FileStoreClassification>(b =>
            {
                b.ToTable(HomeSystemConsts.DbTablePrefix + "Classifications", HomeSystemConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(p => p.Name)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();

                b.HasMany(p => p.Childs)
                .WithOne(p => p.Parent)
                .HasForeignKey(p => p.ParentClassificationId)
                .IsRequired(false);
            });

            builder.Entity<MIMEType>(b =>
            {
                b.ToTable(HomeSystemConsts.DbTablePrefix + "MIMETypes", HomeSystemConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(p => p.TypeName)
                .HasMaxLength(256)
                .HasDefaultValue("application/octet-stream")
                .IsUnicode()
                .IsRequired();

                b.Property(p => p.RefenceExtName)
               .HasMaxLength(256)
               .IsUnicode(true)
               .IsRequired();

                b.Property(p => p.Description)
                .HasMaxLength(2048)
                .IsUnicode(true);


                b.HasMany(p => p.Files)
                .WithOne(p => p.MIME)
                .HasForeignKey(x => x.MIMETypeId)
                .IsRequired();
            });

            builder.Entity<InternalSystemMessages>(b =>
            {
                b.ToTable(HomeSystemConsts.DbTablePrefix + "SystemMessages", HomeSystemConsts.DbSchema);
                b.ConfigureByConvention();
                b.HasOne(p => p.From)
                .WithMany()
                .HasForeignKey(fk => fk.FromUserId);

                b.Property(p => p.Subject)
                .HasMaxLength(1024)
                .IsUnicode()
                .IsRequired();

                b.Property(p => p.Message)
                .HasMaxLength(8192)
                .IsUnicode()
                .IsRequired();

                b.Property(p => p.IsEMail)
                .HasDefaultValue(false)
                .IsRequired();

                b.Property(p => p.IsPush)
                .HasDefaultValue(false)
                .IsRequired();

                b.Property(p => p.SendTime)
                .HasDefaultValue(DateTime.UtcNow)
                .IsRequired();

                b.Property(p => p.IsRead)
                .HasDefaultValue(false)
                .IsRequired();

                b.Property(p => p.ActionCallbackURL)
                .HasMaxLength(2048)
                .IsUnicode();

                b.Property(p => p.IsReply)
                .HasDefaultValue(false)
                .IsRequired();

                b.HasOne(p => p.RefenceMessage)
                .WithOne()
                .HasForeignKey<InternalSystemMessages>(p => p.RefenceMessageId)
                .IsRequired(false);

            });

            builder.Entity<AttachFile>(b =>
            {
                b.ToTable(HomeSystemConsts.DbTablePrefix + "AttachFiles", HomeSystemConsts.DbSchema);
                b.ConfigureByConvention();
                b.HasKey(p => new { p.SystemMessageId, p.FileStoreId });

                b.HasOne(p => p.SystemMessage)
                .WithMany(p => p.AttachFiles)
                .HasForeignKey(p => p.SystemMessageId);

                b.HasOne(p => p.File)
                .WithMany(p => p.RefencedByMessages)
                .HasForeignKey(p => p.FileStoreId);
            });

            builder.Entity<MenuItems>(b =>
            {
                b.ToTable(HomeSystemConsts.DbTablePrefix + nameof(MenuItems), HomeSystemConsts.DbSchema);
                b.ConfigureByConvention();

                b.Property(p => p.DisplayName)
                .HasMaxLength(64)
                .IsUnicode()
                .IsRequired();

                b.Property(p => p.IsActive)
                .IsRequired();

                b.Property(p => p.Url)
                .HasMaxLength(1024)
                .IsUnicode()
                .IsRequired();

                b.Property(p => p.Icon)
                .IsUnicode()
                .IsRequired(false);

                b.Property(p => p.Order)
                .HasDefaultValue(0)
                .IsRequired();

                b.Property(p => p.Target)
                .IsUnicode()
                .IsRequired(false);

                b.Property(p => p.ElementId)
                .IsUnicode()
                .IsRequired(false);

                b.Property(p => p.CssClass)
                .IsUnicode()
                .IsRequired(false);

                b.HasOne(p => p.Page)
                .WithOne(p => p.MenuItem)
                .HasForeignKey<MenuItems>(fk => fk.PageId);

                b.HasOne(p => p.Tenant)
                .WithMany()
                .HasForeignKey(fk => fk.TenantId);

                b.HasMany(p => p.SubMenuItems)
                .WithOne()
                .HasForeignKey(fk => fk.ParentId);
            });
        }

        public static void ConfigureAdditionalSystemFunctions4Users(this ModelBuilder builder)
        {
            builder.Entity<PersonalCalendar>(b =>
            {
                //Calendars
                b.ToTable(HomeSystemConsts.DbTablePrefix + "Calendars", HomeSystemConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(p => p.IsAllDay)
                .IsRequired();

                b.Property(p => p.StartTime)
               .IsRequired();

                b.Property(p => p.Color)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();

                b.Property(p => p.StartTime)
                .IsRequired();

                b.HasOne(p => p.SystemMessages)
                .WithMany()
                .HasForeignKey(fk => fk.SystemMessageId)
                .IsRequired(false);

                b.HasOne(p => p.RefenceEvent)
                .WithMany()
                .HasForeignKey(fk => fk.RefenceEventId)
                .IsRequired(false);

            });

            builder.Entity<ProductKeys>(b =>
            {
                b.ToTable(HomeSystemConsts.DbTablePrefix + "ProductKeyStores", HomeSystemConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(p => p.ProductName)
                .HasMaxLength(256)
                .IsUnicode()
                .IsRequired();

                b.Property(p => p.ProductKey)
                .HasMaxLength(256)
                .IsUnicode()
                .IsRequired();

                b.Property(p => p.Shared)
                .HasDefaultValue(false)
                .IsRequired();

            });

            builder.Entity<UserPasswordStore>(b =>
            {
                b.ToTable(HomeSystemConsts.DbTablePrefix + "UserPasswordStores", HomeSystemConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(p => p.Account)
                .HasMaxLength(256)
                .IsUnicode(true);

                b.Property(p => p.IsHistory)
                .IsRequired();


                b.Property(p => p.Password)
                .HasMaxLength(256)
                .IsUnicode(true)
                .IsRequired();

                b.Property(p => p.Site)
                .HasMaxLength(2048)
                .IsUnicode(true);

                b.Property(p => p.SiteName)
                .HasMaxLength(50)
                .IsUnicode(true)
                .IsRequired();
            });
        }

        public static void ConfigureBlogs(this ModelBuilder builder)
        {
            builder.Entity<Blogs>(b =>
            {
                b.ToTable(HomeSystemConsts.DbTablePrefix + nameof(Blogs), HomeSystemConsts.DbSchema);
                b.ConfigureByConvention();

                b.Property(p => p.Name)
                .HasMaxLength(64)
                .IsUnicode()
                .IsRequired();

                b.Property(p => p.Slug)
                .HasMaxLength(64)
                .IsUnicode()
                .IsRequired();

                b.HasOne(p => p.Tenant)
                .WithMany()
                .HasForeignKey(b => b.TenantId);
            });

            builder.Entity<BlogPosts>(b =>
            {
                b.ToTable(HomeSystemConsts.DbTablePrefix + nameof(BlogPosts), HomeSystemConsts.DbSchema);
                b.ConfigureByConvention();

                b.HasOne(p => p.Blog)
                .WithMany(p => p.Posts)
                .HasForeignKey(fk => fk.BlogId);

                b.Property(p => p.BlogId)
                .IsRequired();

                b.Property(p => p.Title)
                .HasMaxLength(64)
                .IsUnicode()
                .IsRequired();

                b.Property(p => p.Slug)
                .HasMaxLength(256)
                .IsUnicode()
                .IsRequired();

                b.Property(p => p.ShortDescription)
                .HasMaxLength(256)
                .IsUnicode();

                b.Property(p => p.Content)
                .IsUnicode();

                b.HasOne(p => p.Author)
                .WithMany()
                .HasForeignKey(fk => fk.AuthorId)
                .IsRequired();

                b.HasOne(p => p.CoverImageMedia)
                .WithMany()
                .HasForeignKey(fk => fk.CoverImageMediaId);

                b.HasOne(p => p.Tenant)
               .WithMany()
               .HasForeignKey(b => b.TenantId);
            });

            builder.Entity<Comments>(b =>
            {
                b.ToTable(HomeSystemConsts.DbTablePrefix + nameof(Comments), HomeSystemConsts.DbSchema);
                b.ConfigureByConvention();

                b.HasOne(p => p.Tenant)
                .WithMany()
                .HasForeignKey(fk => fk.TenantId);

                b.Property(p => p.EntityType)
               .HasMaxLength(64)
               .IsUnicode()
               .IsRequired();

                b.Property(p => p.EntityId)
               .HasMaxLength(64)
               .IsUnicode()
               .IsRequired();

                b.Property(p => p.Text)
               .HasMaxLength(64)
               .IsUnicode()
               .IsRequired();

                b.HasOne(p => p.RepliedComment)
                .WithMany()
                .HasForeignKey(fk => fk.RepliedCommentId);
            });

            builder.Entity<EntityTags>(b =>
            {
                b.ToTable(HomeSystemConsts.DbTablePrefix + nameof(EntityTags), HomeSystemConsts.DbSchema);
                b.ConfigureByConvention();

                b.HasKey(pk => new { pk.TagId, pk.EntityId });


                b.HasOne(p => p.Tags)
                .WithMany(p => p.Entities)
                .HasForeignKey(fk => fk.TagId);

                b.HasOne(p => p.Tenant)
               .WithMany()
               .HasForeignKey(fk => fk.TenantId);
            });

            builder.Entity<Tags>(b =>
            {
                b.ToTable(HomeSystemConsts.DbTablePrefix + nameof(Tags), HomeSystemConsts.DbSchema);
                b.ConfigureByConvention();

                b.Property(p => p.EntityType)
                .HasMaxLength(64)
                .IsUnicode()
                .IsRequired();

                b.Property(p => p.Name)
                .HasMaxLength(32)
                .IsUnicode()
                .IsRequired();

                b.HasOne(p => p.Tenant)
                .WithMany()
                .HasForeignKey(fk => fk.TenantId);
            });

            builder.Entity<Ratings>(b =>
            {
                b.ToTable(HomeSystemConsts.DbTablePrefix + nameof(Ratings), HomeSystemConsts.DbSchema);
                b.ConfigureByConvention();

                b.Property(p => p.EntityType)
                .HasMaxLength(64)
                .IsUnicode()
                .IsRequired();

                b.Property(p => p.EntityId)
                .HasMaxLength(64)
                .IsUnicode()
                .IsRequired();

                b.Property(p => p.StarCount)
                .HasDefaultValue(0)
                .IsRequired();

                b.HasOne(p => p.Tenant)
                .WithMany()
                .HasForeignKey(fk => fk.TenantId);
            });

        }

        public static void ConfigureCMS(this ModelBuilder builder)
        {
            builder.Entity<Pages>(b =>
            {
                b.ToTable(HomeSystemConsts.DbTablePrefix + nameof(Pages), HomeSystemConsts.DbSchema);
                b.ConfigureByConvention();

                b.Property(p => p.Title)
                .HasMaxLength(256)
                .IsUnicode()
                .IsRequired();

                b.Property(p => p.Slug)
                .HasMaxLength(256)
                .IsUnicode()
                .IsRequired();

                b.Property(p => p.Content)
                .IsUnicode()
                .IsRequired(false);

                b.Property(p => p.Script)
                .IsUnicode()
                .IsRequired(false);

                b.Property(p => p.Style)
                .IsUnicode()
                .IsRequired(false);

                b.HasOne(p => p.Tenant)
              .WithMany()
              .HasForeignKey(fk => fk.TenantId);
            });
        }

        public static void ConfigureCommons(this ModelBuilder builder)
        {
            builder.Entity<Consortiums>(b =>
            {
                b.ToTable(HomeSystemConsts.DbTablePrefix + nameof(Consortiums), HomeSystemConsts.DbSchema);
                b.ConfigureByConvention();

                b.Property(p => p.Name)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();

                b.Property(p => p.GroupRepresentative)
                .HasMaxLength(20)
                .IsUnicode()
                .IsRequired();

                b.Property(p => p.Description)
                .HasMaxLength(2048)
                .IsUnicode();

                b.HasOne(p => p.LogoFile)
                .WithMany(p => p.Consortiums)
                .HasForeignKey(p => p.LogoFileRef);

            });
        }
    }
}
