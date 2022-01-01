using Ediux.HomeSystem.Files;
using Ediux.HomeSystem.Localization;
using Ediux.HomeSystem.Models.DTOs.AutoSave;
using Ediux.HomeSystem.Models.DTOs.Files;
using System;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp;
using Volo.Abp.Application.Services;

namespace Ediux.HomeSystem.Miscellaneous
{
    public class MiscellaneousAppService : ApplicationService, IMiscellaneousAppService
    {
        private readonly IFileStoreAppService _fileStoreAppService;

        public MiscellaneousAppService(IFileStoreAppService fileStoreAppService)
        {
            _fileStoreAppService = fileStoreAppService;
        }

        public async Task<AutoSaveDTO> CreateAsync(AutoSaveDTO input)
        {
            if (input != null)
            {
                if (string.IsNullOrWhiteSpace(input.entityType))
                {
                    input.entityType = "default";
                }

                Guid tempId = GuidGenerator.Create();

                if (string.IsNullOrWhiteSpace(input.Id))
                {
                    if (CurrentUser.IsAuthenticated == false)
                    {
                        input.Id = tempId.ToString();
                    }
                    else
                    {
                        tempId = CurrentUser.Id ?? tempId;
                        input.Id = tempId.ToString();
                    }
                }
                else
                {
                    tempId = Guid.Parse(input.Id);
                }

                FileStoreDTO fileStoreDTO = new FileStoreDTO()
                {
                    Name = $"autosave_{input.entityType}_{input.Id}_{input.elementId}",
                    ExtName = "",
                    IsAutoSaveFile = true,
                    ContentType = HomeSystemConsts.DefaultContentType,
                    Description = L[HomeSystemResource.Features.Files.IsAutoSaveFile],
                    OriginFullPath = $"autosave_{input.entityType}_{input.Id}_{input.elementId}",
                    Size = input.data.Length,
                    FileContent = input.data.GetBytes(),
                    Id = tempId
                };

                string tempfilename = $"autosave_{input.entityType}_{input.Id}_{input.elementId}";

                if (await _fileStoreAppService.IsExistsAsync(fileStoreDTO.Id))
                {
                    await _fileStoreAppService.UpdateAsync(fileStoreDTO.Id, fileStoreDTO);
                }
                else
                {
                    await _fileStoreAppService.CreateAsync(fileStoreDTO);
                }
            }

            return input;
        }

        public async Task RemoveAutoSaveDataAsync(AutoSaveDTO input)
        {
            if (input != null)
            {
                if (string.IsNullOrWhiteSpace(input.entityType))
                {
                    input.entityType = "default";
                }

                if (string.IsNullOrWhiteSpace(input.Id))
                {
                    throw new UserFriendlyException(L[HomeSystemResource.CannotBeNullOrEmpty, nameof(input.Id)],
                        code: HomeSystemDomainErrorCodes.CannotBeNullOrEmpty,
                        logLevel: Microsoft.Extensions.Logging.LogLevel.Error);
                }

                await _fileStoreAppService.DeleteAsync(Guid.Parse(input.Id));
            }
        }
    }
}
