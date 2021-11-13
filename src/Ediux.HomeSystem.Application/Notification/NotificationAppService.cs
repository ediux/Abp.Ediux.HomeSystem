using Ediux.HomeSystem.Data;
using Ediux.HomeSystem.Models.DTOs.Firebase;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Ediux.HomeSystem.Notification
{
    public class NotificationAppService : HomeSystemAppService, INotificationAppService
    {
        private readonly IRepository<GCMUserTokenMapping> gCMUserTokenMappings;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<NotificationAppService> logger;
        public NotificationAppService(IRepository<GCMUserTokenMapping> gCMUserTokenMappings,
            IHttpClientFactory httpClientFactory,
            ILogger<NotificationAppService> logger)
        {
            this.gCMUserTokenMappings = gCMUserTokenMappings;
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }

        public async Task PushToUserAsync(Guid? userId, string title, string message)
        {
            try
            {
                string serverKey = "AAAA4hSVrtA:APA91bFnZB7QheYVLd53jpVmm3d34ChYn8IcYObZYdzN3pWHAIngpm5Q7-rXLeR3ahjri2x3FwspLkx_EbapSm2GO_p6eGIMBGKHDQ0XbvCEX35CxF9_knlYckiEJUdmRq4jVG9rHfyy";
                string messagingSenderId = "971007962832";
                var userTokens = (await gCMUserTokenMappings.GetQueryableAsync())
                      .WhereIf(userId.HasValue, w => w.user_id == userId)
                      .Select(s => s.user_token)
                      .ToList();


                //var wc = new System.Net.WebClient();
                //wc.Headers.Add("Authorization", "key=" + serverKey);
                //wc.Headers.Add("Content-Type", "application/json");
                //wc.Encoding = System.Text.Encoding.UTF8;

                var d = new PushRequestInfoDTO();
                d.registration_ids = userTokens;
                d.priority = "normal";

                d.notification.body = message;
                d.notification.title = title;

                var client = httpClientFactory.CreateClient();
                HttpContent content = new StringContent(JsonSerializer.Serialize(d), Encoding.UTF8);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"key={serverKey}");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Sender", $"id={messagingSenderId}");
                //client.DefaultRequestHeaders.Add("Authorization", $"Bearer {serverKey}");
                var response = await client.PostAsync("https://fcm.googleapis.com/fcm/send", content);

                if (response.IsSuccessStatusCode == false)
                {
                    logger.LogError($"發送推播訊息「{title}」內容為「{message}」失敗!");
                }
                else
                {
                    logger.LogError($"發送推播訊息「{title}」內容為「{message}」成功!");
                }
            }
            catch (Exception ex)
            {
                logger.LogError("推送訊息時發生未預期的錯誤!錯誤訊息為{0}{1}", ex.Message, ex.StackTrace);
            }
        }

        public async Task RegisterUserTokenAsync(string token)
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
            }
            catch (Exception ex)
            {
                logger.LogError("註冊推播用戶端Token時發生未預期的錯誤!錯誤訊息為{0}{1}", ex.Message, ex.StackTrace);
                throw;
            }
        }
    }
}
