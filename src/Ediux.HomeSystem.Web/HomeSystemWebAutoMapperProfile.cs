using AutoMapper;

using Ediux.HomeSystem.Models.DTOs.PersonalCalendar;
using Ediux.HomeSystem.Models.PersonalCalendar;
using Ediux.HomeSystem.Web.Models.PersonalCalendar;

using System;

namespace Ediux.HomeSystem.Web
{
    public class HomeSystemWebAutoMapperProfile : Profile
    {
        public HomeSystemWebAutoMapperProfile()
        {
            //Define your AutoMapper configuration here for the Web project.
            CreateMap<CalendarInputViewModel, Data.PersonalCalendar>()
                .ForMember(p => p.allDay, a => a.MapFrom(s => s.AllDay))
                .ForMember(p => p.EventId, a => a.Ignore())
                .ForMember(p => p.ConcurrencyStamp, a => a.Ignore())
                .ForMember(p => p.CreationTime, a => a.Ignore())
                .ForMember(p => p.CreatorId, a => a.Ignore())
                .ForMember(p => p.DeleterId, a => a.Ignore())
                .ForMember(p => p.DeletionTime, a => a.Ignore())
                .ForMember(p => p.groupId, a => a.MapFrom(s => s.GroupId))
                .ForMember(p => p.IsAdded, a => a.Ignore())
                .ForMember(p => p.IsDeleted, a => a.Ignore())
                .ForMember(p => p.LastModificationTime, a => a.Ignore())
                .ForMember(p => p.LastModifierId, a => a.Ignore())
                .ForMember(p => p.title, a => a.MapFrom(s => s.Title))
                .ForMember(p => p.url, a => a.MapFrom(s => s.url))
                .AfterMap((m, e) =>
                {
                    e.t_start = m.StartTime.ToString();
                    if (m.EndTime.HasValue)
                    {
                        e.t_end = m.EndTime.Value.ToString();
                    }
                });

            CreateMap<Data.PersonalCalendar, CalendarInputViewModel>()
                .ForMember(p => p.AllDay, a => a.MapFrom(s => s.allDay))
                .ForMember(p => p.GroupId, a => a.MapFrom(s => s.groupId))
                .ForMember(p => p.Title, a => a.MapFrom(s => s.title))
                .AfterMap((e, m) =>
                {


                    if (!string.IsNullOrEmpty(e.t_end))
                    {
                        m.EndTime = DateTime.Parse(e.t_end);
                    }

                    m.StartTime = DateTime.Parse(e.t_start);
                });

            CreateMap<CalendarInputUIViewModel, Data.PersonalCalendar>()
                .ForMember(p => p.allDay, a => a.MapFrom(s => s.AllDay))
                .ForMember(p => p.EventId, a => a.Ignore())
                .ForMember(p => p.ConcurrencyStamp, a => a.Ignore())
                .ForMember(p => p.CreationTime, a => a.Ignore())
                .ForMember(p => p.CreatorId, a => a.Ignore())
                .ForMember(p => p.DeleterId, a => a.Ignore())
                .ForMember(p => p.DeletionTime, a => a.Ignore())
                .ForMember(p => p.groupId, a => a.MapFrom(s => s.GroupId))
                .ForMember(p => p.IsAdded, a => a.Ignore())
                .ForMember(p => p.IsDeleted, a => a.Ignore())
                .ForMember(p => p.LastModificationTime, a => a.Ignore())
                .ForMember(p => p.LastModifierId, a => a.Ignore())
                .ForMember(p => p.title, a => a.MapFrom(s => s.Title))
                .ForMember(p => p.url, a => a.MapFrom(s => s.url))
                .AfterMap((m, e) =>
                {
                    e.t_start = m.StartTime.ToString();

                    if (m.EndTime.HasValue)
                    {
                        e.t_end = m.EndTime.Value.ToString();
                    }
                });

            CreateMap<Data.PersonalCalendar, CalendarInputUIViewModel>()
                .ForMember(p => p.AllDay, a => a.MapFrom(s => s.allDay))
                .ForMember(p => p.GroupId, a => a.MapFrom(s => s.groupId))
                .ForMember(p => p.Title, a => a.MapFrom(s => s.title))
                .AfterMap((e, m) =>
                {
                    if (!string.IsNullOrEmpty(e.t_end))
                    {
                        m.EndTime = DateTime.Parse(e.t_end);
                    }

                    m.StartTime = DateTime.Parse(e.t_start);
                });

            CreateMap<CalendarInputUIViewModel, CalendarInputViewModel>();
            CreateMap<CalendarInputViewModel, CalendarInputUIViewModel>();

            CreateMap<PersonalCalendarItemDTO, CalendarInputUIViewModel>();
        }
    }
}
