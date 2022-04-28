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
            bool isExists = await _fileStoreAppService.IsExistsAsync(id);

            if (!isExists)
                return NotFound();

            var fs = await _fileStoreAppService.GetAsync(id);

            if (fs != null)
            {
                return File(fs.Blob.FileContent, fs.MIMETypes.ContentType, fs.Name + fs.ExtName);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
