using TMS.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace TMS.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class TMSController : AbpControllerBase
{
    protected TMSController()
    {
        LocalizationResource = typeof(TMSResource);
    }
}
