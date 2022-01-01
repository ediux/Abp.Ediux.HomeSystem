using Ediux.HomeSystem.Localization;
using Ediux.HomeSystem.Permissions;

using System;
using System.Collections.Generic;
using System.Text;

using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Ediux.HomeSystem
{
    public static class PermissionExtensions
    {
        public static PermissionDefinition AddAllSubPermission(this PermissionDefinition definition)
        {
            definition.AddChild(definition.Name + HomeSystemPermissions.SubAction.CreateNew, L(HomeSystemResource.Permissions.SubAction.CreateNew));
            definition.AddChild(definition.Name + HomeSystemPermissions.SubAction.Delete, L(HomeSystemResource.Permissions.SubAction.Delete));
            definition.AddChild(definition.Name + HomeSystemPermissions.SubAction.Execute, L(HomeSystemResource.Permissions.SubAction.Execute))
                .AddChild(definition.Name + HomeSystemPermissions.SubAction.Special, L(HomeSystemResource.Permissions.SubAction.Special));
            definition.AddChild(definition.Name + HomeSystemPermissions.SubAction.Lists, L(HomeSystemResource.Permissions.SubAction.Lists));
            definition.AddChild(definition.Name + HomeSystemPermissions.SubAction.Modify, L(HomeSystemResource.Permissions.SubAction.Modify));
            
            definition.AddChild(definition.Name + HomeSystemPermissions.SubAction.Widget, L(HomeSystemResource.Permissions.SubAction.Widget));
            definition.AddChild(definition.Name + HomeSystemPermissions.SubAction.Options, L(HomeSystemResource.Permissions.SubAction.Options));
            return definition;
        }

        public static PermissionDefinition AddExecutedOnly(this PermissionDefinition definition)
        {
            definition.AddChild(definition.Name + HomeSystemPermissions.SubAction.Execute, L(HomeSystemResource.Permissions.SubAction.Execute));
            definition.AddChild(definition.Name + HomeSystemPermissions.SubAction.Widget, L(HomeSystemResource.Permissions.SubAction.Widget));
            definition.AddChild(definition.Name + HomeSystemPermissions.SubAction.Options, L(HomeSystemResource.Permissions.SubAction.Options));
            return definition;
        }

        public static PermissionDefinition AddReadOnly(this PermissionDefinition definition)
        {
            definition.AddChild(definition.Name + HomeSystemPermissions.SubAction.Execute, L(HomeSystemResource.Permissions.SubAction.Execute))
              .AddChild(definition.Name + HomeSystemPermissions.SubAction.Special, L(HomeSystemResource.Permissions.SubAction.Special));
            definition.AddChild(definition.Name + HomeSystemPermissions.SubAction.Widget, L(HomeSystemResource.Permissions.SubAction.Widget));
            definition.AddChild(definition.Name + HomeSystemPermissions.SubAction.Options, L(HomeSystemResource.Permissions.SubAction.Options));
            return definition;
        }

        public static PermissionDefinition AddListingOnly(this PermissionDefinition definition)
        {
            definition.AddChild(definition.Name + HomeSystemPermissions.SubAction.Execute, L(HomeSystemResource.Permissions.SubAction.Execute))
                .AddChild(definition.Name + HomeSystemPermissions.SubAction.Special, L(HomeSystemResource.Permissions.SubAction.Special));
            definition.AddChild(definition.Name + HomeSystemPermissions.SubAction.Lists, L(HomeSystemResource.Permissions.SubAction.Lists));
            return definition;
        }

        public static PermissionDefinition AddReadsAndModify(this PermissionDefinition definition)
        {
            definition.AddChild(definition.Name + HomeSystemPermissions.SubAction.CreateNew, L(HomeSystemResource.Permissions.SubAction.CreateNew));
            definition.AddChild(definition.Name + HomeSystemPermissions.SubAction.Modify, L(HomeSystemResource.Permissions.SubAction.Modify));
            definition.AddChild(definition.Name + HomeSystemPermissions.SubAction.Execute, L(HomeSystemResource.Permissions.SubAction.Execute))
                .AddChild(definition.Name + HomeSystemPermissions.SubAction.Special, L(HomeSystemResource.Permissions.SubAction.Special));
            definition.AddChild(definition.Name + HomeSystemPermissions.SubAction.Lists, L(HomeSystemResource.Permissions.SubAction.Lists));
            definition.AddChild(definition.Name + HomeSystemPermissions.SubAction.Widget, L(HomeSystemResource.Permissions.SubAction.Widget));
            definition.AddChild(definition.Name + HomeSystemPermissions.SubAction.Options, L(HomeSystemResource.Permissions.SubAction.Options));
            return definition;
        }

        public static PermissionDefinition AddExport(this PermissionDefinition definition)
        {
            definition.AddChild(definition.Name + HomeSystemPermissions.SubAction.Export, L(HomeSystemResource.Permissions.SubAction.Export));
            return definition;
        }
        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<HomeSystemResource>(name);
        }
    }
}
