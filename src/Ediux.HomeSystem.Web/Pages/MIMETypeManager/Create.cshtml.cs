using Ediux.HomeSystem.MIMETypeManager;
using Ediux.HomeSystem.Models.DTOs.MIMETypes;
using Ediux.HomeSystem.Web.Models.MIMETypeManager;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ediux.HomeSystem.Web.Pages.MIMETypeManager
{
    public class CreateModel : HomeSystemPageModel
    {
        private readonly IMIMETypeManagerAppService _mimeTypeManagerAppService;

        [BindProperty]
        public MIMETypeManagerCreateModel ViewModel { get; set; }

        public CreateModel(IMIMETypeManagerAppService mIMETypeManagerAppService)
        {
            _mimeTypeManagerAppService = mIMETypeManagerAppService;
            ViewModel = new MIMETypeManagerCreateModel();
        }

        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await _mimeTypeManagerAppService.CreateAsync(ObjectMapper.Map<MIMETypeManagerCreateModel, MIMETypesDTO>(ViewModel));

            return RedirectToPage("/MIMETypeManager/Index");
        }
    }
}
