using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.Data
{
    /* This is used if database provider does't define
     * IHomeSystemDbSchemaMigrator implementation.
     */
    public class NullHomeSystemDbSchemaMigrator : IHomeSystemDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}