using Volo.Abp.Modularity;

namespace TMS;

[DependsOn(
    typeof(TMSDomainModule),
    typeof(TMSTestBaseModule)
)]
public class TMSDomainTestModule : AbpModule
{

}
