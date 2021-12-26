using Ediux.HomeSystem.Data;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.MultiTenancy;

namespace Ediux.HomeSystem.DataSeeder
{
    public class MIMETypeDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<MIMEType, int> _mimeTypeRepository;
        private readonly ICurrentTenant _currentTenant;

        public MIMETypeDataSeedContributor(IRepository<MIMEType, int> mimeTypeRepository,
            ICurrentTenant currentTenant)
        {
            _mimeTypeRepository = mimeTypeRepository;
            _currentTenant = currentTenant;
        }
        private readonly static MIMEType[] defaultMIME = new MIMEType[] {
                new MIMEType(){ MIME = "text/plain",RefenceExtName=".txt", Description="Text File" },
                new MIMEType(){ MIME = "text/css",RefenceExtName=".css", Description="Style Sheet File" },
                new MIMEType(){ MIME = "text/html",RefenceExtName=".html", Description="HTML File" },
                new MIMEType(){ MIME = "text/html",RefenceExtName=".htm", Description="HTML File" },
                new MIMEType(){ MIME = "text/javascript",RefenceExtName=".js", Description="Java Script File" },
                new MIMEType(){ MIME = "audio/vnd.wave",RefenceExtName=".wav", Description="PCM Audio File" },
                new MIMEType(){ MIME = "audio/ogg",RefenceExtName=".ogg", Description="OGG Format Video File" },
                new MIMEType(){ MIME = "application/json",RefenceExtName=".json", Description="JavaScript Object Notation" },
                new MIMEType(){ MIME = "application/octet-stream",RefenceExtName="*", Description="Binary File" },
                new MIMEType(){ MIME = "application/pdf",RefenceExtName=".pdf", Description="Portable Document Format" },
                new MIMEType(){ MIME = "application/postscript",RefenceExtName=".ps", Description="Post Script File" },
                new MIMEType(){ MIME = "application/font-woff",RefenceExtName=".woff", Description="Web Open Font Format" },
                new MIMEType(){ MIME = "application/xhtml+xml",RefenceExtName=".xml", Description="XML File" },
                new MIMEType(){ MIME = "application/xml",RefenceExtName=".xml", Description="XML File" },
                new MIMEType(){ MIME = "application/zip",RefenceExtName=".zip", Description="ZIP Format Compression File" },
                new MIMEType(){ MIME = "application/x-7z-compressed",RefenceExtName=".7z", Description="7-Zip Format Compression File" },
                new MIMEType(){ MIME = "application/gzip",RefenceExtName=".gz", Description="GZip Format Compression File" },
                new MIMEType(){ MIME = "audio/mp4",RefenceExtName=".mp4", Description="MPEG 4 Audio File" },
                new MIMEType(){ MIME = "audio/mpeg",RefenceExtName=".mp3", Description="MPEG 3 Audio File" },
                new MIMEType(){ MIME = "audio/vnd.rn-realaudio",RefenceExtName=".ra", Description="Real Audio File" },
                new MIMEType(){ MIME = "audio/webm",RefenceExtName=".webm", Description="Matroska Format Media File" },
                new MIMEType(){ MIME = "audio/flac",RefenceExtName=".flac", Description="FLAC Audio File" },
                new MIMEType(){ MIME = "image/gif",RefenceExtName=".gif", Description="GIF Image File" },
                new MIMEType(){ MIME = "image/jpeg",RefenceExtName=".jpg", Description="JPEG Image File" },
                new MIMEType(){ MIME = "image/png",RefenceExtName=".png", Description="PNG Image File" },
                new MIMEType(){ MIME = "image/webp",RefenceExtName=".webp", Description="WebP Image File" },
                new MIMEType(){ MIME = "image/svg+xml",RefenceExtName=".svg", Description="SVG向量圖檔" },
                new MIMEType(){ MIME = "image/tiff",RefenceExtName=".tiff", Description="TIFF圖檔" },
                new MIMEType(){ MIME = "image/icon",RefenceExtName=".icon", Description="Icon File" },
                new MIMEType(){ MIME = "video/mpeg",RefenceExtName=".mpeg", Description="MPEG-1影片檔案" },
                new MIMEType(){ MIME = "video/mp4",RefenceExtName=".mp4", Description="MP4影片檔案" },
                new MIMEType(){ MIME = "video/ogg",RefenceExtName=".ogg", Description="Ogg影片檔案" },
                new MIMEType(){ MIME = "video/quicktime",RefenceExtName=".", Description="QuickTime影片檔案" },
                new MIMEType(){ MIME = "video/webm",RefenceExtName=".webm", Description="WebM影片檔案" },
                new MIMEType(){ MIME = "video/x-ms-wmv",RefenceExtName=".wmv", Description="Windows Media Video影片檔案" },
                new MIMEType(){ MIME = "video/x-flv",RefenceExtName=".flv", Description="Flash Video" },
                new MIMEType(){ MIME = "application/x-rar-compressed",RefenceExtName=".RAR", Description="RAR壓縮檔案" },
                new MIMEType(){ MIME = "application/msword",RefenceExtName=".doc", Description="Word檔案(Office 97)" },
                new MIMEType(){ MIME = "application/vnd.openxmlformats-officedocument.wordprocessingml.document",RefenceExtName=".docx", Description="Microsoft Word(2003-2010)" },
                new MIMEType(){ MIME = "application/vnd.openxmlformats-officedocument.wordprocessingml.template",RefenceExtName=".doct", Description="Microsoft Word Template" },
                new MIMEType(){ MIME = "application/vnd.ms-word.document.macroEnabled.12",RefenceExtName=".docm", Description="Microsoft Word(啟用巨集)" },
                new MIMEType(){ MIME = "application/vnd.ms-word.template.macroEnabled.12",RefenceExtName=".dotm", Description="Microsoft Word 範本(啟用巨集)" },
                new MIMEType(){ MIME = "application/vnd.ms-excel",RefenceExtName=".xls", Description="Microsoft Excel(Office 97)" },
                new MIMEType(){ MIME = "application/vnd.ms-excel",RefenceExtName=".xlt", Description="Microsoft Excel" },
                new MIMEType(){ MIME = "application/vnd.ms-excel",RefenceExtName=".xla", Description="Microsoft Excel" },
                new MIMEType(){ MIME = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",RefenceExtName=".xlsx", Description="Microsoft Excel(2003-2010)" },
                new MIMEType(){ MIME = "application/vnd.openxmlformats-officedocument.spreadsheetml.template",RefenceExtName=".xltx", Description="Microsoft Excel 範本" },
                new MIMEType(){ MIME = "application/vnd.ms-powerpoint",RefenceExtName=".ppt", Description="Microsoft Power Point(Office 97)" },
                new MIMEType(){ MIME = "application/vnd.openxmlformats-officedocument.presentationml.presentation",RefenceExtName=".pptx", Description="Microsoft Power Point(2003-2010)" },
                new MIMEType(){ MIME = "application/vnd.ms-access",RefenceExtName=".mdb", Description="Microsoft Access Database" },
                new MIMEType(){ MIME = "application/octet-stream",RefenceExtName=".dll", Description="Dynamic Link Library" },
            };
        public async Task SeedAsync(DataSeedContext context)
        {
            using (_currentTenant.Change(context?.TenantId))
            {
                var mimeExists = (await _mimeTypeRepository.GetQueryableAsync())                    
                                  .Select(x => x.RefenceExtName).ToList();

                var defaultExtMIME = (defaultMIME.Select(s => s.RefenceExtName).ToList());

                var addMIME = defaultExtMIME.Except(mimeExists).ToList();

                if (addMIME.Count == 0)
                {
                    return;
                }

                var addData = (from d in defaultMIME
                               join x in addMIME on d.RefenceExtName equals x
                               select d).ToList();

                await _mimeTypeRepository.InsertManyAsync(addData);
            }

        }
    }
}
