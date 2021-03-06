
using Newtonsoft.Json.Linq;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.SystemManagement
{
    /// <summary>
    /// 網站檔案儲存庫資料轉換物件
    /// </summary>
    [Serializable]
    public class FileStoreDto : ExtensibleEntityDto<Guid>, ITransientDependency
    {
        /// <summary>
        /// 檔案名稱
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 副檔名
        /// </summary>
        public string ExtName { get; set; }
        /// <summary>
        /// 檔案描述說明
        /// </summary>    
        public string Description
        {
            get
            {
                object val = this.GetProperty(nameof(Description));

                if (val is string)
                {
                    return (string)val;
                }
                else
                {
                    if (val is JsonElement)
                    {
                        return JsonSerializer.Deserialize<string>((JsonElement)val);
                    }
                    else
                    {
                        return null;
                    }
                }

            }
            set
            {
                this.SetProperty(nameof(Description), value);
            }
        }
        /// <summary>
        /// 檔案大小
        /// </summary>
        public long Size { get; set; }
        /// <summary>
        /// 檔案媒體類型
        /// </summary>        
        public MIMETypesDto MIMETypes { get; set; } = new MIMETypesDto();

        /// <summary>
        /// 作者
        /// </summary>
        [JsonIgnore]
        public string Creator { get; set; }
        /// <summary>
        /// 作者使用者識別碼
        /// </summary>
        public Guid CreatorId { get; set; }
        /// <summary>
        /// 建立時間
        /// </summary>       
        public DateTime CreatorDate { get; set; } = DateTime.Now;
        /// <summary>
        /// 修改者
        /// </summary>
        [JsonIgnore]
        public string Modifier { get; set; }
        /// <summary>
        /// 最後修改者識別碼
        /// </summary>
        public Guid? ModifierId { get; set; }
        /// <summary>
        /// 最後修改時間
        /// </summary>
        public DateTime? ModifierDate { get; set; }

        /// <summary>
        /// 是否為公開檔案?
        /// </summary>
        public bool IsPublic
        {
            get; set;
        }

        /// <summary>
        /// 檔案內容物件
        /// </summary>
        [JsonIgnore]
        public BlobStoreObject Blob
        {
            get; set;
        } = new BlobStoreObject();

        /// <summary>
        /// 網芳儲存資訊物件
        /// </summary>
        public SMBStoreInformation ShareInformation
        {
            get
            {
                object val = this.GetProperty(nameof(ShareInformation));

                if (val is SMBStoreInformation)
                {
                    return (SMBStoreInformation)val;
                }
                else
                {
                    if (val is JsonElement)
                    {
                        return JsonSerializer.Deserialize<SMBStoreInformation>((JsonElement)val);
                    }
                    else
                    {
                        return null;
                    }
                }

            }
            set
            {
                this.SetProperty(nameof(ShareInformation), value);
            }
        }

        /// <summary>
        /// 關聯的外掛模組資訊
        /// </summary>
        [JsonIgnore]
        public PluginModuleDto Plugin { get; set; }

        /// <summary>
        /// 檔案所屬分類
        /// </summary>
        public FileClassificationDto Classification { get; set; }



    }
}
