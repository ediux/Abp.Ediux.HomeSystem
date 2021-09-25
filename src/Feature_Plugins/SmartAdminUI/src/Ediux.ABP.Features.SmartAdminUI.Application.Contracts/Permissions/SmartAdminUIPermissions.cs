using Volo.Abp.Reflection;

namespace Ediux.ABP.Features.SmartAdminUI.Permissions
{
    public class SmartAdminUIPermissions
    {
        public const string GroupName = "SmartAdminUI";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(SmartAdminUIPermissions));
        }
    }
}