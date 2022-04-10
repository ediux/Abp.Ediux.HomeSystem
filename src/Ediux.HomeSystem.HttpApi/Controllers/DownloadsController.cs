using Ediux.HomeSystem.SystemManagement;

using Microsoft.AspNetCore.Mvc;

using System;
using System.IO;
using System.Threading.Tasks;

namespace Ediux.HomeSystem.Controllers
{
    public class DownloadsController : HomeSystemController
    {

        private readonly IFileStoreAppService _fileStoreAppService;

        public DownloadsController(IFileStoreAppService fileStoreAppService)
        {
            _fileStoreAppService = fileStoreAppService;
        }
       
        [HttpGet]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var file = await _fileStoreAppService.GetAsync(id);

            if (file == null)
                return NotFound();

            var fs = await _fileStoreAppService.GetStreamAsync(new MediaDescriptorDto() { Id = id });

            return File(fs.GetAllBytes(), file.MIMETypes.ContentType, file.Name + file.MIMETypes.RefenceExtName);
        }
    }
}
