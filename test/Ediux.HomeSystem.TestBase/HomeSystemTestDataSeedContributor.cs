using Ediux.HomeSystem.AdditionalSystemFunctions4Users;
using Ediux.HomeSystem.SystemManagement;

using Microsoft.AspNetCore.Components;

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
    protected readonly UnitOfWorkManager _unitOfWorkManager;

    protected readonly ICurrentTenant _currentTenant;
    protected readonly ICurrentUser _currentUser;
    protected readonly IGuidGenerator _guidGenerator;
    protected readonly IRepository<DashboardWidgets> _dashboardWidgets;
    protected readonly IRepository<DashboardWidgetUsers> _dashboardWidgetUsers;
    protected readonly IRepository<IdentityUser, Guid> _appUserRepository;
    protected readonly IRepository<MIMEType, int> _mimeTypeRepository;
    protected readonly IRepository<FileStoreClassification, Guid> _fileStoreClassificationRepository;
    protected readonly IRepository<File_Store, Guid> _fileStoreRepository;
    protected readonly IRepository<AbpPlugins, Guid> _pluginsRepository;
    protected readonly IRepository<InternalSystemMessages, Guid> _systemMessageRepository;
    protected readonly IRepository<PersonalCalendar, Guid> _calendarRepository;
    protected readonly IRepository<ProductKeys, Guid> _productKeyRepository;
    protected readonly IRepository<UserPasswordStore, long> _userPasswordStoreRepository;

    public HomeSystemTestDataSeedContributor(ICurrentTenant currentTenant,
          ICurrentUser currentUser,
          IGuidGenerator guidGenerator,
          IRepository<DashboardWidgets> dashboardWidgets,
          IRepository<DashboardWidgetUsers> dashboardWidgetUsers,
          IRepository<IdentityUser, Guid> appUserRepository,
          IRepository<MIMEType, int> mimeTypeRepository,
          IRepository<FileStoreClassification, Guid> fileStoreClassificationRepository,
          IRepository<File_Store, Guid> fileStoreRepository,
          IRepository<AbpPlugins, Guid> pluginsRepository,
          IRepository<PersonalCalendar, Guid> calendarRepository,
          IRepository<InternalSystemMessages, Guid> systemMessageRepository,
          IRepository<ProductKeys, Guid> productKeyRepository,
          IRepository<UserPasswordStore, long> userPasswordStoreRepository,
          UnitOfWorkManager unitOfWorkManager)
    {
        _currentUser = currentUser;
        _currentTenant = currentTenant;
        _guidGenerator = guidGenerator;
        _appUserRepository = appUserRepository;
        _dashboardWidgets = dashboardWidgets;
        _dashboardWidgetUsers = dashboardWidgetUsers;
        _mimeTypeRepository = mimeTypeRepository;
        _fileStoreClassificationRepository = fileStoreClassificationRepository;
        _fileStoreRepository = fileStoreRepository;
        _systemMessageRepository = systemMessageRepository;
        _pluginsRepository = pluginsRepository;
        _calendarRepository = calendarRepository;
        _productKeyRepository = productKeyRepository;
        _userPasswordStoreRepository = userPasswordStoreRepository;
        _unitOfWorkManager = unitOfWorkManager;
    }


    public async Task SeedAsync(DataSeedContext context)
    {
        /* Seed additional test data... */
        await CreateDashboardDataAsync();
    }



    public async Task CreateDashboardDataAsync()
    {


        //var adminUser = await _appUserRepository.InsertAsync(new IdentityUser(Guid.Parse("2e701e62-0953-4dd3-910b-dc6cc93ccb0d"), "admin", "admin@abp.io"));
        //await _unitOfWorkManager.Current.SaveChangesAsync();
        var testuser = await _appUserRepository.FindAsync(p => p.Id == _currentUser.Id);
        
        if(testuser == null)
        {
            await _appUserRepository.InsertAsync(new IdentityUser(Guid.Parse("2e701e62-0953-4dd3-910b-dc6cc93ccb0d"), "admin", "admin@abp.io"));
            await _unitOfWorkManager.Current.SaveChangesAsync();
        }

        DateTime systemTime = DateTime.Now;

        FileStoreClassification classification = new FileStoreClassification()
        {
            CreationTime = DateTime.Now,
            CreatorId = _currentUser.Id,
            Name = "Testing"
        };

        classification = await _fileStoreClassificationRepository.InsertAsync(classification);
        await _unitOfWorkManager.Current.SaveChangesAsync();

        //新增擴充模組測試資料
        var mimetype = await _mimeTypeRepository.FindAsync(s => s.RefenceExtName == ".dll");

        if (mimetype != null)
        {
            var pluginsClassification = await _fileStoreClassificationRepository.FindAsync(p => p.Name == "Plugins");

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
        }

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

        demo = await _dashboardWidgets.InsertAsync(demo);

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
                FileClassificationId = classification.Id,
                IsPublic = false,
                Size = 1440
            },
            SystemMessage = internalSystemMessages
        });

        internalSystemMessages = await _systemMessageRepository.InsertAsync(internalSystemMessages);

        await _unitOfWorkManager.Current.SaveChangesAsync();

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

        personalCalendar = await _calendarRepository.InsertAsync(personalCalendar);

        ProductKeys productKeys = new ProductKeys()
        {
            CreatorId = _currentUser.Id,
            CreationTime = systemTime,
            ProductKey = "dasdfsdfsdfsd",
            ProductName = "test",
            Shared = false
        };

        productKeys = await _productKeyRepository.InsertAsync(productKeys);

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

        userPasswordStore = await _userPasswordStoreRepository.InsertAsync(userPasswordStore);

        await _unitOfWorkManager.Current.SaveChangesAsync();
    }


}
