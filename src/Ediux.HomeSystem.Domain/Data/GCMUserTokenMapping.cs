using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Ediux.HomeSystem.Data
{
    public class GCMUserTokenMapping : Entity<Guid>
    {
        public Guid? user_id { get; set; }

        public string user_token { get; set; }
    }
}
