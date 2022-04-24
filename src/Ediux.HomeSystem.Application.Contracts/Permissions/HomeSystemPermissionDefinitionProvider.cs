using Ediux.HomeSystem.AdditionalSystemFunctions4Users;
using Ediux.HomeSystem.Localization;
using Ediux.HomeSystem.Options;

using Microsoft.Extensions.Options;

using System.Linq;

using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Ediux.HomeSystem.Permissions
{
    public class HomeSystemPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        private readonly IOptions<DashboardWidgetOption> options;

        public HomeSystemPermissionDefinitionProvider(IOptions<DashboardWidgetOption> options)
        {
            this.options = options;
        }
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

            myGroup.AddPermission(HomeSystemPermissions.Files.Prefix, L(HomeSystemResource.Permissions.Files.Prefix))
                .AddAllSubPermission()
                .AddExport();

            myGroup.AddPermission(HomeSystemPermissions.Photos.Prefix, L(HomeSystemResource.Permissions.Photos.Prefix))
                .AddAllSubPermission()
                .AddExport();

            myGroup.AddPermission(HomeSystemPermissions.SystemMessages.Prefix, L(HomeSystemResource.Permissions.Photos.Prefix))
              .AddAllSubPermission()
              .AddExport();

            myGroup.AddPermission(HomeSystemPermissions.Blogs.Prefix, L(HomeSystemResource.Permissions.Blogs.Prefix))
             .AddAllSubPermission()
             .AddExport();

            if (options.Value.Widgets.Any())
            {
                foreach (DashBoardWidgetsDto widget in options.Value.Widgets.Values)
                {
                    if (!string.IsNullOrWhiteSpace(widget.PermissionName))
                    {
                        myGroup.AddPermission(widget.PermissionName, L(widget.PermissionName))
                            .AddAllSubPermission();
                    }
                }
            }
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<HomeSystemResource>(name);
        }
    }
}
