
using Ediux.HomeSystem.SystemManagement;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Volo.Abp;
using Volo.Abp.Domain.Repositories;

namespace Ediux.HomeSystem.AdditionalSystemFunctions4Users
{
    [RemoteService(Name = "dashboardManagement")]
    public class DashBoardManagementAppService : HomeSystemAppService, IDashBoardManagementAppService
    {
        protected IRepository<DashboardWidgets> DashboardWidgets { get; }
        protected IRepository<DashboardWidgetUsers> DashboardWidgetUsers { get; }

        public DashBoardManagementAppService(IRepository<DashboardWidgets> dashboardWidgetsRepo, IRepository<DashboardWidgetUsers> dashboardWidgetUsersRepo)
        {
            DashboardWidgets = dashboardWidgetsRepo;
            DashboardWidgetUsers = dashboardWidgetUsersRepo;
        }

        public async Task UpdateDashboardGlobalAsync(AbpSearchRequestDto input)
        {
            if (input != null)
            {
                var selectedWidgets = (await DashboardWidgets.WithDetailsAsync(p => p.AssginedUsers))
                    .Where(w => input.Search.Contains(w.Name)).ToList();

                if (selectedWidgets.Any())
                {
                    foreach (var widget in selectedWidgets)
                    {
                        widget.IsDefault = true;
                    }

                    await DashboardWidgets.UpdateManyAsync(selectedWidgets, autoSave: true);
                }
            }
        }

        public async Task<List<string>> GetDashboardWidgetListsAsync()
        {
            if (CurrentUser.IsAuthenticated)
            {
                var myWidgets = (await DashboardWidgetUsers.WithDetailsAsync())
                     .Where(w => w.UserId == CurrentUser.Id)
                     .Select(s => s.DashboardWidget.Name)
                     .ToList();

                return myWidgets;
            }
            else
            {
                var myWidgets = (await DashboardWidgets.GetQueryableAsync())
                    .Where(w => w.IsDefault == true)
                    .Select(s => s.Name)
                    .ToList();
                return myWidgets;
            }
        }

        public async Task<DashBoardWidgetOptionDto> GetCurrentUserDashboardWidgetsAsync()
        {
            if (CurrentUser.IsAuthenticated)
            {
                var myWidgets = (await DashboardWidgetUsers.WithDetailsAsync())
                    .OrderBy(o => o.DashboardWidget.Order)
                    .Select(s => ObjectMapper.Map<DashboardWidgets, DashBoardWidgetsDto>(s.DashboardWidget))
                    .ToList();

                return new DashBoardWidgetOptionDto() { Widgets = myWidgets.ToArray() };
            }
            else
            {
                var myWidgets = (await DashboardWidgets.WithDetailsAsync())
                    .OrderBy(o => o.Order)
                   .Select(s => ObjectMapper.Map<DashboardWidgets, DashBoardWidgetsDto>(s))
                   .ToList();

                return new DashBoardWidgetOptionDto() { Widgets = myWidgets.ToArray() };
            }

        }

        public async Task<DashBoardWidgetsDto> WidgetRegistrationAsync(DashBoardWidgetsDto input)
        {
            DashboardWidgets widget = await DashboardWidgets.FindAsync(p => p.Name == input.Name || p.Id == input.Id);

            if (widget == null)
            {
                return ObjectMapper.Map<DashboardWidgets, DashBoardWidgetsDto>(
                    await DashboardWidgets.InsertAsync(ObjectMapper.Map<DashBoardWidgetsDto, DashboardWidgets>(input), autoSave: true));
            }

            return ObjectMapper.Map<DashboardWidgets, DashBoardWidgetsDto>(widget);
        }

        public async Task<DashBoardWidgetsDto> UpdateWidgetRegistrationAsync(DashBoardWidgetsDto updated)
        {
            return ObjectMapper.Map<DashboardWidgets, DashBoardWidgetsDto>(
                await DashboardWidgets.UpdateAsync(ObjectMapper.Map<DashBoardWidgetsDto, DashboardWidgets>(updated)));
        }

        public async Task WidgetUnRegistrationAsync(DashBoardWidgetsDto input)
        {
            await DashboardWidgets.DeleteAsync(p => p.Id == input.Id);
        }

        public async Task AddDashboardWidgetToCurrentUserAsync(DashBoardWidgetsDto input)
        {
            DashboardWidgetUsers user = await DashboardWidgetUsers.FindAsync(p => p.UserId == CurrentUser.Id && p.DashboardWidgetId == input.Id);

            if (user == null)
            {
                user = new DashboardWidgetUsers(CurrentUser.Id.Value, input.Id);
                await DashboardWidgetUsers.InsertAsync(user);
            }
        }

        public async Task RemoveDashboardWidgetFromCurrentUserAasync(DashBoardWidgetsDto input)
        {
            await DashboardWidgetUsers.DeleteAsync(p => p.UserId == CurrentUser.Id && p.DashboardWidgetId == input.Id, autoSave: true);
        }

        public async Task<DashBoardWidgetOptionDto> GetAvailableDashboardWidgetsAsync()
        {
            return new DashBoardWidgetOptionDto()
            {
                Widgets = (await DashboardWidgets.GetListAsync(includeDetails: true))
                    .Select(s => ObjectMapper.Map<DashboardWidgets, DashBoardWidgetsDto>(s))
                    .ToArray()
            };
        }
    }
}
