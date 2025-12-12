using Volo.Abp.Settings;

namespace TMS.Settings;

public class TMSSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(TMSSettings.MySetting1));
    }
}
