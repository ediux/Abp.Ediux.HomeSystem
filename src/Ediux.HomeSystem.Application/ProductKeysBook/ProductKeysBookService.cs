using Ediux.HomeSystem.Data;
using Ediux.HomeSystem.Models.DTOs.ProductKeysBook;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace Ediux.HomeSystem.ProductKeysBook
{
    public class ProductKeysBookService : CrudAppService<ProductKeys, ProductKeysBookDTO, Guid>, IProductKeysBookService
    {
        private readonly ICurrentUser currentUser;

        public ProductKeysBookService(IRepository<ProductKeys, Guid> repository, ICurrentUser currentUser) : base(repository)
        {
            this.currentUser = currentUser;
        }

        public Task<IEnumerable<ProductKeysBookDTO>> ListByUserAsync()
        {
            throw new NotImplementedException();
        }
    }
}
