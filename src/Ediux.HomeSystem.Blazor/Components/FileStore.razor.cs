using Blazorise;

using Ediux.HomeSystem.Permissions;
using Ediux.HomeSystem.SystemManagement;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Volo.Abp.Guids;

using static System.Net.WebRequestMethods;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;

namespace Ediux.HomeSystem.Blazor.Components
{
    public partial class FileStore
    {
        [Parameter]
        public FileClassificationDto FileClassification { get; set; }

        [Parameter]
        public EventCallback<FileClassificationDto> FileClassificationChanged { get; set; }

        [Inject]
        public IGuidGenerator GuidGenerator { get; set; }

        public HttpClient Http { get; set; }

        [Inject]
        public IHttpClientFactory HttpClientFactory
        {
            get => _httpClientFactory; set
            {
                _httpClientFactory = value;
                Http = _httpClientFactory.CreateClient("client");
            }
        }

        [Inject]
        public IMIMETypeManagerAppService MIMETypeManager { get; set; }

        private IHttpClientFactory _httpClientFactory;

        protected Visibility create_progressbarVisibility = Visibility.Invisible;
        protected Visibility update_progressbarVisibility = Visibility.Invisible;

        private FileStoreDto selectedFile = null;
        private List<FileStoreDto> selectedFiles = new List<FileStoreDto>();
        protected List<UploadFileJSONData> uploadFileList = new List<UploadFileJSONData>();

        protected FileEdit createNewFileEdit;
        protected FileEdit updateNewFileEdit;

        protected Progress createNewProgress;
        protected Progress updateNewProgress;

        private MultipartFormDataContent createNewFiles = new MultipartFormDataContent(HomeSystemConsts.boundary);
        private MultipartFormDataContent updateNewFiles = new MultipartFormDataContent(HomeSystemConsts.boundary);

        protected double create_uploadpercent = 0;
        protected double update_uploadpercent = 0;

        protected int create_success_count = 0;
        protected int create_failed_count = 0;

        protected int update_success_count = 0;
        protected int update_failed_count = 0;

        protected bool createReady = false;
        protected bool updateReady = false;

        #region 建構式
        public FileStore()
        {
        }

        protected override async Task OnParametersSetAsync()
        {
            await GetEntitiesAsync();
        }
        #endregion

        #region 伺服器查詢
        protected override async Task GetEntitiesAsync()
        {
            if (FileClassification != null)
            {
                var result = await this.AppService.GetListAsync(new FileStoreSearchRequestDto()
                {
                    Classification_Id = FileClassification?.Id,
                    CurrentUser_Id = CurrentUser.Id
                });

                Entities = result.Items;
            }
            else
            {
                Entities = new List<FileStoreDto>();
            }
            await InvokeAsync(StateHasChanged);
        }
        #endregion

        #region 工具列按鈕控制
        protected Visibility CheckbtnAddFileVisibility()
        {
            Task<AuthorizationResult> task = AuthorizationService.AuthorizeAsync(HomeSystemPermissions.Files.Special);
            task.Wait();

            bool hasSucceededPolicy = (task.Result).Succeeded;

            if (hasSucceededPolicy)
            {
                if (FileClassification == null)
                {
                    return Visibility.Invisible;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
            else
            {
                return Visibility.Invisible;
            }

        }

        protected Visibility CheckBtnDeleteFileVisibilty()
        {
            Task<AuthorizationResult> task = AuthorizationService.AuthorizeAsync(HomeSystemPermissions.Files.Delete);
            task.Wait();

            bool hasSucceededPolicy = (task.Result).Succeeded;

            if (hasSucceededPolicy)
            {
                if (selectedFiles != null && selectedFiles.Count > 0)
                {
                    return Visibility.Visible;
                }

                if (selectedFile != null)
                {
                    return Visibility.Visible;
                }
            }
            else
            {
                return Visibility.Invisible;
            }


            return Visibility.Invisible;
        }

        protected Visibility CheckBtnEditFileVisibilty()
        {
            Task<AuthorizationResult> task = AuthorizationService.AuthorizeAsync(HomeSystemPermissions.Files.Modify);
            task.Wait();

            bool hasSucceededPolicy = (task.Result).Succeeded;

            if (hasSucceededPolicy)
            {
                if (selectedFiles != null && selectedFiles.Count > 0)
                {
                    return Visibility.Visible;
                }

                if (selectedFile != null)
                {
                    return Visibility.Visible;
                }
            }
            else
            {
                return Visibility.Invisible;
            }


            return Visibility.Invisible;
        }

        protected string UpdateHeadingMessage()
        {
            if (FileClassification != null)
            {
                if (selectedFile != null)
                {
                    return $"已選擇 {FileClassification.Name} 分類, 已選取 1 個檔案(共 {Entities.Count} 個檔案)";
                }
                else
                {
                    if (selectedFiles != null)
                    {
                        return $"已選擇 {FileClassification.Name} 分類, 已選取 {selectedFiles.Count} 個檔案(共 {Entities.Count} 個檔案)";
                    }
                    else
                    {
                        return $"已選擇 {FileClassification.Name} 分類, 已選取 0 個檔案(共 {Entities.Count} 個檔案)";
                    }
                }

            }
            else
            {
                return $"未選擇分類";
            }
        }

        protected async Task EditFilesClick()
        {
            if (selectedFile != null)
            {
                await OpenEditModalAsync(selectedFile);
            }
            else
            {
                if (selectedFiles != null && selectedFiles.Count > 0)
                {
                    await OpenEditModalAsync(selectedFiles.First());
                }
                else
                {
                    await Message.Error("請選擇檔案!");
                }
            }

            await InvokeAsync(StateHasChanged);
        }

        protected async Task DeleteFilesClick()
        {
            if (selectedFile != null)
            {
                await DeleteEntityAsync(selectedFile);
            }
            else
            {
                if (selectedFiles != null && selectedFiles.Count > 0)
                {
                    for (int i = 0; i < selectedFiles.Count; i++)
                    {
                        await DeleteEntityAsync(selectedFiles[i]);
                    }
                }
                else
                {
                    await Message.Error("請選擇檔案!");
                }
            }

            selectedFile = null;
            selectedFiles.Clear();
            await InvokeAsync(StateHasChanged);
        }

        protected async Task RefreshClick()
        {
            selectedFile = null;
            selectedFiles.Clear();

            create_progressbarVisibility = Visibility.Invisible;
            update_progressbarVisibility = Visibility.Invisible;

            create_uploadpercent = 0;
            update_uploadpercent = 0;

            await GetEntitiesAsync();

        }
        #endregion

        #region 新增檔案
        protected override Task OpenCreateModalAsync()
        {
            createNewFiles.Dispose();
            createNewFiles = new MultipartFormDataContent();

            return base.OpenCreateModalAsync();
        }

        protected void OnProgressed(FileProgressedEventArgs e)
        {
            create_uploadpercent = e.Percentage;

            if (e.Progress == 1)
            {
                create_progressbarVisibility = Visibility.Invisible;
            }
        }

        protected override async Task CreateEntityAsync()
        {
            //上傳檔案到遠端伺服器
            if (createReady)
            {
                var response = await Http.PostAsync("/api/Upload", createNewFiles);

                if (response.IsSuccessStatusCode)
                {
                    var newUploadResults = await response.Content
                    .ReadFromJsonAsync<IList<UploadResult>>();

                    if (newUploadResults != null)
                    {
                        create_success_count = newUploadResults.Where(w => w.Success == true).Count();
                        create_failed_count = newUploadResults.Where(w => w.Success == false).Count();
                    }

                    if (create_success_count > 0 && create_failed_count == 0)
                    {
                        await Notify.Success("上傳成功!");
                    }
                    else if (create_success_count > 0 && create_failed_count > 0)
                    {
                        await Notify.Warn("部分檔案上傳成功!");
                    }
                    else
                    {
                        await Notify.Error("上傳失敗!");
                    }
                }
                else
                {
                    await Notify.Error("上傳失敗!伺服器發生錯誤!");

                }
            }

            await CloseCreateModalAsync();
            await RefreshClick();
        }

        protected async Task OnFileUploadChanged(FileChangedEventArgs e)
        {
            create_progressbarVisibility = Visibility.Visible;

            if (e.Files != null && e.Files.Length > 0)
            {
                int i = 0;

                uploadFileList = new List<UploadFileJSONData>();

                foreach (var file in e.Files)
                {
                    var stream = new MemoryStream();

                    // Here we're telling the FileEdit where to write the upload result                        
                    await file.OpenReadStream(HomeSystemConsts.maxFileSize).CopyToAsync(stream);

                    // Once we reach this line it means the file is fully uploaded.
                    // In this case we're going to offset to the beginning of file
                    // so we can read it.
                    stream.Seek(0, SeekOrigin.Begin);

                    var fileType = await MIMETypeManager.GetByExtNameAsync(Path.GetExtension(file.Name));

                    if (fileType == null)
                    {
                        continue;
                    }

                    var fileContent = new StreamContent(stream);

                    fileContent.Headers.ContentType =
                        new MediaTypeHeaderValue(fileType.ContentType);

                    createNewFiles.Add(content: fileContent, name: "\"files\"", fileName: file.Name);

                    UploadFileJSONData detail = new UploadFileJSONData()
                    {
                        BlobContainerName = "cms-kit-media",
                        Classification = FileClassification.Name,
                        Order = i,
                        FileName = file.Name,
                        SplitChar = '/',
                        UploadUserId = CurrentUser.Id.HasValue ? CurrentUser.Id.Value : Guid.Parse("2e701e62-0953-4dd3-910b-dc6cc93ccb0d")
                    };

                    uploadFileList.Add(detail);

                    i++;

                }

                createReady = true;

                createNewFiles.Add(content: new StringContent(JsonSerializer.Serialize(uploadFileList)), name: "\"Data\"");

            }

            //await InvokeAsync(StateHasChanged);
        }
        #endregion

        #region 更新檔案
        protected override Task OpenEditModalAsync(FileStoreDto entity)
        {
            updateNewFiles.Dispose();
            updateNewFiles = new MultipartFormDataContent();

            return base.OpenEditModalAsync(entity);
        }

        protected void UpdateOnProgressed(FileProgressedEventArgs e)
        {
            update_uploadpercent = e.Percentage;

            if (e.Progress == 1)
            {
                update_progressbarVisibility = Visibility.Invisible;
            }
        }

        protected override async Task UpdateEntityAsync()
        {
            if (updateReady)
            {

                string updateJson = JsonSerializer.Serialize(EditingEntity);
                updateNewFiles.Add(content: new StringContent(updateJson), "\"detail\"");
                updateNewFiles.Add(content: new StringContent(EditingEntity.Description), "\"description\"");

                var response = await Http.PutAsync("/api/Upload", updateNewFiles);

                if (response.IsSuccessStatusCode)
                {
                    var newUploadResults = await response.Content.ReadFromJsonAsync<UploadResult>();

                    if (newUploadResults.Success)
                    {
                        await Notify.Success("更新檔案成功!");
                    }
                    else
                    {
                        await Notify.Error("更新檔案失敗!");
                    }
                }
                else
                {
                    await Notify.Error("更新失敗!伺服器發生錯誤!");

                }
            }

            await CloseEditModalAsync();
            await RefreshClick();
        }



        protected async Task OnUpdateFileUploadChanged(FileChangedEventArgs e)
        {
            update_progressbarVisibility = Visibility.Visible;

            if (e.Files != null && e.Files.Length == 1)
            {
                IFileEntry file = e.Files[0];

                var stream = new MemoryStream();

                // Here we're telling the FileEdit where to write the upload result                        
                await file.OpenReadStream(HomeSystemConsts.maxFileSize).CopyToAsync(stream);

                // Once we reach this line it means the file is fully uploaded.
                // In this case we're going to offset to the beginning of file
                // so we can read it.
                stream.Seek(0, SeekOrigin.Begin);

                var fileType = await MIMETypeManager.GetByExtNameAsync(Path.GetExtension(file.Name));

                if (fileType == null)
                {
                    updateReady = false;
                    return;
                }

                EditingEntity.Size = file.Size;

                var fileContent = new StreamContent(stream);

                fileContent.Headers.ContentType =
                    new MediaTypeHeaderValue(fileType.ContentType);

                updateNewFiles.Add(content: fileContent, name: "\"file\"", fileName: file.Name);

                EditingEntity.ModifierDate = DateTime.Now;
                EditingEntity.ModifierId = CurrentUser.Id;

                updateReady = true;
            }

        }

        public void EditingFileName_TextChanged(string data)
        {
            if (!EditingEntity.Name.IsNullOrEmpty() && !data.IsNullOrEmpty())
            {
                if (EditingEntity.Name != data)
                {
                    EditingEntity.Name = data;
                    updateReady = true;
                }
            }
            else if (!EditingEntity.Name.IsNullOrEmpty())
            {
                EditingEntity.Name = data;
                updateReady = true;
            }
            else if (!data.IsNullOrEmpty())
            {
                EditingEntity.Name = data;
                updateReady = true;
            }
            EditingEntity.ModifierDate = DateTime.Now;
            EditingEntity.ModifierId = CurrentUser.Id;
        }

        public void EditingFileExtName_TextChanged(string data)
        {
            if (!EditingEntity.ExtName.IsNullOrEmpty() && !data.IsNullOrEmpty())
            {
                if (EditingEntity.ExtName != data)
                {
                    EditingEntity.ExtName = data;
                    updateReady = true;
                }
            }
            else if (!EditingEntity.ExtName.IsNullOrEmpty())
            {
                EditingEntity.ExtName = data;
                updateReady = true;
            }
            else if (!data.IsNullOrEmpty())
            {
                EditingEntity.ExtName = data;
                updateReady = true;
            }
            EditingEntity.ModifierDate = DateTime.Now;
            EditingEntity.ModifierId = CurrentUser.Id;

        }

        public void EditingFileDescription_TextChanaged(string data)
        {
            if (!EditingEntity.Description.IsNullOrEmpty() && !data.IsNullOrEmpty())
            {
                if (EditingEntity.Description != data)
                {
                    EditingEntity.Description = data;
                    updateReady = true;
                }
            }
            else if (!EditingEntity.ExtName.IsNullOrEmpty())
            {
                EditingEntity.Description = data;
                updateReady = true;
            }
            else if (!data.IsNullOrEmpty())
            {
                EditingEntity.Description = data;
                updateReady = true;
            }
            EditingEntity.ModifierDate = DateTime.Now;
            EditingEntity.ModifierId = CurrentUser.Id;
        }

        public void EditingIsPublic_CheckChanaged(bool check)
        {
            if (EditingEntity.IsPublic != check)
            {
                EditingEntity.IsPublic = check;
                updateReady = true;

                EditingEntity.ModifierDate = DateTime.Now;
                EditingEntity.ModifierId = CurrentUser.Id;
            }
        }
        #endregion

        #region 共用控制
        protected bool ChcekIsCanAdd(bool isNew)
        {
            if (isNew)
            {
                return !createReady;
            }
            else
            {
                return !updateReady;
            }
        }
        #endregion

    }
}