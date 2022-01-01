using AutoMapper;

using Ediux.HomeSystem.Models.DTOs.PersonalCalendar;
using Ediux.HomeSystem.Models.DTOs.PluginModule;
using Ediux.HomeSystem.Models.PersonalCalendar;

namespace Ediux.HomeSystem
{
    public class HomeSystemHttpAPIAutoMapperProfile : Profile
    {
        public HomeSystemHttpAPIAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
           * Alternatively, you can split your mapping configurations
           * into multiple profile classes for a better organization. */
            //CreateMap<PluginModuleCreateOrUpdateDTO, PluginModuleDTO>()
            //    .ReverseMap();               

            CreateMap<CalendarInputViewModel, PersonalCalendarItemDTO>()
              .ForMember(p => p.CreationTime, a => a.Ignore())
              .ForMember(p => p.CreatorId, a => a.Ignore())
              .ForMember(p => p.LastModificationTime, a => a.Ignore())
              .ForMember(p => p.LastModifierId, a => a.Ignore())
              .ReverseMap();
        }
    }
}
