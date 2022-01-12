using System;
using Ediux.HomeSystem.Plugins.HololivePages.Data;
using Ediux.HomeSystem.Plugins.HololivePages.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Ediux.HomeSystem.Plugins.HololivePages.EntityFrameworkCore
{
    public static class HololivePagesDbContextModelCreatingExtensions
    {
        public static void ConfigureHololivePages(
            this ModelBuilder builder,
            Action<HololivePagesModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new HololivePagesModelBuilderConfigurationOptions(
                HololivePagesDbProperties.DbTablePrefix,
                HololivePagesDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);
            Create_Hololive_Groups(builder, options);
            Create_Hololive_Companies(builder, options);
            Create_Hololive_Branches(builder, options);
            Create_Hololive_Departments(builder, options);
            Create_Hololive_Members(builder, options);
            Create_Hololive_MemberEvents(builder, options);
            Create_Hololive_PhotosRefence(builder, options);
            Create_Hololive_YTuberVideoRefence(builder, options);
            /* Configure all entities here. Example:

            builder.Entity<Question>(b =>
            {
                //Configure table & schema name
                b.ToTable(options.TablePrefix + "Questions", options.Schema);
            
                b.ConfigureByConvention();
            
                //Properties
                b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);
                
                //Relations
                b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

                //Indexes
                b.HasIndex(q => q.CreationTime);
            });
            */
        }

        private static void Create_Hololive_Groups(ModelBuilder builder, HololivePagesModelBuilderConfigurationOptions options)
        {
            builder.Entity<Groups>(b =>
            {
                b.ToTable(options.TablePrefix + "Groups", options.Schema);

                b.ConfigureByConvention();

                b.Property(p => p.Description)
                .IsUnicode(true)
                .HasMaxLength(2048);

                b.Property(p => p.GroupRepresentative)
                .HasMaxLength(20)
                .IsUnicode()
                .IsRequired();

                b.Property(p => p.Name)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();

                b.Property(p => p.RegistrationDate)
                .IsRequired();


            });
        }

        private static void Create_Hololive_Companies(ModelBuilder builder, HololivePagesModelBuilderConfigurationOptions options)
        {
            builder.Entity<Company>(b =>
            {
                b.ToTable(options.TablePrefix + "Companies", options.Schema);

                b.ConfigureByConvention();

                b.Property(p => p.Area)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();

                b.Property(p => p.CEO)
                .HasMaxLength(20)
                .IsUnicode()
                .IsRequired();

                b.Property(p => p.CompanyRepresentative)
                .HasMaxLength(20)
                .IsUnicode()
                .IsRequired();

                b.Property(p => p.Description)
                .IsUnicode(true)
                .HasMaxLength(2048);

                b.Property(p => p.Name)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();

                b.HasOne(p => p.Groups)
                .WithMany(m => m.Companies)
                .HasForeignKey(fk => fk.GroupId);

                b.Navigation(b => b.Groups)
                .UsePropertyAccessMode(PropertyAccessMode.Property);

            });
        }

        private static void Create_Hololive_Branches(ModelBuilder builder, HololivePagesModelBuilderConfigurationOptions options)
        {
            builder.Entity<Branches>(b =>
            {
                b.ToTable(options.TablePrefix + "Branches", options.Schema);

                b.ConfigureByConvention();

                b.Property(p => p.Description)
                .IsUnicode(true)
                .HasMaxLength(2048);

                b.Property(p => p.Name)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();

                b.HasOne(p => p.Company)
                .WithMany(p => p.Branches)
                .HasForeignKey(fk => fk.CompanyId);

                b.Navigation(b => b.Company)
               .UsePropertyAccessMode(PropertyAccessMode.Property);
            });
        }

        private static void Create_Hololive_Departments(ModelBuilder builder, HololivePagesModelBuilderConfigurationOptions options)
        {
            builder.Entity<Departments>(b =>
            {
                b.ToTable(options.TablePrefix + "Departments", options.Schema);

                b.ConfigureByConvention();

                b.Property(p => p.Description)
                .IsUnicode(true)
                .HasMaxLength(2048);

                b.Property(p => p.Name)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();

                b.HasOne(p => p.Branch)
                .WithMany(p => p.Departments)
                .HasForeignKey(fk => fk.BranchId);

                b.Navigation(b => b.Branch)
                .UsePropertyAccessMode(PropertyAccessMode.Property);
            });
        }

        private static void Create_Hololive_Members(ModelBuilder builder, HololivePagesModelBuilderConfigurationOptions options)
        {
            builder.Entity<Members>(b =>
            {
                b.ToTable(options.TablePrefix + "Members", options.Schema);

                b.ConfigureByConvention();

                b.Property(p => p.ChannelFeatures)
                .IsUnicode(true)
                .HasMaxLength(2048);

                b.Property(p => p.Comment)
                .IsUnicode(true)
                .HasMaxLength(8192);

                b.Property(p => p.Name)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();

                b.Property(p => p.SureName)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();

                b.Property(p => p.MiddleName)
                .HasMaxLength(50)
                .IsUnicode();

                b.Property(p => p.JanpaneseName)
                .HasMaxLength(50)
                .IsUnicode();

                b.Property(p => p.ChineseName)
                .HasMaxLength(50)
                .IsUnicode();

                b.Property(p => p.RoleDescription)
                .HasMaxLength(50)
                .IsUnicode();

                b.Property(p => p.Live2DDesigner)
                .HasMaxLength(50)
                .IsUnicode();

                b.Property(p => p.Live3DDesigner)
                .HasMaxLength(50)
                .IsUnicode();

                b.Property(p => p.VTOperatorExpertise)
                .HasMaxLength(50)
                .IsUnicode();

                var converter = new EnumToNumberConverter<Sex,int>();
                b.Property(p => p.Sex).HasConversion(converter);

                b.HasOne(p => p.Department)
                .WithMany(p => p.Members)
                .HasForeignKey(fk => fk.DepartmentsId);

                b.Navigation(b => b.Department)
                .UsePropertyAccessMode(PropertyAccessMode.Property);
            });
        }

        private static void Create_Hololive_MemberEvents(ModelBuilder builder, HololivePagesModelBuilderConfigurationOptions options)
        {
            builder.Entity<MemberEvents>(b =>
            {
                b.ToTable(options.TablePrefix + "MemberEvents", options.Schema);

                b.ConfigureByConvention();

                b.Property(p => p.Name)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();

                b.Property(p => p.Description)
                .HasMaxLength(500)
                .IsUnicode()
                .IsRequired();

                b.HasOne(p => p.Member)
                .WithMany(p => p.MemberEvents)
                .HasForeignKey(fk => fk.MemberId);

                b.Navigation(b => b.Member)
                .UsePropertyAccessMode(PropertyAccessMode.Property);
            });
        }

        private static void Create_Hololive_PhotosRefence(ModelBuilder builder, HololivePagesModelBuilderConfigurationOptions options)
        {
            builder.Entity<PhotosRefence>(b =>
            {
                b.ToTable(options.TablePrefix + "PhotosRefence", options.Schema);

                b.HasKey(p => new { p.MemberId, p.FileId });

                b.ConfigureByConvention();

                b.HasOne(p => p.Member)
                .WithMany(p => p.Photos)
                .HasForeignKey(fk => fk.MemberId);

                b.Navigation(b => b.Member)
                .UsePropertyAccessMode(PropertyAccessMode.Property);
            });
        }
        private static void Create_Hololive_YTuberVideoRefence(ModelBuilder builder, HololivePagesModelBuilderConfigurationOptions options)
        {
            builder.Entity<YTuberVideoRefence>(b =>
            {
                b.ToTable(options.TablePrefix + "YTuberVideoRefence", options.Schema);

                b.ConfigureByConvention();

                b.Property(p => p.Title)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();

                b.Property(p => p.Description)
                .HasMaxLength(500)
                .IsUnicode()
                .IsRequired();

                b.Property(p => p.Url)
               .HasMaxLength(2048)
               .IsUnicode()
               .IsRequired();

                b.HasOne(p => p.Member)
                .WithMany(p => p.YouTuberVideos)
                .HasForeignKey(fk => fk.MemberId);
                
                b.Navigation(b => b.Member)
                .UsePropertyAccessMode(PropertyAccessMode.Property);
            });
        }
    }
}