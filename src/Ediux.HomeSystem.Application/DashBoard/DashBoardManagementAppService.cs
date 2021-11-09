using Ediux.HomeSystem.Data;
using Ediux.HomeSystem.Models.DTOs.DashBoard;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Volo.Abp;
using Volo.Abp.Domain.Repositories;

namespace Ediux.HomeSystem.DashBoard
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

        public async Task UpdateDashboardGlobalAsync(DashboardWidgetRequestedDTOs input)
        {
            if (input != null)
            {
                var selectedWidgets = (await DashboardWidgets.GetListAsync()).Where(w => input.SelectedDefaultWidgets.Contains(w.Name)).ToList();

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
                var myWidgets = (await DashboardWidgetUsers.GetQueryableAsync())
                     .Where(w => w.Id == CurrentUser.Id)
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

        public async Task<DashBoardWidgetOptionDTOs> GetCurrentUserDashboardWidgetsAsync()
        {
            if (CurrentUser.IsAuthenticated)
            {
                var myWidgets = (await DashboardWidgetUsers.GetListAsync(p => p.Id == CurrentUser.Id, includeDetails: true))
                .Select(s => ObjectMapper.Map<DashboardWidgets, DashBoardWidgetsDTO>(s.DashboardWidget))
                .ToList();

                return new DashBoardWidgetOptionDTOs() { Widgets = myWidgets.ToArray() };
            }
            else
            {
                var myWidgets = (await DashboardWidgets.GetListAsync(p => p.IsDefault, includeDetails: true))
                   .Select(s => ObjectMapper.Map<DashboardWidgets, DashBoardWidgetsDTO>(s))
                   .ToList();

                return new DashBoardWidgetOptionDTOs() { Widgets = myWidgets.ToArray() };
            }

        }

        public async Task<DashBoardWidgetsDTO> WidgetRegistrationAsync(DashBoardWidgetsDTO input)
        {
            DashboardWidgets widget = await DashboardWidgets.FindAsync(p => p.Name == input.Name || p.Id == input.Id);

            if (widget == null)
            {
                return ObjectMapper.Map<DashboardWidgets, DashBoardWidgetsDTO>(
                    await DashboardWidgets.InsertAsync(ObjectMapper.Map<DashBoardWidgetsDTO, DashboardWidgets>(input), autoSave: true));
            }

            return ObjectMapper.Map<DashboardWidgets, DashBoardWidgetsDTO>(widget);
        }

        public async Task<DashBoardWidgetsDTO> UpdateWidgetRegistrationAsync(DashBoardWidgetsDTO updated)
        {
            return ObjectMapper.Map<DashboardWidgets, DashBoardWidgetsDTO>(
                await DashboardWidgets.UpdateAsync(ObjectMapper.Map<DashBoardWidgetsDTO, DashboardWidgets>(updated)));
        }

        public async Task WidgetUnRegistrationAsync(DashBoardWidgetsDTO input)
        {
            await DashboardWidgets.DeleteAsync(p => p.Id == input.Id);
        }

        public async Task AddDashboardWidgetToCurrentUserAsync(DashBoardWidgetsDTO input)
        {
            DashboardWidgetUsers user = await DashboardWidgetUsers.FindAsync(p => p.Id == CurrentUser.Id && p.DashboardWidgetId == input.Id);

            if (user == null)
            {
                user = new DashboardWidgetUsers(CurrentUser.Id.Value, input.Id);
                await DashboardWidgetUsers.InsertAsync(user);
            }
        }

        public async Task RemoveDashboardWidgetFromCurrentUserAasync(DashBoardWidgetsDTO input)
        {
            await DashboardWidgetUsers.DeleteAsync(p => p.Id == CurrentUser.Id && p.DashboardWidgetId == input.Id, autoSave: true);
        }

        public async Task<DashBoardWidgetOptionDTOs> GetAvailableDashboardWidgetsAsync()
        {
            return new DashBoardWidgetOptionDTOs()
            {
                Widgets = (await DashboardWidgets.GetListAsync(includeDetails: true))
                    .Select(s => ObjectMapper.Map<DashboardWidgets, DashBoardWidgetsDTO>(s))
                    .ToArray()
            };
        }
    }
}
