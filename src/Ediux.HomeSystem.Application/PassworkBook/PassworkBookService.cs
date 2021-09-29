using Ediux.HomeSystem.Data;
using Ediux.HomeSystem.Models.DTOs.PassworkBook;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Ediux.HomeSystem.PassworkBook
{
    public class PassworkBookService : CrudAppService<UserPasswordStore, PassworkBookDTO, long>, IPassworkBookService
    {
        public PassworkBookService(IRepository<UserPasswordStore, long> repository) : base(repository)
        {
        }
    }
}
