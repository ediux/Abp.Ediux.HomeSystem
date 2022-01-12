using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Ediux.HomeSystem.Plugins.HololivePages.EntityFrameworkCore
{
    public class HololivePagesModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public HololivePagesModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}