using Blazorise;

using Ediux.HomeSystem.Permissions;
using Ediux.HomeSystem.SystemManagement;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Volo.Abp.Guids;

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

      
        protected Visibility progressbarVisibility = Visibility.Invisible;
        private FileStoreDto selectedFile = null;
        private List<FileStoreDto> selectedFiles = new List<FileStoreDto>();
        private List<FileStoreDto> createNewFiles = new List<FileStoreDto>();
        private List<FileStoreDto> updateNewFiles = new List<FileStoreDto>();
        protected double uploadpercent = 0;

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
                    if(selectedFiles!=null )
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
        protected override async Task OnParametersSetAsync()
        {
            
            await GetEntitiesAsync();
            await base.OnParametersSetAsync();
        }


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

        protected override async Task CreateEntityAsync()
        {
            if (createNewFiles.Count > 0)
            {
                foreach (var f in createNewFiles)
                {
                    await AppService.CreateAsync(f);
                }
            }

            await base.CreateEntityAsync();
        }

        protected override async Task UpdateEntityAsync()
        {
            await base.UpdateEntityAsync();
            selectedFile = null;
            selectedFiles.Clear();
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

        protected bool ChcekIsCanAdd()
        {
            if (createNewFiles != null && createNewFiles.Count > 0)
            {
                return !((NewEntity.Blob != null && NewEntity.Blob.FileContent != null && NewEntity.Blob.FileContent.LongLength > 0) &&
                    createNewFiles.All(a => a.Blob != null && a.Blob.FileContent != null && a.Blob.FileContent.LongLength > 0));
            }

            return !(NewEntity.Blob != null && NewEntity.Blob.FileContent != null && NewEntity.Blob.FileContent.LongLength > 0);
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

            progressbarVisibility = Visibility.Invisible;
            uploadpercent = 0;

            await InvokeAsync(StateHasChanged);
        }

        protected async Task OnFileUploadChanged(FileChangedEventArgs e)
        {
            if (e.Files != null && e.Files.Length > 0)
            {
                IFileEntry fileEntry = null;
                createNewFiles.Clear();

                if (e.Files.Length > 1)
                {
                    for (int i = 0; i < e.Files.Length; i++)
                    {
                        fileEntry = e.Files[i];

                        if (i == 0)
                        {

                            NewEntity.Id = GuidGenerator.Create();
                            NewEntity.Name = Path.GetFileNameWithoutExtension(fileEntry.Name);
                            NewEntity.ExtName = Path.GetExtension(fileEntry.Name);
                            NewEntity.Classification = FileClassification;
                            NewEntity.Blob = new BlobStoreObject()
                            {
                                BlobContainerName = "cms-kit-media"
                            };
                            NewEntity.Size = fileEntry.Size;
                            using (var stream = new MemoryStream())
                            {
                                // Here we're telling the FileEdit where to write the upload result
                                await fileEntry.WriteToStreamAsync(stream);

                                // Once we reach this line it means the file is fully uploaded.
                                // In this case we're going to offset to the beginning of file
                                // so we can read it.
                                stream.Seek(0, SeekOrigin.Begin);

                                // Use the stream reader to read the content of uploaded file,
                                // in this case we can assume it is a textual file.
                                NewEntity.Blob.FileContent = stream.ToArray();
                            }


                        }
                        else
                        {
                            FileStoreDto newFile = new FileStoreDto();

                            newFile.Id = GuidGenerator.Create();
                            newFile.Name = Path.GetFileNameWithoutExtension(fileEntry.Name);
                            newFile.ExtName = Path.GetExtension(fileEntry.Name);
                            newFile.Classification = FileClassification;
                            newFile.IsPublic = NewEntity.IsPublic;
                            newFile.Blob = new SystemManagement.BlobStoreObject()
                            {
                                BlobContainerName = "cms-kit-media",

                            };
                            newFile.Size = fileEntry.Size;
                            using (var stream = new MemoryStream())
                            {
                                // Here we're telling the FileEdit where to write the upload result
                                await fileEntry.WriteToStreamAsync(stream);

                                // Once we reach this line it means the file is fully uploaded.
                                // In this case we're going to offset to the beginning of file
                                // so we can read it.
                                stream.Seek(0, SeekOrigin.Begin);

                                // Use the stream reader to read the content of uploaded file,
                                // in this case we can assume it is a textual file.
                                newFile.Blob.FileContent = stream.ToArray();
                            }
                            createNewFiles.Add(newFile);
                        }
                    }
                }
                else
                {
                    fileEntry = e.Files[0];

                    NewEntity.Id = GuidGenerator.Create();
                    NewEntity.Name = Path.GetFileNameWithoutExtension(fileEntry.Name);
                    NewEntity.ExtName = Path.GetExtension(fileEntry.Name);
                    NewEntity.Classification = FileClassification;
                    NewEntity.Blob = new SystemManagement.BlobStoreObject()
                    {
                        BlobContainerName = "cms-kit-media",
                    };
                    NewEntity.Size = fileEntry.Size;

                    using (var stream = new MemoryStream())
                    {
                        // Here we're telling the FileEdit where to write the upload result
                        await fileEntry.WriteToStreamAsync(stream);

                        // Once we reach this line it means the file is fully uploaded.
                        // In this case we're going to offset to the beginning of file
                        // so we can read it.
                        stream.Seek(0, SeekOrigin.Begin);

                        // Use the stream reader to read the content of uploaded file,
                        // in this case we can assume it is a textual file.
                        NewEntity.Blob.FileContent = stream.ToArray();
                    }
                }
            }
            await InvokeAsync(StateHasChanged);
        }

        protected async Task OnUpdateFileUploadChanged(FileChangedEventArgs e)
        {
            if (e.Files != null && e.Files.Length > 0)
            {
                IFileEntry fileEntry = null;
                updateNewFiles.Clear();

                if (e.Files.Length > 1)
                {
                    for (int i = 0; i < e.Files.Length; i++)
                    {
                        fileEntry = e.Files[i];

                        if (i == 0)
                        {


                            EditingEntity.Name = Path.GetFileNameWithoutExtension(fileEntry.Name);
                            EditingEntity.ExtName = Path.GetExtension(fileEntry.Name);
                            EditingEntity.Classification = FileClassification;
                            EditingEntity.Blob = new SystemManagement.BlobStoreObject()
                            {
                                BlobContainerName = "cms-kit-media"
                            };
                            EditingEntity.Size = fileEntry.Size;
                            using (var stream = new MemoryStream())
                            {
                                // Here we're telling the FileEdit where to write the upload result
                                await fileEntry.WriteToStreamAsync(stream);

                                // Once we reach this line it means the file is fully uploaded.
                                // In this case we're going to offset to the beginning of file
                                // so we can read it.
                                stream.Seek(0, SeekOrigin.Begin);

                                // Use the stream reader to read the content of uploaded file,
                                // in this case we can assume it is a textual file.
                                EditingEntity.Blob.FileContent = stream.ToArray();
                            }


                        }
                        else
                        {
                            FileStoreDto newFile = new FileStoreDto();


                            newFile.Name = Path.GetFileNameWithoutExtension(fileEntry.Name);
                            newFile.ExtName = Path.GetExtension(fileEntry.Name);
                            newFile.Classification = FileClassification;
                            newFile.IsPublic = EditingEntity.IsPublic;
                            newFile.Blob = new SystemManagement.BlobStoreObject()
                            {
                                BlobContainerName = "cms-kit-media",

                            };
                            newFile.Size = fileEntry.Size;
                            using (var stream = new MemoryStream())
                            {
                                // Here we're telling the FileEdit where to write the upload result
                                await fileEntry.WriteToStreamAsync(stream);

                                // Once we reach this line it means the file is fully uploaded.
                                // In this case we're going to offset to the beginning of file
                                // so we can read it.
                                stream.Seek(0, SeekOrigin.Begin);

                                // Use the stream reader to read the content of uploaded file,
                                // in this case we can assume it is a textual file.
                                newFile.Blob.FileContent = stream.ToArray();
                            }
                            updateNewFiles.Add(newFile);
                        }
                    }
                }
                else
                {
                    fileEntry = e.Files[0];


                    EditingEntity.Name = Path.GetFileNameWithoutExtension(fileEntry.Name);
                    EditingEntity.ExtName = Path.GetExtension(fileEntry.Name);
                    EditingEntity.Classification = FileClassification;
                    EditingEntity.Blob = new SystemManagement.BlobStoreObject()
                    {
                        BlobContainerName = "cms-kit-media",
                    };
                    EditingEntity.Size = fileEntry.Size;

                    using (var stream = new MemoryStream())
                    {
                        // Here we're telling the FileEdit where to write the upload result
                        await fileEntry.WriteToStreamAsync(stream);

                        // Once we reach this line it means the file is fully uploaded.
                        // In this case we're going to offset to the beginning of file
                        // so we can read it.
                        stream.Seek(0, SeekOrigin.Begin);

                        // Use the stream reader to read the content of uploaded file,
                        // in this case we can assume it is a textual file.
                        NewEntity.Blob.FileContent = stream.ToArray();
                    }
                }
            }
            await InvokeAsync(StateHasChanged);
        }

        protected void OnProgressed(FileProgressedEventArgs e)
        {
            if (e.Percentage < 100)
            {
                progressbarVisibility = Visibility.Visible;
            }
            else
            {
                progressbarVisibility = Visibility.Invisible;
            }

            uploadpercent = e.Percentage;

            if (e.Progress == 1)
            {
                progressbarVisibility = Visibility.Invisible;
            }
        }
    }
}