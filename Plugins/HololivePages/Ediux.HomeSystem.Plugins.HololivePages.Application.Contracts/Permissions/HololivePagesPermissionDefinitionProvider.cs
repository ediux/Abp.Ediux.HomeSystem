using Ediux.HomeSystem.Plugins.HololivePages.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Ediux.HomeSystem.Plugins.HololivePages.Permissions
{
    public class HololivePagesPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(HololivePagesPermissions.GroupName, L("Permission:HololivePages"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<HololivePagesResource>(name);
        }
    }
}