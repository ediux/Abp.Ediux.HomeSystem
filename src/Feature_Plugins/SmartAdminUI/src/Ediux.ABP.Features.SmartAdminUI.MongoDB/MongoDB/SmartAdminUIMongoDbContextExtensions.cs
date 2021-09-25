using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Ediux.ABP.Features.SmartAdminUI.MongoDB
{
    public static class SmartAdminUIMongoDbContextExtensions
    {
        public static void ConfigureSmartAdminUI(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new SmartAdminUIMongoModelBuilderConfigurationOptions(
                SmartAdminUIDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}