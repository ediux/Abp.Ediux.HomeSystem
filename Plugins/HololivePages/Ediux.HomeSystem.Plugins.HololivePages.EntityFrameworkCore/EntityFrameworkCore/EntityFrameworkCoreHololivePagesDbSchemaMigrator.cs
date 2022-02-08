using Ediux.HomeSystem.Data;
using Ediux.HomeSystem.Plugins.HololivePages.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Threading.Tasks;

using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.Plugins.HololivePages.EntityFrameworkCore
{
    public class EntityFrameworkCoreHololivePagesDbSchemaMigrator
        : IHomeSystemDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreHololivePagesDbSchemaMigrator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            await _serviceProvider
               .GetRequiredService<HololivePagesDbContext>()
               .Database
               .MigrateAsync();
        }
    }
}
