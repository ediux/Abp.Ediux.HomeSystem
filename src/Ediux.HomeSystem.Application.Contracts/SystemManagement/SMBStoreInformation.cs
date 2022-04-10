using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;

namespace Ediux.HomeSystem.SystemManagement
{
    [Serializable]
    public class SMBStoreInformation 
    {
        public string SMBFullPath { get; set; }

        public string SMBLoginId { get; set; }

        public string SMBPassword { get; set; }

        public bool StorageInSMB { get; set; }

        
    }
}
