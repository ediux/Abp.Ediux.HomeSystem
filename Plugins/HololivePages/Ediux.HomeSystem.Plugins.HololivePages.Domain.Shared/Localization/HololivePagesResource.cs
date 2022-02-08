using Volo.Abp.Localization;

using static Ediux.HomeSystem.Localization.HomeSystemResource;

namespace Ediux.HomeSystem.Plugins.HololivePages.Localization
{
    [LocalizationResourceName("HololivePages")]
    public class HololivePagesResource
    {
        public const string Prefix = nameof(Permissions);

        public class Permissions
        {
            public const string HololivePages = Prefix + ":" + nameof(HololivePages);
        }
       
    }
}
