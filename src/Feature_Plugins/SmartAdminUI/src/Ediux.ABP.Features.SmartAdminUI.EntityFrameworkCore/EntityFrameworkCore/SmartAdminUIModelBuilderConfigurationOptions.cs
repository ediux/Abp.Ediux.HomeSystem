using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Ediux.ABP.Features.SmartAdminUI.EntityFrameworkCore
{
    public class SmartAdminUIModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public SmartAdminUIModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}