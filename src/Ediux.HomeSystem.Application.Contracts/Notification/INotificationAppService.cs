using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.Notification
{
    public interface INotificationAppService : IApplicationService, ITransientDependency
    {
        /// <summary>
        /// 對指定使用者推送訊息
        /// </summary>
        /// <param name="title">訊息標題</param>
        /// <param name="message">訊息內容</param>
        /// <returns></returns>
        Task PushToUserAsync(Guid? userId, string title, string message, Dictionary<string, string> extraData = null, string icon = "/favicon-16x16.png", string priority = "normal");

        /// <summary>
        /// 註冊用戶端TOKEN對應
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<string> RegisterUserTokenAsync(string token);   
    }
}
