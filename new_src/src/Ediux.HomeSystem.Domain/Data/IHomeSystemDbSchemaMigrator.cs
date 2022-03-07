using System.Threading.Tasks;

namespace Ediux.HomeSystem.Data
{
    public interface IHomeSystemDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
