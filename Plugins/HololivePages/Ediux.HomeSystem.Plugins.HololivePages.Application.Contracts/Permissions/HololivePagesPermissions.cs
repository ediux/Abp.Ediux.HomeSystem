using Volo.Abp.Reflection;

namespace Ediux.HomeSystem.Plugins.HololivePages.Permissions
{
    public class HololivePagesPermissions
    {
        public const string GroupName = "HololivePages";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(HololivePagesPermissions));
        }
    }
}