namespace Ediux.HomeSystem.SystemManagement
{
    public class BlobStoreObject 
    {
        /// <summary>
        /// Abp的Blob容器名稱
        /// </summary>
        public string BlobContainerName { get; set; }

        /// <summary>
        /// 檔案二進制內容
        /// </summary>
        public byte[] FileContent { get; set; } = null;
        
        
    }
}
