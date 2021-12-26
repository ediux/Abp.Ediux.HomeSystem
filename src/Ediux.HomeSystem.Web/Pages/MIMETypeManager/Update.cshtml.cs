using Ediux.HomeSystem.MIMETypeManager;
using Ediux.HomeSystem.Models.DTOs.MIMETypes;
using Ediux.HomeSystem.Web.Models.MIMETypeManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace Ediux.HomeSystem.Web.Pages.MIMETypeManager
{
    public class UpdateModel : HomeSystemPageModel
    {
        private readonly IMIMETypeManagerAppService _mimeTypeManagerAppService;

        [BindProperty]
        public MIMETypeManagerUpdateModel ViewModel { get; set; }

        public UpdateModel(IMIMETypeManagerAppService mIMETypeManagerAppService)
        {
            _mimeTypeManagerAppService = mIMETypeManagerAppService;
            ViewModel = new MIMETypeManagerUpdateModel();
        }

        public async Task<IActionResult> OnGetAsync(int? Id)
        {
            var entity = await _mimeTypeManagerAppService.GetAsync(Id.Value);

            if (entity != null)
            {
                ViewModel = ObjectMapper.Map<MIMETypesDTO, MIMETypeManagerUpdateModel>(entity);                
                return Page();
            }

            return NotFound();
        }
        public async Task<IActionResult> OnPostAsync(int Id)
        {
            try
            {
                var Dto = ObjectMapper.Map<MIMETypeManagerUpdateModel, MIMETypesDTO>(ViewModel);
                await _mimeTypeManagerAppService.UpdateAsync(Id, Dto);
                return RedirectToPage("/MIMETypeManager/Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return Page();
            }
        }
    }
}
