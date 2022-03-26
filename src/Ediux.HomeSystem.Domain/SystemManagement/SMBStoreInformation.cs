using System.Collections.Generic;

using Volo.Abp.Domain.Values;

namespace Ediux.HomeSystem.SystemManagement
{
    public class SMBStoreInformation : ValueObject
    {
        public string SMBFullPath { get; set; }

        public string SMBLoginId { get; set; }

        public string SMBPassword { get; set; }

        public bool StorageInSMB { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return StorageInSMB;
            yield return SMBLoginId;
            yield return SMBPassword;
            yield return SMBFullPath;
        }
    }
}
