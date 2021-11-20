using Ediux.HomeSystem.Data;
using Ediux.HomeSystem.Models.DTOs.Firebase;
using Ediux.HomeSystem.Settings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.SettingManagement;

namespace Ediux.HomeSystem.Notification
{
    public class NotificationAppService : HomeSystemAppService, INotificationAppService
    {
        private readonly IRepository<GCMUserTokenMapping> gCMUserTokenMappings;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<NotificationAppService> logger;
        private readonly ISettingManager settingManager;
        public NotificationAppService(IRepository<GCMUserTokenMapping> gCMUserTokenMappings,
            ISettingManager settingManager,
            IHttpClientFactory httpClientFactory,
            ILogger<NotificationAppService> logger)
        {
            this.gCMUserTokenMappings = gCMUserTokenMappings;
            this.httpClientFactory = httpClientFactory;
            this.settingManager = settingManager;
            this.logger = logger;
        }

        public async Task PushToUserAsync(Guid? userId, string title, string message, Dictionary<string, string> extraData = null, string icon = "/favicon-16x16.png", string priority = "normal", string openURL = null)
        {
            try
            {
                string serverKey = await settingManager.GetOrNullGlobalAsync(HomeSystemSettings.FCMSettings.ServiceKey);
                string messagingSenderId = await settingManager.GetOrNullGlobalAsync(HomeSystemSettings.FCMSettings.MessagingSenderId);

                var userTokens = (await gCMUserTokenMappings.GetQueryableAsync())
                      .WhereIf(userId.HasValue, w => w.user_id == userId)
                      .Select(s => s.user_token)
                      .Distinct()
                      .ToList();

                var d = new PushRequestInfoDTO();
                d.registration_ids = userTokens;
                d.priority = priority;

                d.notification.body = message;
                d.notification.title = title;
                d.notification.icon = icon;

                if (!string.IsNullOrWhiteSpace(openURL))
                {
                    d.notification.click_action = openURL.Trim();
                }

                if (extraData != null)
                {
                    d.data = extraData;
                }

                var client = httpClientFactory.CreateClient();
                HttpContent content = new StringContent(JsonSerializer.Serialize(d), Encoding.UTF8);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"key={serverKey}");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Sender", $"id={messagingSenderId}");
                var response = await client.PostAsync("https://fcm.googleapis.com/fcm/send", content);

                if (response.IsSuccessStatusCode == false)
                {
                    logger.LogError($"發送推播訊息「{title}」內容為「{message}」失敗!");
                }
                else
                {
                    logger.LogInformation($"發送推播訊息「{title}」內容為「{message}」成功!");
                }
            }
            catch (Exception ex)
            {
                logger.LogError("推送訊息時發生未預期的錯誤!錯誤訊息為{0}{1}", ex.Message, ex.StackTrace);
            }
        }

        public async Task<string> RegisterUserTokenAsync(string token)
        {
            try
            {
                var pushTokens = (await gCMUserTokenMappings.GetQueryableAsync())
                    .WhereIf(CurrentUser.IsAuthenticated, p => p.user_id == CurrentUser.Id && p.user_token == token)
                    .WhereIf(CurrentUser.IsAuthenticated == false, p => p.user_token == token)
                    .ToList();

                if (pushTokens.Any() == false)
                {
                    await gCMUserTokenMappings.InsertAsync(new GCMUserTokenMapping() { user_id = CurrentUser.Id, user_token = token }, autoSave: true);
                }

                return string.Join(",", pushTokens.Select(s => s.user_token).ToArray());
            }
            catch (Exception ex)
            {
                logger.LogError("註冊推播用戶端Token時發生未預期的錯誤!錯誤訊息為{0}{1}", ex.Message, ex.StackTrace);
                throw;
            }
        }

        public async Task<bool> UnregisterUserTokenAsync(string token)
        {
            try
            {
                await gCMUserTokenMappings.DeleteAsync(p => p.user_id == CurrentUser.Id || p.user_token == token);

                return !(await UserTokenExistAsync(CurrentUser.Id, token));
            }
            catch (Exception ex)
            {
                logger.LogError("查詢用戶端Token時發生未預期的錯誤!錯誤訊息為{0}{1}", ex.Message, ex.StackTrace);
                throw;
            }
        }

        public async Task<bool> UserTokenExistAsync(Guid? userId, string token)
        {
            try
            {
                var tokenExists = (await gCMUserTokenMappings.GetQueryableAsync())
                  .WhereIf(userId.HasValue, p => p.user_id == userId && p.user_token == token)
                  .WhereIf(userId.HasValue == false, p => p.user_token == token)
                  .Any();

                return tokenExists;
            }
            catch (Exception ex)
            {
                logger.LogError("查詢用戶端Token時發生未預期的錯誤!錯誤訊息為{0}{1}", ex.Message, ex.StackTrace);
                throw;
            }
        }
    }
}
