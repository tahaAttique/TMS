using TMS.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace TMS.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(TMSEntityFrameworkCoreModule),
    typeof(TMSApplicationContractsModule)
)]
public class TMSDbMigratorModule : AbpModule
{
}
