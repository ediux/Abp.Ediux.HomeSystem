using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ediux.HomeSystem.Data;
using Serilog;
using Volo.Abp;
using System.Linq;
using System.Reflection;
using System;
using Microsoft.Extensions.Logging;

namespace Ediux.HomeSystem.DbMigrator
{
    public class DbMigratorHostedService : IHostedService
    {
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _application;
        private readonly ILogger<DbMigratorHostedService> logger;

        public DbMigratorHostedService(IHostApplicationLifetime hostApplicationLifetime,
            IServiceProvider abpApplicationWithInternalServiceProvider,
            IConfiguration configuration,
            ILogger<DbMigratorHostedService> logger)
        {
            _hostApplicationLifetime = hostApplicationLifetime;
            _configuration = configuration;
            _application = abpApplicationWithInternalServiceProvider;
            this.logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                //using (var application = AbpApplicationFactory.Create<HomeSystemDbMigratorModule>(options =>
                //{
                //    options.Services.ReplaceConfiguration(_configuration);
                //    options.UseAutofac();
                //    options.Services.AddLogging(c => c.AddSerilog());

                //}))
                //{
                //    application.Initialize();

                await _application
                    .GetRequiredService<HomeSystemDbMigrationService>()
                    .MigrateAsync();
                

                //_application.Shutdown();

                _hostApplicationLifetime.StopApplication();
                //}

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error:" + ex.Message);
                _hostApplicationLifetime.StopApplication();
            }

        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
