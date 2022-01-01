using Ediux.HomeSystem.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Ediux.HomeSystem.Miscellaneous
{
    public class ComponentsManagementAppService : HomeSystemAppService, IComponentsManagementAppService
    {
        protected IRepository<ComponentsRegistration> ComponentsRegistrations { get; }

        public ComponentsManagementAppService(IRepository<ComponentsRegistration> componentsRegistrationsRepo)
        {
            ComponentsRegistrations = componentsRegistrationsRepo;
        }

        public async Task CreateComponentsAsync(string Input)
        {
            ComponentsRegistration componentsRegistration = new ComponentsRegistration()
            {
                AllowUserSetting = false,
                HasOption = false,
                Name = Input
            };

            if ((await ComponentsRegistrations.FindAsync(p => p.Name == Input)) == null)
            {
               await ComponentsRegistrations.InsertAsync(componentsRegistration, autoSave: true);
            }
            
        }

        public async Task<List<string>> GetComponentsAsync()
        {
            List<string> currentUserComponents = (await ComponentsRegistrations.GetQueryableAsync())
                .Select(s=>s.Name)
                .ToList();

            return currentUserComponents;
        }

        public async Task RemoveComponentAsync(string input)
        {
            await ComponentsRegistrations.DeleteAsync(p => p.Name == input, autoSave: true);
        }

    }
}
