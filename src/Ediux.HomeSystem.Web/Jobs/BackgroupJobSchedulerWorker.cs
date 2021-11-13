using Ediux.HomeSystem.Models.DTOs.PersonalCalendar;
using Ediux.HomeSystem.Notification;
using Ediux.HomeSystem.PersonalCalendar;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Threading;

namespace Ediux.HomeSystem.Web.Jobs
{
    public class BackgroupJobSchedulerWorker : AsyncPeriodicBackgroundWorkerBase
    {
        public BackgroupJobSchedulerWorker(AbpAsyncTimer timer, IServiceScopeFactory serviceScopeFactory) : base(timer, serviceScopeFactory)
        {
            timer.Period = 600000;
        }

        protected override async Task DoWorkAsync(PeriodicBackgroundWorkerContext workerContext)
        {
            Logger.LogInformation("定期掃描即將到來的行事曆事件...");

            try
            {
                IPersonalCalendarAppService personalCalendarAppService =
                 workerContext.ServiceProvider.GetRequiredService<IPersonalCalendarAppService>();
                INotificationAppService notificationAppService =
                    workerContext.ServiceProvider.GetRequiredService<INotificationAppService>();

                DateTime systemTime = DateTime.Now;
                DateTime after1Hours = systemTime.AddHours(1);
                DateTime after2Hours = systemTime.AddHours(2);
                DateTime afterAWeek = systemTime.AddDays(8).AddMilliseconds(-1);
                DateTime afterTwoWeek = systemTime.AddDays(16).AddMilliseconds(-1);

                var reminds = await personalCalendarAppService.GetRemindAsync(new PersonalCalendarRequestDTO() { Start = systemTime, End = after1Hours });
                if (reminds != null && reminds.Items.Count > 0)
                {
                    reminds.Items.GroupBy(p => p.CreatorId).ToList().ForEach(p =>
                    {
                        string msg = $"您有{p.Count()}件即將在1小時後到期的行事曆事件!\n事件如下:{string.Join("\n", p.Select(s => s.Title+"("+s.StartTime.Value.Date.ToShortDateString()+")").ToArray())}";
                        notificationAppService.PushToUserAsync(p.Key, "行事曆", msg);
                    });
                }

                var reminds_b = await personalCalendarAppService.GetRemindAsync(new PersonalCalendarRequestDTO() { Start = after1Hours, End = after2Hours });
                if (reminds_b != null && reminds_b.Items.Count > 0)
                {
                    reminds_b.Items.GroupBy(p => p.CreatorId).ToList().ForEach(p =>
                    {
                        string msg = $"您有{p.Count()}件即將在2小時後到期的行事曆事件!\n事件如下:{string.Join("\n", p.Select(s => s.Title + "(" + s.StartTime.Value.Date.ToShortDateString() + ")").ToArray())}";
                        notificationAppService.PushToUserAsync(p.Key, "行事曆", msg);
                    });
                }
                var reminds_c = await personalCalendarAppService.GetRemindAsync(new PersonalCalendarRequestDTO() { Start = after2Hours, End = afterAWeek });
                if (reminds_c != null && reminds_c.Items.Count > 0)
                {
                    reminds_c.Items.GroupBy(p => p.CreatorId).ToList().ForEach(p =>
                    {
                        string msg = $"您有{p.Count()}件即將在1周後到期的行事曆事件!\n事件如下:{string.Join("\n", p.Select(s => s.Title + "(" + s.StartTime.Value.Date.ToShortDateString() + ")").ToArray())}";
                        notificationAppService.PushToUserAsync(p.Key, "行事曆", msg);
                    });
                }
                var reminds_d = await personalCalendarAppService.GetRemindAsync(new PersonalCalendarRequestDTO() { Start = afterAWeek, End = afterTwoWeek });

                if (reminds_d != null && reminds_d.Items.Count > 0)
                {
                    reminds_d.Items.GroupBy(p => p.CreatorId).ToList().ForEach(p =>
                    {
                        string msg = $"您有{p.Count()}件即將在2周後到期的行事曆事件!\n事件如下:{string.Join("\n", p.Select(s => s.Title + "(" + s.StartTime.Value.Date.ToShortDateString() + ")").ToArray())}";
                        notificationAppService.PushToUserAsync(p.Key, "行事曆", msg);
                    });
                }
            }
            catch (Exception ex)
            {

                Logger.LogError(ex, "定期掃描行事曆發生錯誤!");
            }
        }
    }
}
