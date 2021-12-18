using Ediux.HomeSystem.ApplicationPluginsManager;
using Ediux.HomeSystem.Models.DTOs.PluginModule;
using Ediux.HomeSystem.Web.Models.PluginManager;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Ediux.HomeSystem.Web.Pages.PluginsManager
{
    public class CreateModel : HomeSystemPageModel
    {
        private readonly IApplicationPluginsManager _applicationPluginsManager;
        private readonly IWebHostEnvironment hostEnvironment;
        [BindProperty]
        public PluginManagerCreateViewModel ViewModel { get; set; }

        public CreateModel(IApplicationPluginsManager applicationPluginsManager, IWebHostEnvironment hostEnvironment)
        {
            _applicationPluginsManager = applicationPluginsManager;
            this.hostEnvironment = hostEnvironment;
            ViewModel = new PluginManagerCreateViewModel();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ViewModel.HostEnvironment = hostEnvironment;

            if (ModelState.IsValid)
            {
                if (ViewModel.AssemblyFile != null)
                {
                    string pluginFolderPath = ViewModel.Disabled ? Path.Combine(hostEnvironment.ContentRootPath, "DisabledPlugins") : Path.Combine(hostEnvironment.ContentRootPath, "Plugins");

                    ViewModel.PluginPath = Path.Combine(pluginFolderPath, (ViewModel.AssemblyFile.FileName ?? ViewModel.AssemblyFile.Name) ?? Path.GetRandomFileName());
                    ViewModel.Name = Path.GetFileNameWithoutExtension(ViewModel.PluginPath);
                }

                PluginModuleCreateOrUpdateDTO pluginModuleDTO = ObjectMapper.Map<PluginManagerCreateViewModel, PluginModuleCreateOrUpdateDTO>(ViewModel);
                pluginModuleDTO.CreationTime = DateTime.UtcNow;
                pluginModuleDTO.CreatorId = CurrentUser.Id;

                await _applicationPluginsManager.CreateAsync(pluginModuleDTO);

                ViewModel.SaveToDisk();

                return RedirectToPage("/PluginsManager/Index");
            }

            return RedirectToPage(ViewModel);
        }
    }
}
