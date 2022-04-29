using System;
using System.Text.Json.Serialization;

namespace Ediux.HomeSystem.SystemManagement
{
    [Serializable]
    public class UploadFileJSONData
    {
        public char SplitChar { get; set; }

        public string Classification { get; set; }

        public string BlobContainerName { get; set; }

        public int Order { get; set; }

        public string FileName { get; set; }

        public Guid UploadUserId { get; set; }

        [JsonIgnore]
        public string Message { get; set; }
    }
}
