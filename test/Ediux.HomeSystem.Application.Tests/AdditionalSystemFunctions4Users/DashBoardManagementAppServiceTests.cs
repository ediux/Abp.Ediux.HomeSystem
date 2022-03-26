using Xunit;
using Ediux.HomeSystem.AdditionalSystemFunctions4Users;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Identity;
using Volo.Abp.Users;

namespace Ediux.HomeSystem.AdditionalSystemFunctions4Users.Tests
{
    public class DashBoardManagementAppServiceTests : HomeSystemApplicationTestBase
    {
        private readonly IDashBoardManagementAppService dashBoardManagementAppService;
        private readonly ICurrentUser currentUser;
        public DashBoardManagementAppServiceTests()
        {
            dashBoardManagementAppService = GetRequiredService<IDashBoardManagementAppService>();
            currentUser = GetRequiredService<ICurrentUser>();   
        }

        [Fact()]
        public void UpdateDashboardGlobalAsyncTest()
        {

        }

        [Fact()]
        public async Task GetDashboardWidgetListsAsyncTest()
        {
            var result = await dashBoardManagementAppService.GetDashboardWidgetListsAsync();
            result.ShouldNotBeEmpty();
        }

        [Fact()]
        public async Task GetCurrentUserDashboardWidgetsAsyncTest()
        {
          
            DashBoardWidgetOptionDto result = await dashBoardManagementAppService.GetCurrentUserDashboardWidgetsAsync();
            result.ShouldNotBeNull();
            result.Widgets.ShouldNotBeEmpty();
        }

        [Fact()]
        public void WidgetRegistrationAsyncTest()
        {

        }

        [Fact()]
        public void UpdateWidgetRegistrationAsyncTest()
        {

        }

        [Fact()]
        public void WidgetUnRegistrationAsyncTest()
        {

        }

        [Fact()]
        public void AddDashboardWidgetToCurrentUserAsyncTest()
        {

        }

        [Fact()]
        public void RemoveDashboardWidgetFromCurrentUserAasyncTest()
        {

        }

        [Fact()]
        public void GetAvailableDashboardWidgetsAsyncTest()
        {

        }
    }
}