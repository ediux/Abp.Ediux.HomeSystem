using System;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace Ediux.ABP.Features.SmartAdminUI.EntityFrameworkCore
{
    public static class SmartAdminUIDbContextModelCreatingExtensions
    {
        public static void ConfigureSmartAdminUI(
            this ModelBuilder builder,
            Action<SmartAdminUIModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new SmartAdminUIModelBuilderConfigurationOptions(
                SmartAdminUIDbProperties.DbTablePrefix,
                SmartAdminUIDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

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
    }
}