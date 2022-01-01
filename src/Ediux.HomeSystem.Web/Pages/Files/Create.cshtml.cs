using Ediux.HomeSystem.Files;
using Ediux.HomeSystem.Models.DTOs.Files;
using Ediux.HomeSystem.Web.Models.Files;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Ediux.HomeSystem.Web.Pages.Files
{
    public class CreateModel : HomeSystemPageModel
    {
        private readonly IFileStoreAppService _fileStoreAppService;
        [BindProperty]
        public FileUploadViewModel ViewModel { get; set; }

        public CreateModel(IFileStoreAppService fileStoreAppService)
        {
            _fileStoreAppService = fileStoreAppService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (ViewModel.UploadFile != null)
                    {
                        FileStoreDTO entityDto = ObjectMapper.Map<FileUploadViewModel, FileStoreDTO>(ViewModel);

                        entityDto.OriginFullPath = ViewModel.UploadFile.FileName;

                        if (entityDto.Name.IsNullOrWhiteSpace())
                        {
                            entityDto.Name = Path.GetFileNameWithoutExtension(ViewModel.UploadFile.FileName).Trim();
                        }
                        if (entityDto.ExtName.IsNullOrWhiteSpace())
                        {
                            entityDto.ExtName = Path.GetExtension(ViewModel.UploadFile.FileName).Trim();
                        }

                        entityDto.FileContent = ViewModel.UploadFile.OpenReadStream().GetAllBytes();
                        entityDto.Size = ViewModel.UploadFile.Length;

                        await _fileStoreAppService.CreateAsync(entityDto);
                    }

                    return RedirectToPage("/Files/Index");
                }

                ModelState.AddModelError("", "µo¥Í¿ù»~!");
                return Page();
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return Page();
            }

        }
    }
}
