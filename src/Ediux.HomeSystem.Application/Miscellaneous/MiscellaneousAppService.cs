using Ediux.HomeSystem.Data;
using Ediux.HomeSystem.Localization;
using Ediux.HomeSystem.MediaDescriptors;
using Ediux.HomeSystem.MIMETypeManager;
using Ediux.HomeSystem.Models.DTOs.AutoSave;

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.Domain.Repositories;

namespace Ediux.HomeSystem.Miscellaneous
{
    public class MiscellaneousAppService : ApplicationService, IMiscellaneousAppService
    {

        protected readonly IBlobContainer<AutoSaveContainer> autoSaveContainer;
        protected readonly IMIMETypeManagerAppService MIMETypeManagerAppService;
        protected readonly IRepository<File_Store> file_Stores;

        public MiscellaneousAppService(IBlobContainer<AutoSaveContainer> autoSaveContainer,
             IRepository<File_Store> file_Stores,
             IMIMETypeManagerAppService mIMETypeManagerAppService)
        {
            this.autoSaveContainer = autoSaveContainer;
            this.file_Stores = file_Stores;
            MIMETypeManagerAppService = mIMETypeManagerAppService;
        }

        public async Task<AutoSaveDTO> CreateAsync(AutoSaveDTO input)
        {
            if (input != null)
            {
                if (string.IsNullOrWhiteSpace(input.entityType))
                {
                    input.entityType = "default";
                }

                if (string.IsNullOrWhiteSpace(input.Id))
                {
                    if (CurrentUser.IsAuthenticated == false)
                    {
                        input.Id = GuidGenerator.Create().ToString();
                    }
                    else
                    {
                        input.Id = CurrentUser.Id.ToString();
                    }
                }

                string tempfilename = $"autosave_{input.entityType}_{input.Id}_{input.elementId}";

                Stream stream = await autoSaveContainer.GetOrNullAsync(tempfilename);

                if (stream != null)
                {
                    using (StreamReader streamReader = new StreamReader(stream))
                    {
                        string savedData = streamReader.ReadToEnd();

                        if (savedData != input.data)
                        {
                            await autoSaveContainer.SaveAsync(tempfilename, Encoding.UTF8.GetBytes(input.data), overrideExisting: true);
                        }

                        streamReader.Close();
                    }
                }
                else
                {
                    await autoSaveContainer.SaveAsync(tempfilename, Encoding.UTF8.GetBytes(input.data));
                }


                File_Store file_Store = file_Stores.Where(w => w.Name == tempfilename).SingleOrDefault();

                if (file_Store != null)
                {
                    file_Store.LastModificationTime = DateTime.UtcNow;
                    file_Store.LastModifierId = CurrentUser.Id;
                    await file_Stores.UpdateAsync(file_Store);
                }
                else
                {
                    file_Store = new File_Store()
                    {
                        CreationTime = DateTime.UtcNow,
                        CreatorId = CurrentUser.Id,
                        ExtName = "",
                        Name = tempfilename,
                        Size = input.data.Length,
                        StorageInSMB = false,
                        OriginFullPath = $"host{Path.DirectorySeparatorChar}auto-save-temp{Path.DirectorySeparatorChar}{tempfilename}"
                    };

                    var mimetypes = await MIMETypeManagerAppService.GetListAsync(new Models.DTOs.jqDataTables.jqDTSearchedResultRequestDto() { Search = file_Store.ExtName });
                    Models.DTOs.MIMETypes.MIMETypesDTO mIMETypesDTO = null;

                    if (mimetypes.TotalCount >= 1)
                    {
                        mIMETypesDTO = mimetypes.Items.SingleOrDefault(p => p.RefenceExtName == file_Store.ExtName);
                    }

                    if (mIMETypesDTO == null)
                    {
                        mIMETypesDTO = await MIMETypeManagerAppService.CreateAsync(new Models.DTOs.MIMETypes.MIMETypesDTO()
                        {
                            Description = L[HomeSystemResource.Features.MIMETypes.DefaultBinaryFile_Description],
                            MIME = "application/octet-stream",
                            RefenceExtName = file_Store.ExtName,
                            CreationTime = DateTime.UtcNow,
                            CreatorId = CurrentUser.Id
                        });
                    }

                    file_Store.MIMETypeId = mIMETypesDTO.Id;

                    file_Store = await file_Stores.InsertAsync(file_Store);
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

                string tempfilename = $"autosave_{input.entityType}_{input.Id}";

                var foundList = (await file_Stores.GetQueryableAsync()).Where(w => w.Name.Contains(tempfilename)).Select(s => s.Name).ToList();

                if (foundList.Any())
                {
                    foundList.ForEach(async l =>
                    {
                        await autoSaveContainer.DeleteAsync(l);
                    });

                    await file_Stores.DeleteAsync(p => p.Name.Contains(tempfilename));
                }
            }
        }
    }
}
