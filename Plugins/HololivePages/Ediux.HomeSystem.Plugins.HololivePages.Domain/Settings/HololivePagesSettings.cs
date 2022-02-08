namespace Ediux.HomeSystem.Plugins.HololivePages.Settings
{
    public static class HololivePagesSettings
    {
        public const string GroupName = "HololivePages";

        /* Add constants for setting names. Example:
         * public const string MySettingName = GroupName + ".MySettingName";
         */

        public const string ConnectionStringSetting = SharedSettingsConsts.PluginsDatabaseConnectionPrefix + HololivePagesDbProperties.ConnectionStringName;
    }
}