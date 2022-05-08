using Ediux.HomeSystem.Features.Blogs;
using Ediux.HomeSystem.Features.Blogs.DTOs;

using NSubstitute;

using System;
using System.Threading.Tasks;

using Volo.Abp.Domain.Repositories;
using Volo.Abp.MultiTenancy;
using Volo.Abp.TenantManagement;

using Xunit;

namespace Ediux.HomeSystem.Features.Blogs
{
    public class BlogPostAppServiceTests : HomeSystemApplicationTestBase
    {
        private readonly IBlogPostAppService _blogPostAppService;
        private readonly IBlogsAppServices _blogsAppServices;
        private readonly ICurrentTenant currentTenant;
        private Guid PostId;

        public BlogPostAppServiceTests()
        {
            this._blogPostAppService = GetService<IBlogPostAppService>() ?? CreateService();
            this._blogsAppServices = GetService<IBlogsAppServices>();
            currentTenant = GetService<ICurrentTenant>();
        }

        private BlogPostAppService CreateService()
        {
            return new BlogPostAppService(
                GetService<IRepository<BlogPosts, Guid>>());
        }

        [Fact]
        public async Task GetListByBlogAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            BlogPostSearchRequestDto input = new BlogPostSearchRequestDto() { BlogId = HomeSystemTestConsts.TestBlogId };

            // Act
            var result = await _blogPostAppService.GetListByBlogAsync(input);

            // Assert
            Assert.NotEmpty(result.Items);
        }

        [Fact]
        public async Task CreateAsync_StateUnderTest_ExpectedBehavior()
        {
            BlogItemDto testBlog = await _blogsAppServices.GetAsync(HomeSystemTestConsts.TestBlogId);

            Assert.NotNull(testBlog);

            var userInfo = new SystemManagement.UserInforamtionDto() { Id = HomeSystemTestConsts.NormalUserId, UserName = "user", Surname = "user", Name = "abp" };
            TenantDto tenant = null;

            if (currentTenant.Id.HasValue)
            {
                tenant = new TenantDto();
                tenant.Id = currentTenant.Id.Value;
                tenant.Name = currentTenant.Name;
            }

            BlogPostItemDto input = new BlogPostItemDto()
            {
                Author = userInfo,
                CreatorId = HomeSystemTestConsts.NormalUserId,
                Blog = testBlog,
                Creator = userInfo,
                Context = "Test context",
                CreationTime = DateTime.Now,
                ShortDescription = "testing",
                Slug = "test",
                Tenant = tenant,
                Title = "Test Post"
            };

            var result = await _blogPostAppService.CreateAsync(input);

            PostId = result.Id;

            Assert.NotNull(result);

        }

        [Fact]
        public async Task UpdateAsync_StateUnderTest_ExpectedBehavior()
        {
            BlogItemDto testBlog = await _blogsAppServices.GetAsync(HomeSystemTestConsts.TestBlogId);

            Assert.NotNull(testBlog);

            var userInfo = new SystemManagement.UserInforamtionDto() { Id = HomeSystemTestConsts.NormalUserId, UserName = "user", Surname = "user", Name = "abp" };
            TenantDto tenant = null;

            if (currentTenant.Id.HasValue)
            {
                tenant = new TenantDto();
                tenant.Id = currentTenant.Id.Value;
                tenant.Name = currentTenant.Name;
            }

            BlogPostItemDto input = new BlogPostItemDto()
            {
                Id = Guid.NewGuid(),
                Author = userInfo,
                CreatorId = HomeSystemTestConsts.NormalUserId,
                Blog = testBlog,
                Creator = userInfo,
                Context = "Test context",
                CreationTime = DateTime.Now,
                ShortDescription = "testing",
                Slug = "test",
                Tenant = tenant,
                Title = "Test Post"
            };

            var result = await _blogPostAppService.CreateAsync(input);

            Assert.NotNull(result);

            string ori = result.Context;

            result.Context = "testing";
           
            result = await _blogPostAppService.UpdateAsync(result.Id, result);

            Assert.NotEqual(ori, result.Context);

        }
   
        [Fact]
        public async Task DeleteAsync_StateUnderTest_ExpectedBehavior()
        {
            BlogItemDto testBlog = await _blogsAppServices.GetAsync(HomeSystemTestConsts.TestBlogId);

            Assert.NotNull(testBlog);

            var userInfo = new SystemManagement.UserInforamtionDto() { Id = HomeSystemTestConsts.NormalUserId, UserName = "user", Surname = "user", Name = "abp" };
            TenantDto tenant = null;

            if (currentTenant.Id.HasValue)
            {
                tenant = new TenantDto();
                tenant.Id = currentTenant.Id.Value;
                tenant.Name = currentTenant.Name;
            }

            BlogPostItemDto input = new BlogPostItemDto()
            {
                Author = userInfo,
                CreatorId = HomeSystemTestConsts.NormalUserId,
                Blog = testBlog,
                Creator = userInfo,
                Context = "Test context",
                CreationTime = DateTime.Now,
                ShortDescription = "testing",
                Slug = "test",
                Tenant = tenant,
                Title = "Test Post"
            };

            var result = await _blogPostAppService.CreateAsync(input);

            await _blogPostAppService.DeleteAsync(result.Id);

            try
            {
                BlogPostItemDto post = await _blogPostAppService.GetAsync(result.Id);
                Assert.True(false);
            }
            catch (Exception)
            {

                Assert.True(true);
            }
            
            

            
        }
    }
}
