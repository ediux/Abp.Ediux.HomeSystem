using Ediux.HomeSystem.SystemManagement;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Ediux.HomeSystem.Localization;

namespace Ediux.HomeSystem.Controllers
{

    public class UploadController : HomeSystemController
    {
        private readonly IFileStoreAppService _fileStoreAppService;
        private readonly IFileStoreClassificationAppService _classificationAppService;
        private readonly ILogger<UploadController> logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UploadController(IFileStoreAppService fileStoreAppService,
            IFileStoreClassificationAppService classificationAppService,
            IWebHostEnvironment webHostEnvironment,
            ILogger<UploadController> logger) : base()
        {
            _fileStoreAppService = fileStoreAppService;
            _classificationAppService = classificationAppService;
            _webHostEnvironment = webHostEnvironment;
            this.logger = logger;
        }

        [RequireHttps]
        [HttpPost]
        public async Task<ActionResult<IList<UploadResult>>> Post([FromForm] string Data, [FromForm] IEnumerable<IFormFile> files)
        {
            List<UploadResult> uploadResults = new();

            if (string.IsNullOrEmpty(Data))
            {
                uploadResults.Add(new UploadResult()
                {
                    ErrorCode = 99503,
                    ErrorMessage = L[HomeSystemDomainErrorCodes.CannotBeNullOrEmpty].Value,
                    Success = false,
                    RefenceData = null
                });

                return BadRequest(uploadResults);
            }

            List<UploadFileJSONData> details = System.Text.Json.JsonSerializer.Deserialize<List<UploadFileJSONData>>(Data);

            var resourcePath = new Uri($"{Request.Scheme}://{Request.Host}/");

            IFormFile[] formFilesArray = files.ToArray();

            foreach (var uploadDetail in details)
            {
                var uploadResult = new UploadResult();

                IFormFile processfile = formFilesArray[uploadDetail.Order];
                var untrustedFileName = processfile.FileName;

                if (processfile.FileName != uploadDetail.FileName)
                {
                    processfile = formFilesArray.Where(w => w.FileName == uploadDetail.FileName).SingleOrDefault();

                    if (processfile == null)
                    {
                        uploadResult = new UploadResult() { ErrorCode = 93004, ErrorMessage = L[HomeSystemDomainErrorCodes.API_Upload_NotFound].Value, Success = false, RefenceData = uploadDetail };
                        uploadResults.Add(uploadResult);
                        continue;
                    }

                    untrustedFileName = processfile.FileName;
                }

                if (processfile.Length == 0)
                {
                    logger.LogInformation("{FileName} length is 0 (Err: 1)",
                        untrustedFileName);
                    uploadResult.ErrorCode = 1;
                }
                else if (processfile.Length > HomeSystemConsts.maxFileSize)
                {
                    logger.LogInformation("{FileName} of {Length} bytes is larger than the limit of {Limit} bytes (Err: 2)",
                        untrustedFileName, processfile.Length, HomeSystemConsts.maxFileSize);
                    uploadResult.ErrorCode = 2;
                }
                else
                {
                    try
                    {
                        FileStoreDto NewEntity = LazyServiceProvider.LazyGetRequiredService<FileStoreDto>();

                        NewEntity.Id = GuidGenerator.Create();
                        NewEntity.Name = Path.GetFileNameWithoutExtension(untrustedFileName);
                        NewEntity.ExtName = Path.GetExtension(untrustedFileName);
                        NewEntity.Size = processfile.Length;
                        NewEntity.Blob = new BlobStoreObject()
                        {
                            BlobContainerName = uploadDetail.BlobContainerName
                        };

                        string classification = uploadDetail.Classification.Split(uploadDetail.SplitChar, StringSplitOptions.RemoveEmptyEntries).LastOrDefault();

                        NewEntity.Classification = await _classificationAppService.FindByNameAsync(classification);

                        if (NewEntity.Classification == null)
                        {
                            uploadResult.ErrorCode = 93005;
                            uploadResult.ErrorMessage = L[HomeSystemDomainErrorCodes.API_Upload_ClassificationNotFound, classification].Value;
                            uploadResult.Success = false;
                            uploadResult.RefenceData = uploadDetail;
                            uploadResults.Add(uploadResult);
                            continue;
                        }
                        //"cms-kit-media",

                        await using MemoryStream fs = new();
                        await processfile.CopyToAsync(fs);

                        NewEntity.CreatorId = uploadDetail.UploadUserId;
                        NewEntity.Blob.FileContent = fs.ToArray();

                        NewEntity = await _fileStoreAppService.CreateAsync(NewEntity);

                        if (NewEntity != null)
                        {
                            uploadResult.FileStoreId = NewEntity.Id;
                            uploadResult.Success = true;
                        }
                        else
                        {
                            uploadResult.ErrorCode = 9999;
                            uploadResult.ErrorMessage = L[HomeSystemResource.GeneralError];
                            uploadResult.Success = false;
                        }

                        uploadResults.Add(uploadResult);
                    }
                    catch (IOException ex)
                    {
                        string msg = $"{untrustedFileName} error on upload (Err: 3): {ex.Message}";
                        logger.LogError(msg,
                            untrustedFileName, ex.Message);
                        uploadResult.ErrorCode = 99999;
                        uploadResult.ErrorMessage = msg;
                    }
                }


            }

            return new CreatedResult(resourcePath, uploadResults);
        }

        [RequireHttps]
        [HttpPut]
        public async Task<ActionResult<UploadResult>> PutFile([FromForm] string detail,[FromForm] string description, [FromForm] IFormFile file)
        {
            List<UploadResult> uploadResults = new();

            if (string.IsNullOrEmpty(detail))
            {
                uploadResults.Add(new UploadResult()
                {
                    ErrorCode = 99503,
                    ErrorMessage = L[HomeSystemDomainErrorCodes.CannotBeNullOrEmpty].Value,
                    Success = false,
                    RefenceData = null
                });

                return BadRequest(uploadResults);
            }

            FileStoreDto entity = System.Text.Json.JsonSerializer.Deserialize<FileStoreDto>(detail);

            entity.Description = description;

            var fileInDatabase = await _fileStoreAppService.GetAsync(entity.Id);

            if (fileInDatabase == null)
            {
                return NotFound(new UploadResult() { ErrorCode = 93004, ErrorMessage = L[HomeSystemDomainErrorCodes.API_Upload_NotFound, file.Name].Value, FileStoreId = entity.Id, Success = false });
            }

            //string _filenameWithoutExt = Path.GetFileNameWithoutExtension(entity.Name);
            //string _extname = Path.GetExtension(file.Name);

            bool updating = false;

            if (fileInDatabase.Name != entity.Name)
            {
                fileInDatabase.Name = entity.Name;
                updating = true;
            }

            if (fileInDatabase.ExtName != entity.ExtName)
            {
                fileInDatabase.ExtName = entity.ExtName;
                updating = true;
            }

            if (fileInDatabase.Description != entity.Description)
            {
                fileInDatabase.Description = entity.Description;
                updating = true;
            }

            if (fileInDatabase.IsPublic != entity.IsPublic)
            {
                fileInDatabase.IsPublic = entity.IsPublic;
                updating = true;
            }

            if (fileInDatabase.Classification.Name != entity.Classification.Name)
            {
                var newclassification = await _classificationAppService.FindByNameAsync(entity.Classification.Name);

                if (newclassification != null)
                {
                    fileInDatabase.Classification = newclassification;
                    updating = true;
                }
            }

            try
            {
                if (file != null)
                {
                    await using MemoryStream fs = new();
                    await file.CopyToAsync(fs);

                    byte[] buffer = fs.ToArray();

                    if (fileInDatabase.Size != file.Length)
                    {
                        fileInDatabase.Size = file.Length;

                        if (fileInDatabase.Blob == null)
                        {
                            fileInDatabase.Blob = new BlobStoreObject() { BlobContainerName = "cms-kit-media" };
                        }

                        fileInDatabase.Blob.FileContent = buffer;
                        updating = true;
                    }
                }

                if (updating)
                {
                    fileInDatabase.ModifierId = entity.ModifierId;
                    fileInDatabase.ModifierDate = entity.ModifierDate;

                    await _fileStoreAppService.UpdateAsync(entity.Id, fileInDatabase);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(L[HomeSystemDomainErrorCodes.API_Upload_UpdateServerError, entity.Name + entity.ExtName, ex.Message].Value);
                return new UploadResult() { Success = false, ErrorCode = 9999, ErrorMessage = ex.Message, FileStoreId = entity.Id };
            }

            return new UploadResult() { Success = true, FileStoreId = entity.Id, ErrorCode = 0 };
        }
    }
}
