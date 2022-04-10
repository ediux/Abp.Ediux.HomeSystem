using Ediux.HomeSystem.SystemManagement;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ediux.HomeSystem.Blazor.Pages
{
    public partial class Photos
    {
        [Inject] public IFileStoreClassificationAppService ClassificationAppService { get; set; }
        [Inject] public IConfiguration Config { get; set; }
        [Inject] public NavigationManager NavManager { get; set; }

        protected string RemoteHost { get; set; }

        protected override async Task OnInitializedAsync()
        {
            RemoteHost = Config["RemoteServices:Default:BaseUrl"];

            await GetEntitiesAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await GetEntitiesAsync();
            }
        }


        protected override async Task GetEntitiesAsync()
        {

            var result = await ClassificationAppService.GetListAsync(new AbpSearchRequestDto() { Search = "Photos" });

            if (result.TotalCount > 0)
            {
                var cls = result.Items.First();

                if (cls != null)
                {
                    var result2 = await AppService.GetListAsync(new FileStoreSearchRequestDto() { CurrentUser_Id = CurrentUser.Id, Classification_Id = cls.Id });
                    Entities = result2.Items;
                }
            }


        }

        protected void AddPhotoClick()
        {
            NavManager.NavigateTo("/Files");
        }

        protected string GetDescription(FileStoreDto imageSource)
        {
            if (!imageSource.Description.IsNullOrEmpty())
            {
                return imageSource.Description;
            }
            else
            {
                if (!imageSource.Name.IsNullOrEmpty())
                {
                    return imageSource.Name;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
