using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace Ediux.ABP.Features.SmartAdminUI.MongoDB
{
    public class SmartAdminUIMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public SmartAdminUIMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}