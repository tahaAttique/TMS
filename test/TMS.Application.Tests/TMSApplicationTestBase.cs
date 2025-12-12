using Volo.Abp.Modularity;

namespace TMS;

public abstract class TMSApplicationTestBase<TStartupModule> : TMSTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
