using Ediux.HomeSystem.SystemManagement;


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

namespace Ediux.HomeSystem
{
    public class HomeSystemTestDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        protected readonly ICurrentTenant _currentTenant;
        protected readonly IGuidGenerator _guidGenerator;
        protected readonly IRepository<DashboardWidgets> _dashboardWidgets;
        protected readonly IRepository<DashboardWidgetUsers> _dashboardWidgetUsers;
        protected readonly IRepository<IdentityUser, Guid> _appUserRepository;
        protected readonly IRepository<MIMEType, int> _mimeTypeRepository;

        public HomeSystemTestDataSeedContributor(ICurrentTenant currentTenant,
            IGuidGenerator guidGenerator,
            IRepository<DashboardWidgets> dashboardWidgets,
            IRepository<DashboardWidgetUsers> dashboardWidgetUsers,
            IRepository<IdentityUser, Guid> appUserRepository,
            IRepository<MIMEType, int> mimeTypeRepository)
        {
            _currentTenant = currentTenant;
            _guidGenerator = guidGenerator;
            _appUserRepository = appUserRepository;
            _dashboardWidgets = dashboardWidgets;
            _dashboardWidgetUsers = dashboardWidgetUsers;
            _mimeTypeRepository = mimeTypeRepository;
        }

        [UnitOfWork]
        public async Task SeedAsync(DataSeedContext context)
        {
            /* Seed additional test data... */
            using (_currentTenant.Change(context?.TenantId))
            {
                await CreateDashboardDataAsync();
                await CreateMIMETypeDataAsync();
            }
        }

        private readonly static MIMEType[] defaultMIME = new MIMEType[] {
                new MIMEType(){ TypeName = "text/plain",RefenceExtName=".txt", Description="Text File" },
                new MIMEType(){ TypeName = "text/css",RefenceExtName=".css", Description="Style Sheet File" },
                new MIMEType(){ TypeName = "text/html",RefenceExtName=".html", Description="HTML File" },
                new MIMEType(){ TypeName = "text/html",RefenceExtName=".htm", Description="HTML File" },
                new MIMEType(){ TypeName = "text/javascript",RefenceExtName=".js", Description="Java Script File" },
                new MIMEType(){ TypeName = "audio/vnd.wave",RefenceExtName=".wav", Description="PCM Audio File" },
                new MIMEType(){ TypeName = "audio/ogg",RefenceExtName=".ogg", Description="OGG Format Video File" },
                new MIMEType(){ TypeName = "application/json",RefenceExtName=".json", Description="JavaScript Object Notation" },
                new MIMEType(){ TypeName = "application/octet-stream",RefenceExtName="*", Description="Binary File" },
                new MIMEType(){ TypeName = "application/pdf",RefenceExtName=".pdf", Description="Portable Document Format" },
                new MIMEType(){ TypeName = "application/postscript",RefenceExtName=".ps", Description="Post Script File" },
                new MIMEType(){ TypeName = "application/font-woff",RefenceExtName=".woff", Description="Web Open Font Format" },
                new MIMEType(){ TypeName = "application/xhtml+xml",RefenceExtName=".xml", Description="XML File" },
                new MIMEType(){ TypeName = "application/xml",RefenceExtName=".xml", Description="XML File" },
                new MIMEType(){ TypeName = "application/zip",RefenceExtName=".zip", Description="ZIP Format Compression File" },
                new MIMEType(){ TypeName = "application/x-7z-compressed",RefenceExtName=".7z", Description="7-Zip Format Compression File" },
                new MIMEType(){ TypeName = "application/gzip",RefenceExtName=".gz", Description="GZip Format Compression File" },
                new MIMEType(){ TypeName = "audio/mp4",RefenceExtName=".mp4", Description="MPEG 4 Audio File" },
                new MIMEType(){ TypeName = "audio/mpeg",RefenceExtName=".mp3", Description="MPEG 3 Audio File" },
                new MIMEType(){ TypeName = "audio/vnd.rn-realaudio",RefenceExtName=".ra", Description="Real Audio File" },
                new MIMEType(){ TypeName = "audio/webm",RefenceExtName=".webm", Description="Matroska Format Media File" },
                new MIMEType(){ TypeName = "audio/flac",RefenceExtName=".flac", Description="FLAC Audio File" },
                new MIMEType(){ TypeName = "image/gif",RefenceExtName=".gif", Description="GIF Image File" },
                new MIMEType(){ TypeName = "image/jpeg",RefenceExtName=".jpg", Description="JPEG Image File" },
                new MIMEType(){ TypeName = "image/png",RefenceExtName=".png", Description="PNG Image File" },
                new MIMEType(){ TypeName = "image/webp",RefenceExtName=".webp", Description="WebP Image File" },
                new MIMEType(){ TypeName = "image/svg+xml",RefenceExtName=".svg", Description="SVG向量圖檔" },
                new MIMEType(){ TypeName = "image/tiff",RefenceExtName=".tiff", Description="TIFF圖檔" },
                new MIMEType(){ TypeName = "image/icon",RefenceExtName=".icon", Description="Icon File" },
                new MIMEType(){ TypeName = "video/mpeg",RefenceExtName=".mpeg", Description="MPEG-1影片檔案" },
                new MIMEType(){ TypeName = "video/mp4",RefenceExtName=".mp4", Description="MP4影片檔案" },
                new MIMEType(){ TypeName = "video/ogg",RefenceExtName=".ogg", Description="Ogg影片檔案" },
                new MIMEType(){ TypeName = "video/quicktime",RefenceExtName=".qt", Description="QuickTime影片檔案" },
                new MIMEType(){ TypeName = "video/quicktime",RefenceExtName=".mov", Description="QuickTime影片檔案" },
                new MIMEType(){ TypeName = "video/webm",RefenceExtName=".webm", Description="WebM影片檔案" },
                new MIMEType(){ TypeName = "video/x-ms-wmv",RefenceExtName=".wmv", Description="Windows Media Video影片檔案" },
                new MIMEType(){ TypeName = "video/x-flv",RefenceExtName=".flv", Description="Flash Video" },
                new MIMEType(){ TypeName = "application/x-rar-compressed",RefenceExtName=".RAR", Description="RAR壓縮檔案" },
                new MIMEType(){ TypeName = "application/msword",RefenceExtName=".doc", Description="Word檔案(Office 97)" },
                new MIMEType(){ TypeName = "application/vnd.openxmlformats-officedocument.wordprocessingml.document",RefenceExtName=".docx", Description="Microsoft Word(2003-2010)" },
                new MIMEType(){ TypeName = "application/vnd.openxmlformats-officedocument.wordprocessingml.template",RefenceExtName=".doct", Description="Microsoft Word Template" },
                new MIMEType(){ TypeName = "application/vnd.ms-word.document.macroEnabled.12",RefenceExtName=".docm", Description="Microsoft Word(啟用巨集)" },
                new MIMEType(){ TypeName = "application/vnd.ms-word.template.macroEnabled.12",RefenceExtName=".dotm", Description="Microsoft Word 範本(啟用巨集)" },
                new MIMEType(){ TypeName = "application/vnd.ms-excel",RefenceExtName=".xls", Description="Microsoft Excel(Office 97)" },
                new MIMEType(){ TypeName = "application/vnd.ms-excel",RefenceExtName=".xlt", Description="Microsoft Excel" },
                new MIMEType(){ TypeName = "application/vnd.ms-excel",RefenceExtName=".xla", Description="Microsoft Excel" },
                new MIMEType(){ TypeName = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",RefenceExtName=".xlsx", Description="Microsoft Excel(2003-2010)" },
                new MIMEType(){ TypeName = "application/vnd.openxmlformats-officedocument.spreadsheetml.template",RefenceExtName=".xltx", Description="Microsoft Excel 範本" },
                new MIMEType(){ TypeName = "application/vnd.ms-powerpoint",RefenceExtName=".ppt", Description="Microsoft Power Point(Office 97)" },
                new MIMEType(){ TypeName = "application/vnd.openxmlformats-officedocument.presentationml.presentation",RefenceExtName=".pptx", Description="Microsoft Power Point(2003-2010)" },
                new MIMEType(){ TypeName = "application/vnd.ms-access",RefenceExtName=".mdb", Description="Microsoft Access Database" },
                new MIMEType(){ TypeName = "application/octet-stream",RefenceExtName=".dll", Description="Dynamic Link Library" },
            };

        public async Task CreateDashboardDataAsync()
        {
            var adminUser = (await _appUserRepository.GetQueryableAsync())
                    .Where(u => u.UserName == "admin")
                    .FirstOrDefault();

            var demo = new DashboardWidgets()
            {
                AllowMulti = false,
                DisplayName = "Test Widget",
                HasOption = false,
                IsDefault = false,
                Name = "Test_Widget",
                Order = 0
            };

            demo = await _dashboardWidgets.InsertAsync(demo);

            demo.AssginedUsers.Add(new DashboardWidgetUsers()
            {
                DashboardWidgetId = demo.Id,
                DashboardWidget = demo,
                User = adminUser,
                UserId = adminUser.Id
            });

           
        }

        public async Task CreateMIMETypeDataAsync()
        {
            await _mimeTypeRepository.InsertManyAsync(defaultMIME);
        }
    }
}