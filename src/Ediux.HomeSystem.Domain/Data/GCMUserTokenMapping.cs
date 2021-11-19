using System;
using Volo.Abp.Domain.Entities;

namespace Ediux.HomeSystem.Data
{
    public class GCMUserTokenMapping : Entity<Guid>
    {
        public Guid? user_id { get; set; }

        public string user_token { get; set; }        
    }
}
