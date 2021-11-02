using Ediux.HomeSystem.Data;
using Ediux.HomeSystem.MediaDescriptors;
using Ediux.HomeSystem.Models.Views;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.Domain.Repositories;
using Volo.CmsKit.Admin.Blogs;
using Volo.CmsKit.Admin.Pages;

namespace Ediux.HomeSystem.Miscellaneous
{
    public class MiscellaneousAppService : ApplicationService, IMiscellaneousAppService
    {

        protected readonly IBlobContainer<AutoSaveContainer> autoSaveContainer;
        protected readonly IRepository<File_Store> file_Stores;

        public MiscellaneousAppService(IBlobContainer<AutoSaveContainer> autoSaveContainer,
             IRepository<Data.File_Store> file_Stores)
        {
            this.autoSaveContainer = autoSaveContainer;
            this.file_Stores = file_Stores;
        }

        public async Task<AutoSaveModel> AutoSaveAsync(AutoSaveModel input)
        {
            if (input != null)
            {
                if (string.IsNullOrWhiteSpace(input.entityType))
                {
                    input.entityType = "default";
                }

                if (string.IsNullOrWhiteSpace(input.id))
                {
                    input.id = CurrentUser.Id.ToString();
                }

                string tempfilename = $"{input.entityType}_{input.id}_{input.elementId}";

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

                    file_Store.MIMETypeId = -1;

                    file_Store = await file_Stores.InsertAsync(file_Store);
                }
            }

            return input;
        }

        public async Task RemoveAutoSaveDataAsync(AutoSaveModel input)
        {
            if (input != null)
            {
                if (string.IsNullOrWhiteSpace(input.entityType))
                {
                    input.entityType = "default";
                }

                if (string.IsNullOrWhiteSpace(input.id))
                {
                    throw new UserFriendlyException("'id' can't be null or empty.");
                }

                string tempfilename = $"{input.entityType}_{input.id}";

                var foundList = (await file_Stores.GetQueryableAsync()).Where(w => w.Name.Contains(tempfilename)).Select(s => s.Name).ToList();

                if (foundList.Any())
                {
                    foundList.ForEach(async l =>
                    {
                        await autoSaveContainer.DeleteAsync(tempfilename);
                    });

                    await file_Stores.DeleteAsync(p => p.Name.Contains(tempfilename));
                }
            }
        }
    }
}
