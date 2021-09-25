using Volo.Abp.Settings;

namespace Ediux.HomeSystem.Settings
{
    public class HomeSystemSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(HomeSystemSettings.MySetting1));
            context.Add(new SettingDefinition(HomeSystemSettings.Root_SmartSettings_Features_AppHeader, bool.TrueString));
            context.Add(new SettingDefinition(HomeSystemSettings.Root_SmartSettings_Theme_Role, "Administrator"));
        }
    }
}
