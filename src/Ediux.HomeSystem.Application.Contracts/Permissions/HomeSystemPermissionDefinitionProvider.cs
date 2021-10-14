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
            var pProductKeysBook = myGroup.AddPermission(HomeSystemPermissions.ProductKeysBook.Prefix, L(HomeSystemResource.Permissions.ProductKeysBook.Prefix));
            pProductKeysBook.AddChild(HomeSystemPermissions.ProductKeysBook.CreateNew, L(HomeSystemResource.Permissions.ProductKeysBook.CreateNew));
            pProductKeysBook.AddChild(HomeSystemPermissions.ProductKeysBook.Delete, L(HomeSystemResource.Permissions.ProductKeysBook.Delete));
            pProductKeysBook.AddChild(HomeSystemPermissions.ProductKeysBook.Execute, L(HomeSystemResource.Permissions.ProductKeysBook.Execute));
            pProductKeysBook.AddChild(HomeSystemPermissions.ProductKeysBook.Lists, L(HomeSystemResource.Permissions.ProductKeysBook.Lists));
            pProductKeysBook.AddChild(HomeSystemPermissions.ProductKeysBook.Modify, L(HomeSystemResource.Permissions.ProductKeysBook.Modify));
            pProductKeysBook.AddChild(HomeSystemPermissions.ProductKeysBook.Special, L(HomeSystemResource.Permissions.ProductKeysBook.Special));

            var pPasswordBook = myGroup.AddPermission(HomeSystemPermissions.PasswordBook.Prefix, L(HomeSystemResource.Permissions.PasswordBook.Prefix));
            pPasswordBook.AddChild(HomeSystemPermissions.PasswordBook.CreateNew, L(HomeSystemResource.Permissions.PasswordBook.CreateNew));
            pPasswordBook.AddChild(HomeSystemPermissions.PasswordBook.Delete, L(HomeSystemResource.Permissions.PasswordBook.Delete));
            pPasswordBook.AddChild(HomeSystemPermissions.PasswordBook.Execute, L(HomeSystemResource.Permissions.PasswordBook.Execute));
            pPasswordBook.AddChild(HomeSystemPermissions.PasswordBook.Lists, L(HomeSystemResource.Permissions.PasswordBook.Lists));
            pPasswordBook.AddChild(HomeSystemPermissions.PasswordBook.Modify, L(HomeSystemResource.Permissions.PasswordBook.Modify));
            pPasswordBook.AddChild(HomeSystemPermissions.PasswordBook.Special, L(HomeSystemResource.Permissions.PasswordBook.Special));

            var pDocs= myGroup.AddPermission(HomeSystemPermissions.Docs.Prefix, L(HomeSystemResource.Permissions.Docs.Prefix));
            pDocs.AddChild(HomeSystemPermissions.Docs.CreateNew, L(HomeSystemResource.Permissions.Docs.CreateNew));
            pDocs.AddChild(HomeSystemPermissions.Docs.Delete, L(HomeSystemResource.Permissions.Docs.Delete));
            pDocs.AddChild(HomeSystemPermissions.Docs.Execute, L(HomeSystemResource.Permissions.Docs.Execute));
            pDocs.AddChild(HomeSystemPermissions.Docs.Lists, L(HomeSystemResource.Permissions.Docs.Lists));
            pDocs.AddChild(HomeSystemPermissions.Docs.Modify, L(HomeSystemResource.Permissions.Docs.Modify));
            pDocs.AddChild(HomeSystemPermissions.Docs.Special, L(HomeSystemResource.Permissions.Docs.Special));

            var pSettings = myGroup.AddPermission(HomeSystemPermissions.Settings.Prefix, L(HomeSystemResource.Permissions.Settings.Prefix));
            pSettings.AddChild(HomeSystemPermissions.Settings.Execute, L(HomeSystemResource.Permissions.Settings.Execute));
            pSettings.AddChild(HomeSystemPermissions.Settings.Special, L(HomeSystemResource.Permissions.Settings.Special));

            var pHome = myGroup.AddPermission(HomeSystemPermissions.Home.Prefix, L(HomeSystemResource.Permissions.Home.Prefix));
            pHome.AddChild(HomeSystemPermissions.Home.CreateNew, L(HomeSystemResource.Permissions.Home.CreateNew));
            pHome.AddChild(HomeSystemPermissions.Home.Delete, L(HomeSystemResource.Permissions.Home.Delete));
            pHome.AddChild(HomeSystemPermissions.Home.Execute, L(HomeSystemResource.Permissions.Home.Execute));
            pHome.AddChild(HomeSystemPermissions.Home.Lists, L(HomeSystemResource.Permissions.Home.Lists));
            pHome.AddChild(HomeSystemPermissions.Home.Modify, L(HomeSystemResource.Permissions.Home.Modify));
            pHome.AddChild(HomeSystemPermissions.Home.Special, L(HomeSystemResource.Permissions.Home.Special));

            var pPluginManasers = myGroup.AddPermission(HomeSystemPermissions.PluginsManager.Prefix, L(HomeSystemResource.Permissions.PluginsManager.Prefix));
            pPluginManasers.AddChild(HomeSystemPermissions.PluginsManager.CreateNew, L(HomeSystemResource.Permissions.PluginsManager.CreateNew));
            pPluginManasers.AddChild(HomeSystemPermissions.PluginsManager.Delete, L(HomeSystemResource.Permissions.PluginsManager.Delete));
            pPluginManasers.AddChild(HomeSystemPermissions.PluginsManager.Execute, L(HomeSystemResource.Permissions.PluginsManager.Execute));
            pPluginManasers.AddChild(HomeSystemPermissions.PluginsManager.Lists, L(HomeSystemResource.Permissions.PluginsManager.Lists));
            pPluginManasers.AddChild(HomeSystemPermissions.PluginsManager.Modify, L(HomeSystemResource.Permissions.PluginsManager.Modify));
            pPluginManasers.AddChild(HomeSystemPermissions.PluginsManager.Special, L(HomeSystemResource.Permissions.PluginsManager.Special));

            var pMIME = myGroup.AddPermission(HomeSystemPermissions.MIMETypeManager.Prefix, L(HomeSystemResource.Permissions.MIMETypesManager));
            pMIME.AddChild(HomeSystemPermissions.MIMETypeManager.CreateNew, L(HomeSystemResource.Permissions.SubAction.CreateNew));
            pMIME.AddChild(HomeSystemPermissions.MIMETypeManager.Delete, L(HomeSystemResource.Permissions.SubAction.Delete));
            pMIME.AddChild(HomeSystemPermissions.MIMETypeManager.Execute, L(HomeSystemResource.Permissions.SubAction.Execute));
            pMIME.AddChild(HomeSystemPermissions.MIMETypeManager.Lists, L(HomeSystemResource.Permissions.SubAction.Lists));
            pMIME.AddChild(HomeSystemPermissions.MIMETypeManager.Modify, L(HomeSystemResource.Permissions.SubAction.Modify));
            pMIME.AddChild(HomeSystemPermissions.MIMETypeManager.Special, L(HomeSystemResource.Permissions.SubAction.Special));

            var pPersonalCalendar = myGroup.AddPermission(HomeSystemPermissions.PersonalCalendar.Prefix, L(HomeSystemResource.Permissions.PersonalCalendar));
            pPersonalCalendar.AddChild(HomeSystemPermissions.PersonalCalendar.CreateNew, L(HomeSystemResource.Permissions.SubAction.CreateNew));
            pPersonalCalendar.AddChild(HomeSystemPermissions.PersonalCalendar.Delete, L(HomeSystemResource.Permissions.SubAction.Delete));
            pPersonalCalendar.AddChild(HomeSystemPermissions.PersonalCalendar.Execute, L(HomeSystemResource.Permissions.SubAction.Execute));
            pPersonalCalendar.AddChild(HomeSystemPermissions.PersonalCalendar.Lists, L(HomeSystemResource.Permissions.SubAction.Lists));
            pPersonalCalendar.AddChild(HomeSystemPermissions.PersonalCalendar.Modify, L(HomeSystemResource.Permissions.SubAction.Modify));
            pPersonalCalendar.AddChild(HomeSystemPermissions.PersonalCalendar.Special, L(HomeSystemResource.Permissions.SubAction.Special));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<HomeSystemResource>(name);
        }
    }
}
