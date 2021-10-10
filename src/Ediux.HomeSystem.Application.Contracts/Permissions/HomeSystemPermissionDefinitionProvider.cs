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
            myGroup.AddPermission(HomeSystemPermissions.ProductKeysBook, L(HomeSystemResource.Permissions.ProductKeysBook.Prefix));
            myGroup.AddPermission(HomeSystemPermissions.PasswordBook, L(HomeSystemResource.Permissions.PasswordBook.Prefix));
            myGroup.AddPermission(HomeSystemPermissions.DocumentsList, L(HomeSystemResource.Permissions.Docs.Prefix));
            myGroup.AddPermission(HomeSystemPermissions.Home.Preifx, L(HomeSystemResource.Permissions.Home.Prefix))
                .AddChild(HomeSystemPermissions.Home.Execute, L(HomeSystemResource.Permissions.Home.Execute))
                .AddChild(HomeSystemPermissions.SubAction.Lists,L(HomeSystemResource.Permissions.Home.Lists));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<HomeSystemResource>(name);
        }
    }
}
