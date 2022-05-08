using Ediux.HomeSystem.AdditionalSystemFunctions4Users;
using Ediux.HomeSystem.Features.Blogs;
using Ediux.HomeSystem.SystemManagement;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Linq;
using System.Threading.Tasks;

using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;
using Volo.Abp.Users;

namespace Ediux.HomeSystem;

public class HomeSystemTestDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    protected readonly IServiceProvider _serviceProvider;
    protected readonly UnitOfWorkManager _unitOfWorkManager;

    protected readonly ICurrentTenant _currentTenant;
    protected readonly ICurrentUser _currentUser;
    protected readonly IGuidGenerator _guidGenerator;

    public HomeSystemTestDataSeedContributor(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _currentUser = _serviceProvider.GetRequiredService<ICurrentUser>();
        _currentTenant = _serviceProvider.GetRequiredService<ICurrentTenant>();
        _guidGenerator = _serviceProvider.GetRequiredService<IGuidGenerator>();
        _unitOfWorkManager = _serviceProvider.GetRequiredService<UnitOfWorkManager>();
    }


    public async Task SeedAsync(DataSeedContext context)
    {
        /* Seed additional test data... */
        await CreateAbpUsers_Roles_PolicyDataAsync();
        await CreateFileStoreDataAsync();
        await CreatePluginsDataAsync();
        await CreateSystemMessageAsync();
        await CreateDashboardDataAsync();
        await CreateProductKeysStoreDataAsync();
        await CreatePersonalCalendarDataAsync();
        await CreatePasswordStoreDataAsync();
        await CreateBlogDataAsync();
    }

    public async Task CreateAbpUsers_Roles_PolicyDataAsync()
    {
        IRepository<IdentityUser, Guid> _appUserRepository = _serviceProvider.GetRequiredService<IRepository<IdentityUser, Guid>>();

        var testuser = await _appUserRepository.FindAsync(p => p.Id == HomeSystemTestConsts.AdminUserId);

        if (testuser == null)
        {
            await _appUserRepository.InsertAsync(new IdentityUser(HomeSystemTestConsts.AdminUserId, "admin", "admin@abp.io"));
            await _unitOfWorkManager.Current.SaveChangesAsync();
        }

        var normalUser = await _appUserRepository.FindAsync(p => p.Id == HomeSystemTestConsts.NormalUserId);

        if (testuser == null)
        {
            await _appUserRepository.InsertAsync(new IdentityUser(HomeSystemTestConsts.NormalUserId, "user", "user@abp.io"));
            await _unitOfWorkManager.Current.SaveChangesAsync();
        }

    }

    public async Task CreateFileStoreDataAsync()
    {
        IRepository<MIMEType, int> _mimeTypeRepository = _serviceProvider.GetRequiredService<IRepository<MIMEType, int>>();
        IRepository<FileStoreClassification, Guid> _fileStoreClassificationRepository = _serviceProvider.GetRequiredService<IRepository<FileStoreClassification, Guid>>();
        IRepository<File_Store, Guid> _fileStoreRepository = _serviceProvider.GetRequiredService<IRepository<File_Store, Guid>>();

        DateTime systemTime = DateTime.Now;

        FileStoreClassification classification = await CreateFileClassificationDataAsync(_fileStoreClassificationRepository, "Testing");

        //新增一般檔案
        var mimetype_txt = await _mimeTypeRepository.FindAsync(s => s.RefenceExtName == ".txt");

        if (mimetype_txt != null)
        {
            File_Store file_Store = new File_Store()
            {
                Name = "Test",
                MIMETypeId = mimetype_txt.Id,
                BlobContainerName = "cms-kit-media",
                CreatorId = _currentUser.Id,
                CreationTime = DateTime.Now,
                FileClassificationId = classification.Id,
                IsPublic = false,
                Size = 52
            };

            await _fileStoreRepository.InsertAsync(file_Store);
        }

        await _unitOfWorkManager.Current.SaveChangesAsync();

    }

    public async Task<FileStoreClassification> CreateFileClassificationDataAsync(IRepository<FileStoreClassification, Guid> _fileStoreClassificationRepository, string classificationName)
    {
        FileStoreClassification classification = await _fileStoreClassificationRepository.FindAsync(p => p.Name == classificationName);

        if (classification == null)
        {
            classification = new FileStoreClassification()
            {
                CreationTime = DateTime.Now,
                CreatorId = _currentUser.Id,
                Name = classificationName
            };

            classification = await _fileStoreClassificationRepository.InsertAsync(classification);
            await _unitOfWorkManager.Current.SaveChangesAsync();
        }

        return classification;
    }

    public async Task CreatePluginsDataAsync()
    {
        IRepository<AbpPlugins, Guid> _pluginsRepository = _serviceProvider.GetRequiredService<IRepository<AbpPlugins, Guid>>();
        IRepository<MIMEType, int> _mimeTypeRepository = _serviceProvider.GetRequiredService<IRepository<MIMEType, int>>();
        IRepository<FileStoreClassification, Guid> _fileStoreClassificationRepository = _serviceProvider.GetRequiredService<IRepository<FileStoreClassification, Guid>>();

        DateTime systemTime = DateTime.Now;

        //新增擴充模組測試資料
        var mimetype = await _mimeTypeRepository.FindAsync(s => s.RefenceExtName == ".dll");

        if (mimetype != null)
        {
            var pluginsClassification = await CreateFileClassificationDataAsync(_fileStoreClassificationRepository, "Plugins");

            AbpPlugins abpPlugins = new AbpPlugins()
            {
                AssemblyName = "Test.dll",
                Disabled = true,
                Name = "Test",
                CreatorId = _currentUser.Id,
                CreationTime = systemTime,
                RefFileInstance = new File_Store()
                {
                    Name = "Test",
                    MIMETypeId = mimetype.Id,
                    BlobContainerName = "plugins",
                    CreatorId = _currentUser.Id,
                    CreationTime = DateTime.Now,
                    FileClassificationId = pluginsClassification.Id,
                    IsPublic = false,
                    Size = 1440
                }
            };

            abpPlugins = await _pluginsRepository.InsertAsync(abpPlugins);
            await _unitOfWorkManager.Current.SaveChangesAsync();
        }
    }

    public async Task CreateSystemMessageAsync()
    {
        IRepository<InternalSystemMessages, Guid> _systemMessageRepository = _serviceProvider.GetRequiredService<IRepository<InternalSystemMessages, Guid>>();
        IRepository<MIMEType, int> _mimeTypeRepository = _serviceProvider.GetRequiredService<IRepository<MIMEType, int>>();
        IRepository<FileStoreClassification, Guid> _fileStoreClassificationRepository = _serviceProvider.GetRequiredService<IRepository<FileStoreClassification, Guid>>();

        DateTime systemTime = DateTime.Now;
        var mimetype = await _mimeTypeRepository.FindAsync(s => s.RefenceExtName == ".dll");

        if (mimetype != null)
        {
            var pluginsClassification = await CreateFileClassificationDataAsync(_fileStoreClassificationRepository, "Plugins");

            if (pluginsClassification != null)
            {
                InternalSystemMessages internalSystemMessages = new InternalSystemMessages()
                {
                    ActionCallbackURL = "https://localhost:44307",
                    CreationTime = systemTime,
                    CreatorId = _currentUser.Id,
                    FromUserId = _currentUser.Id.Value,
                    IsBCC = false,
                    IsCC = false,
                    IsEMail = false,
                    IsPush = false,
                    IsRead = false,
                    IsReply = false,
                    Message = "test",
                    Subject = "test",
                };

                internalSystemMessages.AttachFiles.Add(new AttachFile()
                {
                    File = new File_Store()
                    {
                        Name = "Test",
                        MIMETypeId = mimetype.Id,
                        BlobContainerName = "cms-kit-media",
                        CreatorId = _currentUser.Id,
                        CreationTime = DateTime.Now,
                        FileClassificationId = pluginsClassification.Id,
                        IsPublic = false,
                        Size = 1440
                    },
                    SystemMessage = internalSystemMessages
                });

                internalSystemMessages = await _systemMessageRepository.InsertAsync(internalSystemMessages);
            }
        }

        // 新增系統訊息
        await _systemMessageRepository.InsertAsync(new InternalSystemMessages()
        {
            ActionCallbackURL = "https://localhost:44307",
            CreationTime = systemTime,
            CreatorId = _currentUser.Id,
            FromUserId = _currentUser.Id.Value,
            IsBCC = false,
            IsCC = false,
            IsEMail = false,
            IsPush = false,
            IsRead = false,
            IsReply = false,
            Message = "test",
            Subject = "test",
        });

        await _unitOfWorkManager.Current.SaveChangesAsync();
    }

    public async Task CreateDashboardDataAsync()
    {

        IRepository<DashboardWidgets> _dashboardWidgets = _serviceProvider.GetRequiredService<IRepository<DashboardWidgets>>();

        DateTime systemTime = DateTime.Now;

        var demo = new DashboardWidgets()
        {
            AllowMulti = false,
            DisplayName = "Test Widget",
            HasOption = false,
            IsDefault = false,
            Name = "Test_Widget",
            Order = 0
        };

        demo.AssginedUsers.Add(new DashboardWidgetUsers()
        {
            DashboardWidget = demo,
            UserId = _currentUser.Id.Value
        });

        await _dashboardWidgets.InsertAsync(demo);

        await _unitOfWorkManager.Current.SaveChangesAsync();


    }

    private async Task CreateProductKeysStoreDataAsync()
    {
        IRepository<ProductKeys, Guid> _productKeyRepository = _serviceProvider.GetRequiredService<IRepository<ProductKeys, Guid>>();

        DateTime systemTime = DateTime.Now;

        ProductKeys productKeys = new ProductKeys()
        {
            CreatorId = _currentUser.Id,
            CreationTime = systemTime,
            ProductKey = "dasdfsdfsdfsd",
            ProductName = "test",
            Shared = false
        };

        await _productKeyRepository.InsertAsync(productKeys);
        await _unitOfWorkManager.Current.SaveChangesAsync();
    }

    public async Task CreatePersonalCalendarDataAsync()
    {
        IRepository<PersonalCalendar, Guid> _calendarRepository = _serviceProvider.GetRequiredService<IRepository<PersonalCalendar, Guid>>();
        DateTime systemTime = DateTime.Now;

        PersonalCalendar personalCalendar = new PersonalCalendar()
        {
            EndTime = systemTime.AddDays(3),
            StartTime = systemTime,
            Color = "black",
            CreatorId = _currentUser.Id,
            CreationTime = systemTime,
            IsAllDay = false,

            SystemMessages = new InternalSystemMessages()
            {
                ActionCallbackURL = "https://localhost:44307",
                CreationTime = systemTime,
                CreatorId = _currentUser.Id,
                FromUserId = _currentUser.Id.Value,
                IsBCC = false,
                IsCC = false,
                IsEMail = false,
                IsPush = false,
                IsRead = false,
                IsReply = false,
                Message = "New event",
                Subject = "Event Test 1",
            }
        };

        await _calendarRepository.InsertAsync(personalCalendar);
        await _unitOfWorkManager.Current.SaveChangesAsync();
    }

    public async Task CreatePasswordStoreDataAsync()
    {
        IRepository<UserPasswordStore, long> _userPasswordStoreRepository = _serviceProvider.GetRequiredService<IRepository<UserPasswordStore, long>>();

        DateTime systemTime = DateTime.Now;

        UserPasswordStore userPasswordStore = new UserPasswordStore()
        {
            Account = "ttt@123.com",
            CreationTime = systemTime,
            CreatorId = _currentUser.Id,
            IsHistory = false,
            Password = "1qaz@WSX",
            Site = "http://localhost",
            SiteName = "Test"
        };

        await _userPasswordStoreRepository.InsertAsync(userPasswordStore);

        await _unitOfWorkManager.Current.SaveChangesAsync();
    }

    public async Task CreateBlogDataAsync()
    {
        IRepository<Blogs, Guid> repository = _serviceProvider.GetRequiredService<IRepository<Blogs, Guid>>();
        IRepository<BlogPosts, Guid> postRepository = _serviceProvider.GetRequiredService<IRepository<BlogPosts, Guid>>();
        IRepository<Tags, Guid> tagRepository = _serviceProvider.GetRequiredService<IRepository<Tags, Guid>>();

        Blogs blog = await repository.FindAsync(o => o.Slug == "testingblog");

        if (blog == null)
        {
            blog = await repository.InsertAsync(new Blogs()
            {
                CreatorId = HomeSystemTestConsts.NormalUserId,
                CreationTime = DateTime.Now,
                Name = "Testing Blog",
                Slug = "testingblog",
                TenantId = _currentTenant?.Id
            });

            HomeSystemTestConsts.TestBlogId = blog.Id;

            await _unitOfWorkManager.Current.SaveChangesAsync();
        }

        BlogPosts input = new BlogPosts()
        {
            AuthorId = HomeSystemTestConsts.NormalUserId,
            CreatorId = HomeSystemTestConsts.NormalUserId,
            BlogId = blog.Id,
            Content = "Test context",
            CreationTime = DateTime.Now,
            ShortDescription = "testing",
            Slug = "test",
            Title = "Test Post"
        };

        await postRepository.InsertAsync(input);
        await _unitOfWorkManager.Current.SaveChangesAsync();

        Tags[] tags = new[] {
            new Tags() {
                CreationTime = DateTime.Now,
                CreatorId = HomeSystemTestConsts.NormalUserId,
                EntityType = "BlogPost",
                Name = "TestPost"
            },
            new Tags(){
                CreationTime = DateTime.Now,
                CreatorId = HomeSystemTestConsts.NormalUserId,
                EntityType = "Photo",
                Name = "TestPhoto"
            },
            new Tags(){
                CreationTime = DateTime.Now,
                CreatorId = HomeSystemTestConsts.NormalUserId,
                EntityType = "Blog",
                Name = "TestPhoto"
            }
        };

        await tagRepository.InsertManyAsync(tags);
        await _unitOfWorkManager.Current.SaveChangesAsync();

        tags[0].Entities.Add(new EntityTags() { EntityId = blog.Id.ToString(), TagId = tags[0].Id });

        await tagRepository.UpdateAsync(tags[0]);
        await _unitOfWorkManager.Current.SaveChangesAsync();
    }
}
