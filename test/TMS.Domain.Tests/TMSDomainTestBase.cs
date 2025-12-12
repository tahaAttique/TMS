using Volo.Abp.Modularity;

namespace TMS;

/* Inherit from this class for your domain layer tests. */
public abstract class TMSDomainTestBase<TStartupModule> : TMSTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
