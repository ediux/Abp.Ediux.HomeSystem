
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

using System.IO;

namespace Ediux.HomeSystem.Plugins.HololivePages.EntityFrameworkCore
{
    public class HololivePagesDbContextFactory : IDesignTimeDbContextFactory<HololivePagesDbContext>
    {
        public HololivePagesDbContext CreateDbContext(string[] args)
        {
            //HomeSystemEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<HololivePagesDbContext>()
                .UseSqlServer(configuration.GetConnectionString(HololivePagesDbProperties.ConnectionStringName));

            return new HololivePagesDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Ediux.HomeSystem.Plugins.HololivePages.EntityFrameworkCore/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
