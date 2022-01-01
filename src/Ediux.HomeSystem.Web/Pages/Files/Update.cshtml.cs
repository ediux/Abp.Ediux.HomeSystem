using Ediux.HomeSystem.Files;
using Ediux.HomeSystem.Models.DTOs.Files;
using Ediux.HomeSystem.Web.Models.Files;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Ediux.HomeSystem.Web.Pages.Files
{
    public class UpdateModel : HomeSystemPageModel
    {
        private readonly IFileStoreAppService _fileStoreAppService;
        [BindProperty]
        public FileReuploadViewModel ViewModel { get; set; }

        public UpdateModel(IFileStoreAppService fileStoreAppService)
        {
            _fileStoreAppService = fileStoreAppService;
            ViewModel = new FileReuploadViewModel();
        }

        public async Task<IActionResult> OnGetAsync(Guid Id)
        {
            try
            {
                var entity = await _fileStoreAppService.GetAsync(Id);
                if (entity != null)
                {
                    ViewModel = ObjectMapper.Map<FileStoreDTO, FileReuploadViewModel>(entity);
                    return Page();
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync(Guid Id)
        {
            try
            {
                var Dto = ObjectMapper.Map<FileReuploadViewModel, FileStoreDTO>(ViewModel);
                
                if (Dto == null)
                {
                    return NotFound();
                }

                if(ViewModel.UploadFile!= null)
                {
                    Dto.FileContent = ViewModel.UploadFile.OpenReadStream().GetAllBytes();
                    Dto.Size = ViewModel.UploadFile.Length;
                }
                
                await _fileStoreAppService.UpdateAsync(Id, Dto);

                return RedirectToPage("/Files/Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return Page();
            }
        }
    }
}
