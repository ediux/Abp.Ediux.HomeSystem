using Shouldly;

using System;
using System.Linq;
using System.Threading.Tasks;

using Volo.Abp;
using Volo.Abp.Users;

using Xunit;
namespace Ediux.HomeSystem.AdditionalSystemFunctions4Users.Tests
{
    public class PersonalCalendarAppServiceTests : HomeSystemApplicationTestBase
    {
        private readonly ICurrentUser currentUser;
        private readonly IPersonalCalendarAppService personalCalendarAppService;

        public PersonalCalendarAppServiceTests()
        {
            personalCalendarAppService = GetRequiredService<IPersonalCalendarAppService>();
            currentUser = GetRequiredService<ICurrentUser>();

        }



        [Fact()]
        public async Task GetListAsyncTest()
        {
            var result = await personalCalendarAppService.GetListAsync(new PersonalCalendarRequestDto());
            result.TotalCount.ShouldBeGreaterThan(0);
            result.Items.ShouldNotBeNull();
        }

        [Fact()]
        public async Task CreateAsyncTest()
        {
            DateTime systime = DateTime.Now;
            PersonalCalendarDto calendar = null;
            calendar = await personalCalendarAppService.CreateAsync(new PersonalCalendarDto()
            {
                Color = "Black",
                Description = "測試",
                Start = systime,
                End = systime,
                IsAllDay = true,
                Title = "測試",
                UIAction = "https://"
            });

            calendar.ShouldNotBeNull();
            calendar.Id.ShouldNotBe(Guid.Empty);
        }

        [Fact()]
        public async void UpdateAsyncTest()
        {
            DateTime systime = DateTime.Now;
            var result = await personalCalendarAppService.GetListAsync(new PersonalCalendarRequestDto());
            PersonalCalendarDto personalCalendarDto = result.Items.FirstOrDefault();
            personalCalendarDto.ShouldNotBeNull();

            personalCalendarDto.Description = "modify";

            personalCalendarDto = await personalCalendarAppService.UpdateAsync(personalCalendarDto.Id, personalCalendarDto);

            personalCalendarDto.Description.ShouldBe("modify");
        }

        [Fact()]
        public async void GetRemindAsyncTest()
        {
            DateTime systime = DateTime.Now;
            var result = await personalCalendarAppService.GetRemindAsync(new PersonalCalendarRequestDto() { Start = systime.AddDays(-7), End = systime.AddDays(7) });
            result.ShouldNotBeNull();
            result.Items.ShouldNotBeEmpty();

        }
    }
}