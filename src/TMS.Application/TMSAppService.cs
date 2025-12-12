using TMS.Localization;
using Volo.Abp.Application.Services;

namespace TMS;

/* Inherit your application services from this class.
 */
public abstract class TMSAppService : ApplicationService
{
    protected TMSAppService()
    {
        LocalizationResource = typeof(TMSResource);
    }
}
