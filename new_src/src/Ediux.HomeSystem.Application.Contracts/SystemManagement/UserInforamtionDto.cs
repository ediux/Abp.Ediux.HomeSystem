using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Application.Dtos;

namespace Ediux.HomeSystem.SystemManagement
{
    public class UserInforamtionDto : ExtensibleEntityDto<Guid>
    {
        public string UserName { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Fullname { get { return Surname + Name; } }



    }
}
