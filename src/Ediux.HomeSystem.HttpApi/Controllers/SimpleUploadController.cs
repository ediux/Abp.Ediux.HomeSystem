using Ediux.HomeSystem.Models.SimpleUpload;
using Ediux.HomeSystem.SimpleUpload;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading.Tasks;
using Volo.Abp.Content;
using Volo.CmsKit.Admin.MediaDescriptors;

namespace Ediux.HomeSystem.Controllers
{
    [ApiController]
    [Route("api/simpleupload")]
    public class SimpleUploadController : HomeSystemController
    {
        protected readonly ISimpleUploadAppService SimpleUploadAppService;

        public SimpleUploadController(ISimpleUploadAppService simpleUploadAppService)
        {
            SimpleUploadAppService = simpleUploadAppService;
        }

        [HttpGet]
        [Route("download/{id}")]
        public async virtual Task<RemoteStreamContent> GetAsync(Guid id)
        {
            return await SimpleUploadAppService.DownloadAsync(id);
        }

        [HttpPost("upload/{entityType}")]
        [RequestFormLimits(MultipartBodyLengthLimit = 104857600)]
        [IgnoreAntiforgeryToken]
        public async virtual Task<IActionResult> CreateAsync(string entityType, [FromForm] IFormFile upload)
        {
            try
            {
                if (string.IsNullOrEmpty(entityType))
                    entityType = "Default";

                var result = await SimpleUploadAppService.CreateAsync(entityType, new CreateMediaInputWithStream()
                {
                    File = new RemoteStreamContent(upload.OpenReadStream(), upload.FileName),
                    Name = upload.Name
                });

                return Json(new SimpleUploadResponse() { url = $"/api/simpleupload/download/{result.Id}" });
            }
            catch (Exception ex)
            {
                return Json(new { error = new { message = ex.Message } });
            }
        }
    }
}
