using Microsoft.Extensions.Logging;

using System;
using System.Threading.Tasks;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.SystemManagement
{
    public interface ISystemMessageAppService : ICrudAppService<SystemMessageDto, Guid, AbpSearchRequestDto>, ITransientDependency
    {
        /// <summary>
        /// 產生系統訊息
        /// </summary>
        /// <param name="userId">系統發送對象</param>
        /// <param name="message">訊息內容</param>
        /// <param name="sendMail">是否發送為電子郵件通知</param>
        /// <param name="push">是否要發送推播</param>
        /// <param name="logger">要同時寫入操作紀錄的參考個體</param>
        /// <returns>成功建立的系統訊息</returns>
        Task<SystemMessageDto> CreateSystemMessageAsync(Guid userId, string message, bool sendMail, bool push, ILogger logger);

        /// <summary>
        /// 取得指定使用者系統訊息收件清單(包含時間區間搜尋)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        Task<PagedResultDto<SystemMessageDto>> GetListByUserAsync(Guid userId, DateTime? start, DateTime? end);

        /// <summary>
        /// 將指定訊息標示為已讀
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="systemMessageId"></param>
        /// <returns></returns>
        Task MarkupReadByUserAysnc(Guid userId, Guid systemMessageId);


    }
}
