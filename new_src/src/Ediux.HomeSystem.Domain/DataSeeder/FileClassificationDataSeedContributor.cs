using Ediux.HomeSystem.SystemManagement;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.MultiTenancy;

namespace Ediux.HomeSystem.DataSeeder
{
    public class FileClassificationDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<FileStoreClassification, Guid> _fileStoreClassifications;
        private readonly ICurrentTenant _currentTenant;

        public FileClassificationDataSeedContributor(IRepository<FileStoreClassification, Guid> fileStoreClassifications,
           ICurrentTenant currentTenant)
        {
            _fileStoreClassifications = fileStoreClassifications;
            _currentTenant = currentTenant;
        }
        public async Task SeedAsync(DataSeedContext context)
        {
            using (_currentTenant.Change(context?.TenantId))
            {
                await _fileStoreClassifications.InsertAsync(new FileStoreClassification()
                {
                    Name = "Root"
                });
            }
        }
    }
}
