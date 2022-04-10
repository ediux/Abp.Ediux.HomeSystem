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
using Volo.Abp.Uow;
using Volo.Abp.Users;

namespace Ediux.HomeSystem.DataSeeder
{
    public class FileClassificationDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        protected readonly UnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<FileStoreClassification, Guid> _fileStoreClassifications;
        private readonly ICurrentTenant _currentTenant;
        private readonly ICurrentUser _currentUser;

        public FileClassificationDataSeedContributor(IRepository<FileStoreClassification, Guid> fileStoreClassifications,
           ICurrentTenant currentTenant,
           ICurrentUser currentUser,
           UnitOfWorkManager unitOfWorkManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _fileStoreClassifications = fileStoreClassifications;
            _currentTenant = currentTenant;
            _currentUser = currentUser;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            DateTime systemTime = DateTime.Now;

            using (_currentTenant.Change(context?.TenantId))
            {
                if ((await _fileStoreClassifications.FindAsync(a => a.Name == "Plugins")) == null)
                {
                    await _fileStoreClassifications.InsertAsync(new FileStoreClassification()
                    {
                        Name = "Plugins",
                        CreationTime = systemTime,
                        CreatorId = _currentUser.Id
                    });
                }

                if ((await _fileStoreClassifications.FindAsync(a => a.Name == "Photos")) == null)
                {
                    await _fileStoreClassifications.InsertAsync(new FileStoreClassification()
                    {
                        Name = "Photos",
                        CreationTime = systemTime,
                        CreatorId = _currentUser.Id
                    });
                }
                await _unitOfWorkManager.Current.SaveChangesAsync();
            }
        }
    }
}
