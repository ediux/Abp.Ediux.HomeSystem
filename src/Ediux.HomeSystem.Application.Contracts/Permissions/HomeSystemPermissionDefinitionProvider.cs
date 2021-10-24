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
            myGroup.AddPermission(HomeSystemPermissions.ProductKeysBook.Prefix, L(HomeSystemResource.Permissions.ProductKeysBook.Prefix))
                .AddAllSubPermission();            

            myGroup.AddPermission(HomeSystemPermissions.PasswordBook.Prefix, L(HomeSystemResource.Permissions.PasswordBook.Prefix))
                .AddAllSubPermission();            

            myGroup.AddPermission(HomeSystemPermissions.Docs.Prefix, L(HomeSystemResource.Permissions.Docs.Prefix))
                .AddAllSubPermission();

            myGroup.AddPermission(HomeSystemPermissions.Settings.Prefix, L(HomeSystemResource.Permissions.Settings.Prefix))
                .AddReadOnly();

            myGroup.AddPermission(HomeSystemPermissions.Home.Prefix, L(HomeSystemResource.Permissions.Home.Prefix))
                .AddAllSubPermission();

            myGroup.AddPermission(HomeSystemPermissions.PluginsManager.Prefix, L(HomeSystemResource.Permissions.PluginsManager.Prefix))
                .AddAllSubPermission();

            myGroup.AddPermission(HomeSystemPermissions.MIMETypeManager.Prefix, L(HomeSystemResource.Permissions.MIMETypesManager))
                .AddAllSubPermission();

            myGroup.AddPermission(HomeSystemPermissions.PersonalCalendar.Prefix, L(HomeSystemResource.Permissions.PersonalCalendar))
                .AddAllSubPermission();            
        }


        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<HomeSystemResource>(name);
        }
    }
}
