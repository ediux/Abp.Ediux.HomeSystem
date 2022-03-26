using Blazorise;

using Ediux.HomeSystem.Permissions;
using Ediux.HomeSystem.SystemManagement;

using Microsoft.AspNetCore.Authorization;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ediux.HomeSystem.Blazor.Pages
{
    public partial class FileExplorer
    {

        IList<FileClassificationDto> ExpandedNodes = new List<FileClassificationDto>();
        FileClassificationDto selectedNode;

        protected override async Task OnInitializedAsync()
        {
            await GetEntitiesAsync();
            
        }

        protected override async Task GetEntitiesAsync()
        {
            Entities = (await AppService.GetListAsync(new AbpSearchRequestDto())).Items;

            foreach (var entity in Entities)
            {
                if (entity.Childs != null && entity.Childs.Count > 0)
                {
                    foreach (var child in entity.Childs)
                    {
                        await RecursiveLoad(child);
                    }
                }
            }

            selectedNode = null;
            await InvokeAsync(StateHasChanged);
        }

        protected Visibility CheckCanShowWithSpecialPermission()
        {            
            Task<AuthorizationResult> task = AuthorizationService.AuthorizeAsync(HomeSystemPermissions.Files.Special);
            task.Wait();

            bool hasSucceededPolicy = (task.Result).Succeeded;

            if (hasSucceededPolicy)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Invisible;
            }
        }

        protected bool CheckCanDeleteWithPermission()
        {
            Task<AuthorizationResult> task = AuthorizationService.AuthorizeAsync(HomeSystemPermissions.Files.Delete);
            task.Wait();

            bool hasSucceededPolicy = (task.Result).Succeeded;

            if (hasSucceededPolicy)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected bool CheckCanEditWithPermission()
        {
            Task<AuthorizationResult> task = AuthorizationService.AuthorizeAsync(HomeSystemPermissions.Files.Modify);
            task.Wait();

            bool hasSucceededPolicy = (task.Result).Succeeded;

            if (hasSucceededPolicy)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task RecursiveLoad(FileClassificationDto item)
        {
            var node = await AppService.GetAsync(item.Id);

            item.Childs = node.Childs;

            if (item.Childs != null && item.Childs.Count > 0)
            {
                foreach (var child in item.Childs)
                {
                    await RecursiveLoad(child);
                }
            }
        }

        protected override async Task CreateEntityAsync()
        {
            if (selectedNode != null)
            {
                NewEntity.Parent = selectedNode;
            }

            await base.CreateEntityAsync();
            selectedNode = NewEntity;
            await InvokeAsync(StateHasChanged);
        }

        protected override async Task DeleteEntityAsync(FileClassificationDto entity)
        {
            if (await Message.Confirm(GetDeleteConfirmationMessage(entity)))
            {
                await base.DeleteEntityAsync(entity);
            }
        }



    }
}
