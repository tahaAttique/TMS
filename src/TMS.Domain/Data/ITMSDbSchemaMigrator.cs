using System.Threading.Tasks;

namespace TMS.Data;

public interface ITMSDbSchemaMigrator
{
    Task MigrateAsync();
}
