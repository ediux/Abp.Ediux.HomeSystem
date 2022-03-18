using System;

namespace Ediux.HomeSystem
{
    public static class HomeSystemConsts
    {
        public const string DbTablePrefix = "HomeSystem";

        /// <summary>
        /// 常數: DbSchema
        /// </summary>
        public const string DbSchema = null;

        /// <summary>
        /// 預設的MIME Type
        /// </summary>
        public const string DefaultContentType = "application/octet-stream";
        /// <summary>
        /// 常數: Description(說明欄位名稱)
        /// </summary>
        public const string Description = "Description";
        /// <summary>
        /// 常數: IsAutoSaveFile(指出檔案是否為自動儲存暫存檔)
        /// </summary>
        public const string IsAutoSaveFile = "IsAutoSaveFile";
        /// <summary>
        /// 常數: IsCrypto(檔案是否為加密檔案)
        /// </summary>
        public const string IsCrypto = "IsCrypto";
        /// <summary>
        /// 常數: StorageInSMB
        /// </summary>
        public const string StorageInSMB = "StorageInSMB";

        /// <summary>
        /// 常數: SMBFullPath
        /// </summary>
        public const string SMBFullPath = "SMBFullPath";

        /// <summary>
        /// 常數: SMBLoginId
        /// </summary>
        public const string SMBLoginId = "SMBLoginId";

        /// <summary>
        /// 常數: SMBPassword
        /// </summary>
        public const string SMBPassword = "SMBPassword";

        /// <summary>
        /// 常數: DefaultPluginConfigurationFileName(外掛載入設定檔)
        /// </summary>
        public const string DefaultPluginConfigurationFileName = "plugins.json";
        /// <summary>
        /// 從作業系統環境變數組合資料庫連線字串並傳回!
        /// </summary>
        /// <returns>傳回動態組合後的連線字串內容</returns>
        public static string GetDefultConnectionStringFromOSENV()
        {
            string defaultConnectionStringInDocker = $"Data Source={Environment.GetEnvironmentVariable("SQLServerHost")};Initial Catalog={Environment.GetEnvironmentVariable("DBName")};User ID={Environment.GetEnvironmentVariable("DBUser")};Password={Environment.GetEnvironmentVariable("DBPassword")};Connect Timeout={Environment.GetEnvironmentVariable("DBConnTimeout")};ApplicationIntent=ReadWrite;MultipleActiveResultSets=true";
            return defaultConnectionStringInDocker;
        }
    }
}
