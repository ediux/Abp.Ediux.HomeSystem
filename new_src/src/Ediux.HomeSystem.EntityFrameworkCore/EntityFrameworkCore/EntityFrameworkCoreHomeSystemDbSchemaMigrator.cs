using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ediux.HomeSystem.Data;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.EntityFrameworkCore
{
    public class EntityFrameworkCoreHomeSystemDbSchemaMigrator
        : IHomeSystemDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreHomeSystemDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the HomeSystemDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<HomeSystemDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}
