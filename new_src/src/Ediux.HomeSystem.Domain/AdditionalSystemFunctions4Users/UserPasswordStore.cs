
using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;

namespace Ediux.HomeSystem.AdditionalSystemFunctions4Users
{
    public class UserPasswordStore : AuditedEntity<long>, IAuditedObject, IHasExtraProperties
    {
        public string Site { get; set; }

        public string SiteName { get; set; }
        public string Password { get; set; }
        public string Account { get; set; }

        public bool IsHistory { get; set; }


        public ExtraPropertyDictionary ExtraProperties { get; protected set; }


        public UserPasswordStore()
        {
            ExtraProperties = new ExtraPropertyDictionary();
        }
    }
}
