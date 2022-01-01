using Ediux.HomeSystem.Files;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace Ediux.HomeSystem.Web.Pages.Files
{
    public class IndexModel : HomeSystemPageModel
    {
        private readonly IFileStoreAppService _fileStoreAppService;

        public IndexModel(IFileStoreAppService fileStoreAppService)
        {
            _fileStoreAppService = fileStoreAppService;
        }
        
        public async Task<IActionResult> OnGetAsync(Guid? Id)
        {
            try
            {
                if (Id.HasValue)
                {
                    var item = await _fileStoreAppService.DownloadAsync(Id.Value);
                    return File(item.GetStream(), item.ContentType,item.FileName);
                }

                return Page();
            }
            catch (Exception ex)
            {

                return BadRequest(new { error = new { message = ex.Message } });
            }
        }
    }
}
