using Microsoft.Extensions.Localization;
using TMS.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace TMS;

[Dependency(ReplaceServices = true)]
public class TMSBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<TMSResource> _localizer;

    public TMSBrandingProvider(IStringLocalizer<TMSResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
