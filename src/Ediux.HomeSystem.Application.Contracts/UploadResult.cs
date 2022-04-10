using System;
using System.Collections.Generic;
using System.Text;

namespace Ediux.HomeSystem
{
    [Serializable]
    public class UploadResult
    {
        public bool Success { get; set; }
        public Guid? FileStoreId { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public SystemManagement.UploadFileJSONData RefenceData { get; set; }
    }
}
