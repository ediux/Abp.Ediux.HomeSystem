using Ediux.HomeSystem.ApplicationPluginsManager;
using Ediux.HomeSystem.Models.DTOs.PluginModule;
using Ediux.HomeSystem.Web.Models.PluginManager;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ediux.HomeSystem.Web.Pages.PluginsManager
{
    public class UpdateModel : HomeSystemPageModel
    {
        private readonly IApplicationPluginsManager _applicationPluginsManager;
        private readonly IWebHostEnvironment hostEnvironment;

        public List<SelectListItem> ModuleEnabledOptions
        {
            get
            {
                return new List<SelectListItem> {
                    new SelectListItem { Value = bool.FalseString, Text = "±Ò¥Î"},
                    new SelectListItem { Value = bool.TrueString, Text = "°±¥Î"},
                };
            }
        }

        [BindProperty]
        public PluginManagerUpdatedViewModel ViewModel { get; set; }

        public UpdateModel(IApplicationPluginsManager applicationPluginsManager, IWebHostEnvironment hostEnvironment)
        {
            _applicationPluginsManager = applicationPluginsManager;
            this.hostEnvironment = hostEnvironment;
            ViewModel = new PluginManagerUpdatedViewModel() { HostEnvironment = hostEnvironment };
        }

        public async Task<IActionResult> OnGetAsync(Guid Id)
        {
            var entity = await _applicationPluginsManager.GetAsync(Id);

            if (entity == null)
                return NotFound();

            ViewModel = ObjectMapper.Map<PluginModuleDTO, PluginManagerUpdatedViewModel>(entity);
            ViewModel.HostEnvironment = hostEnvironment;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                ViewModel.HostEnvironment = hostEnvironment;
                ViewModel.ScanAndMoveFile();
                var Dto = ObjectMapper.Map<PluginManagerUpdatedViewModel, PluginModuleDTO>(ViewModel);
                await _applicationPluginsManager.UpdateAsync(ViewModel.Id, Dto);
                return new OkObjectResult(ViewModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("",ex.Message);
                return BadRequest(new { error = new { message = ex.Message } });
            }
          
        }
    }
}
