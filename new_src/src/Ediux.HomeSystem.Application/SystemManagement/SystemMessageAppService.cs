using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Ediux.HomeSystem.SystemManagement
{
    public class SystemMessageAppService : HomeSystemCrudAppService<InternalSystemMessages, SystemMessageDto, Guid, AbpSearchRequestDto>, ISystemMessageAppService
    {
        public SystemMessageAppService(IRepository<InternalSystemMessages, Guid> repository) : base(repository)
        {
        }

        public Task<SystemMessageDto> CreateSystemMessageAsync(Guid userId, string message, bool sendMail, bool push, ILogger logger)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResultDto<SystemMessageDto>> GetListByUserAsync(Guid userId, DateTime? start, DateTime? end)
        {
            throw new NotImplementedException();
        }

        public Task MarkupReadByUserAysnc(Guid userId, Guid systemMessageId)
        {
            throw new NotImplementedException();
        }
    }
}
