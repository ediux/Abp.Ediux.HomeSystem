using Blazorise.RichTextEdit;

using Ediux.HomeSystem.Features.Blogs;
using Ediux.HomeSystem.Features.Blogs.DTOs;
using Ediux.HomeSystem.Localization;
using Ediux.HomeSystem.SystemManagement;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;

using System;
using System.Threading.Tasks;

using Volo.Abp.TenantManagement;

namespace Ediux.HomeSystem.Blazor.Pages
{
    public partial class BlogPostManager
    {
        [Parameter]
        public Guid BlogId { get; set; }

        protected BlogItemDto Blog { get; set; }

        [Inject]
        public IBlogsAppServices BlogsAppService { get; set; }

        [Inject] public IFileStoreClassificationAppService ClassificationAppService { get; set; }
        [Inject] public IConfiguration Config { get; set; }
        [Inject] public NavigationManager NavManager { get; set; }

        protected RichTextEdit richTextCreateRef;
        protected RichTextEdit richTextEditRef;

        protected string RemoteHost { get; set; }

        protected override async Task OnInitializedAsync()
        {

            RemoteHost = Config["RemoteServices:Default:BaseUrl"];

            await GetEntitiesAsync();
        }

        protected string GetHeaderString()
        {
            if (Blog == null)
            {
                return L[HomeSystemResource.Features.Blogs.Prefix].Value;
            }
            else
            {
                return $"{Blog.Name} - {L[HomeSystemResource.Features.Blogs.Prefix].Value}";
            }
        }
        
        protected string GetUrl(FileStoreDto imageSource, string text)
        {
            if (imageSource == null)
            {                
                return $"{RemoteHost}/api/TextImage/Blog_{BlogId}_{text}?text={text}&width=512&height=512";
            }
            else
            {
                return $"{RemoteHost}/api/Downloads/{imageSource.Id}";
            }
        }

        protected string GetDescription(FileStoreDto imageSource)
        {
            if (imageSource == null)
                return string.Empty;

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
        protected override async Task OnParametersSetAsync()
        {
            Blog = await BlogsAppService.GetAsync(BlogId);
        }

        protected override async Task GetEntitiesAsync()
        {
            Entities = (await AppService.GetListByBlogAsync(new BlogPostSearchRequestDto() { BlogId = BlogId })).Items;
        }

        public async Task OnContentChanged()
        {
            NewEntity.Context = await richTextCreateRef.GetHtmlAsync();
        }

        public string ShowCreationTime(BlogPostItemDto post)
        {
            return $"貼文時間: {post.CreationTime:f}";
        }
        protected override async Task OpenCreateModalAsync()
        {
            NewEntity.Id = Guid.NewGuid();
            NewEntity.Blog = Blog;

            if (CurrentTenant != null && CurrentTenant.Id.HasValue)
            {
                NewEntity.Tenant = new TenantDto();
                NewEntity.Tenant.Id = CurrentTenant.Id.Value;
            }

            NewEntity.Author = new SystemManagement.UserInforamtionDto();
            NewEntity.Author.Id = CurrentUser.Id.Value;
            NewEntity.Author.Surname = CurrentUser.SurName;
            NewEntity.Author.Name = CurrentUser.Name;

            NewEntity.CreationTime = DateTime.Now;
            NewEntity.CreatorId = CurrentUser.Id;

            await CreateModal.Show();
        }

        protected override async Task OpenEditModalAsync(BlogPostItemDto entity)
        {
            EditingEntity = entity;
            EditingEntityId = entity.Id;
            await richTextEditRef.SetHtmlAsync(entity.Context);
            await EditModal.Show();
        }

        protected override async Task UpdateEntityAsync()
        {
            await AppService.UpdateAsync(EditingEntityId, EditingEntity);
            await CloseEditModalAsync();
        }

        public async Task OnContentChanged_Edit()
        {
            EditingEntity.Context = await richTextEditRef.GetHtmlAsync();
        }

        public async Task OnCreateChanged()
        {
            NewEntity.Context = await richTextCreateRef.GetHtmlAsync();
        }


    }
}
