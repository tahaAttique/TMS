using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace TMS.Data;

/* This is used if database provider does't define
 * ITMSDbSchemaMigrator implementation.
 */
public class NullTMSDbSchemaMigrator : ITMSDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
