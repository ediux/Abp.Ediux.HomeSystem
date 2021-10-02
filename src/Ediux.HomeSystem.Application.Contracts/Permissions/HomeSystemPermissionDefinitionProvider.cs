using Ediux.HomeSystem.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Ediux.HomeSystem.Permissions
{
    public class HomeSystemPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(HomeSystemPermissions.GroupName);
            //Define your own permissions here. Example:
            //myGroup.AddPermission(HomeSystemPermissions.MyPermission1, L("Permission:MyPermission1"));
            myGroup.AddPermission(HomeSystemPermissions.ProductKeysBook, L(HomeSystemResource.Permissions.ProductKeysBook));                
            myGroup.AddPermission(HomeSystemPermissions.PasswordBook, L(HomeSystemResource.Permissions.PasswordBook));
            myGroup.AddPermission(HomeSystemPermissions.DocumentsList, L(HomeSystemResource.Permissions.DocumentsList));
            myGroup.AddPermission(HomeSystemPermissions.Home, L(HomeSystemResource.Permissions.Home));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<HomeSystemResource>(name);
        }
    }
}
