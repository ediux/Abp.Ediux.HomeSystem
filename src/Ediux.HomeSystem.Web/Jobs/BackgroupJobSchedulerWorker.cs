using Ediux.HomeSystem.Models.DTOs.PersonalCalendar;
using Ediux.HomeSystem.Models.DTOs.SystemSettings;
using Ediux.HomeSystem.Notification;
using Ediux.HomeSystem.PersonalCalendar;
using Ediux.HomeSystem.SettingManagement;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Threading;
using Volo.Abp.Users;

namespace Ediux.HomeSystem.Web.Jobs
{
    public class BackgroupJobSchedulerWorker : AsyncPeriodicBackgroundWorkerBase
    {
        public BackgroupJobSchedulerWorker(AbpAsyncTimer timer, IServiceScopeFactory serviceScopeFactory) : base(timer, serviceScopeFactory)
        {
            timer.Period = 5000;
        }

        protected override async Task DoWorkAsync(PeriodicBackgroundWorkerContext workerContext)
        {
            try
            {
                var setting = ServiceProvider.GetRequiredService<ISettingManagementAppService>();

                BatchSettingsDTO batchSettings = await setting.GetBatchSettingsAsync();

                if (Timer.Period != batchSettings.Timer_Period)
                {
                    Timer.Period = batchSettings.Timer_Period;
                    Logger.LogInformation("變更定時執行的計時器時間間隔為{0}毫秒!", batchSettings.Timer_Period);
                }
                IPersonalCalendarAppService personalCalendarAppService =
                    workerContext.ServiceProvider.GetRequiredService<IPersonalCalendarAppService>();

                INotificationAppService notificationAppService =
                    workerContext.ServiceProvider.GetRequiredService<INotificationAppService>();

                ICurrentUser currentUser = workerContext.ServiceProvider.GetRequiredService<ICurrentUser>(); 

                if(currentUser != null && currentUser.IsAuthenticated)
                {
                    await notificationAppService.PushToUserAsync(currentUser.Id, "行事曆", "定期掃描即將到來的行事曆事件開始...");
                }
                else
                {
                    await notificationAppService.PushToUserAsync(null, "行事曆", "定期掃描即將到來的行事曆事件開始...");
                }
                Logger.LogInformation("定期掃描即將到來的行事曆事件開始...");
               

                DateTime systemTime = DateTime.Now;

                if (systemTime.Hour == 0 || systemTime.Hour == 6 || systemTime.Hour == 12 || systemTime.Hour == 18)
                {
                    if ((systemTime.Minute >= 0 && systemTime.Minute <= 15) || (systemTime.Minute >= 30 && systemTime.Minute <= 45))
                    {

                        //


                        DateTime after1Hours = systemTime.AddHours(1);
                        DateTime after2Hours = systemTime.AddHours(2);
                        DateTime afterAWeek = systemTime.AddDays(8).AddMilliseconds(-1);
                        DateTime afterTwoWeek = systemTime.AddDays(16).AddMilliseconds(-1);

                        var reminds = await personalCalendarAppService.GetRemindAsync(new PersonalCalendarRequestDTO() { Start = systemTime, End = after1Hours });
                        if (reminds != null && reminds.Items.Count > 0)
                        {
                            reminds.Items.GroupBy(p => p.CreatorId).ToList().ForEach(p =>
                            {
                                string msg = $"您有{p.Count()}件即將在1小時後到期的行事曆事件!\n事件如下:{string.Join("\n", p.Select(s => s.Title + "(" + s.StartTime.Value.Date.ToShortDateString() + ")").ToArray())}";
                                notificationAppService.PushToUserAsync(p.Key, "行事曆", msg);
                            });
                        }

                        var reminds_b = await personalCalendarAppService.GetRemindAsync(new PersonalCalendarRequestDTO() { Start = after1Hours.AddSeconds(1), End = after2Hours });
                        if (reminds_b != null && reminds_b.Items.Count > 0)
                        {
                            reminds_b.Items.GroupBy(p => p.CreatorId).ToList().ForEach(p =>
                            {
                                string msg = $"您有{p.Count()}件即將在2小時後到期的行事曆事件!\n事件如下:{string.Join("\n", p.Select(s => s.Title + "(" + s.StartTime.Value.Date.ToShortDateString() + ")").ToArray())}";
                                notificationAppService.PushToUserAsync(p.Key, "行事曆", msg);
                            });
                        }
                        var reminds_c = await personalCalendarAppService.GetRemindAsync(new PersonalCalendarRequestDTO() { Start = after2Hours.AddSeconds(1), End = afterAWeek });
                        if (reminds_c != null && reminds_c.Items.Count > 0)
                        {
                            reminds_c.Items.GroupBy(p => p.CreatorId).ToList().ForEach(p =>
                            {
                                string msg = $"您有{p.Count()}件即將在1周後到期的行事曆事件!\n事件如下:{string.Join("\n", p.Select(s => s.Title + "(" + s.StartTime.Value.Date.ToShortDateString() + ")").ToArray())}";
                                notificationAppService.PushToUserAsync(p.Key, "行事曆", msg);
                            });
                        }
                        var reminds_d = await personalCalendarAppService.GetRemindAsync(new PersonalCalendarRequestDTO() { Start = afterAWeek.AddSeconds(1), End = afterTwoWeek });

                        if (reminds_d != null && reminds_d.Items.Count > 0)
                        {
                            reminds_d.Items.GroupBy(p => p.CreatorId).ToList().ForEach(p =>
                            {
                                string msg = $"您有{p.Count()}件即將在2周後到期的行事曆事件!\n事件如下:{string.Join("\n", p.Select(s => s.Title + "(" + s.StartTime.Value.Date.ToShortDateString() + ")").ToArray())}";
                                notificationAppService.PushToUserAsync(p.Key, "行事曆", msg);
                            });
                        }
                    }
                    else
                    {
                        Logger.LogInformation("錯過預定排程時間，等候下次執行!");
                    }
                }
                else
                {
                    Logger.LogInformation("錯過預定排程時間，等候下次執行!");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "定期掃描行事曆發生錯誤!");
            }
            finally
            {
                Logger.LogInformation("定期掃描即將到來的行事曆事件結束...");
            }
        }
    }
}
