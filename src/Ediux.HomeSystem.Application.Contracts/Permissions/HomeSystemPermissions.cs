using static Ediux.HomeSystem.Localization.HomeSystemResource.Permissions;

namespace Ediux.HomeSystem.Permissions
{
    public static class HomeSystemPermissions
    {
        public const string GroupName = "HomeSystem";

        //Add your own permission names. Example:
        //public const string MyPermission1 = GroupName + ".MyPermission1";
        //public const string Home = GroupName + ".HomePage";

        public const string ProductKeysBook = GroupName + ".ProductKeysBook";

        public const string PasswordBook = GroupName + ".PasswordBook";

        public const string DocumentsList = GroupName + ".Documents";

        public class Home
        {
            public const string Preifx = GroupName + ".HomePage";

            public static string Execute = getSubPremission(Preifx, SubActionPermission.Execute);
        }

        public static string getSubPremission(string name, SubActionPermission subPermission)
        {
            
            switch (subPermission)
            {
                case SubActionPermission.Create:
                    return name.TrimEnd('.') + SubAction.CreateNew;
                case SubActionPermission.Delete:
                    return name.TrimEnd('.') + SubAction.Delete;
                case SubActionPermission.Execute:
                    return name.TrimEnd('.') + SubAction.Execute;
                case SubActionPermission.Lists:
                    return name.TrimEnd('.') + SubAction.Lists;
                case SubActionPermission.Modify:
                    return name.TrimEnd('.') + SubAction.Modify;
                case SubActionPermission.Special:
                    return name.TrimEnd('.') + SubAction.Special;
                default:
                    return name.TrimEnd('.');
            }

        }

        public class SubAction
        {
            public const string Execute = ".Executed";
            public const string CreateNew = ".CreateNew";
            public const string Lists = ".List";
            public const string Modify = ".Modify";
            public const string Delete = ".Delete";
            public const string Special = ".Special";
        }


       
    }
}