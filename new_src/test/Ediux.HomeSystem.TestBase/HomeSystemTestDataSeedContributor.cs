using Ediux.HomeSystem.SystemManagement;


using System;
using System.Linq;
using System.Threading.Tasks;

using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace Ediux.HomeSystem
{
    public class HomeSystemTestDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        protected readonly ICurrentTenant _currentTenant;
        protected readonly IGuidGenerator _guidGenerator;
        protected readonly IRepository<DashboardWidgets> _dashboardWidgets;
        protected readonly IRepository<DashboardWidgetUsers> _dashboardWidgetUsers;
        private readonly IRepository<IdentityUser, Guid> _appUserRepository;

        public HomeSystemTestDataSeedContributor(ICurrentTenant currentTenant,
            IGuidGenerator guidGenerator,
            IRepository<DashboardWidgets> dashboardWidgets,
            IRepository<DashboardWidgetUsers> dashboardWidgetUsers,
            IRepository<IdentityUser, Guid> appUserRepository)
        {
            _currentTenant = currentTenant;
            _guidGenerator = guidGenerator;
            _appUserRepository = appUserRepository;
            _dashboardWidgets = dashboardWidgets;
            _dashboardWidgetUsers = dashboardWidgetUsers;
        }

        [UnitOfWork]
        public async Task SeedAsync(DataSeedContext context)
        {
            /* Seed additional test data... */
            using (_currentTenant.Change(context?.TenantId))
            {
                await CreateDashboardDataAsync();
            }
        }

        public async Task CreateDashboardDataAsync()
        {
            var adminUser = (await _appUserRepository.GetQueryableAsync())
                    .Where(u => u.UserName == "admin")
                    .FirstOrDefault();

            var demo = new DashboardWidgets()
            {
                AllowMulti = false,
                DisplayName = "Test Widget",
                HasOption = false,
                IsDefault = false,
                Name = "Test_Widget",
                Order = 0,
            };
            demo = await _dashboardWidgets.InsertAsync(demo);
            demo.AssginedUsers.Add(new DashboardWidgetUsers()
            {
                DashboardWidgetId = demo.Id,
                DashboardWidget = demo,
                User = adminUser,
                UserId = adminUser.Id
            });

        }

    }
}