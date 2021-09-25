using Ediux.ABP.Features.SmartAdminUI.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Ediux.ABP.Features.SmartAdminUI.Permissions
{
    public class SmartAdminUIPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(SmartAdminUIPermissions.GroupName, L("Permission:SmartAdminUI"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<SmartAdminUIResource>(name);
        }
    }
}