using Volo.Abp.Modularity;

namespace TMS;

[DependsOn(
    typeof(TMSApplicationModule),
    typeof(TMSDomainTestModule)
)]
public class TMSApplicationTestModule : AbpModule
{

}
