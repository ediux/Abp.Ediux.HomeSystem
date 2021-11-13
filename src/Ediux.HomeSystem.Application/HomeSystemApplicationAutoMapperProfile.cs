using AutoMapper;

using Ediux.HomeSystem.Data;
using Ediux.HomeSystem.Models.DTOs.DashBoard;
using Ediux.HomeSystem.Models.DTOs.MIMETypes;
using Ediux.HomeSystem.Models.DTOs.PassworkBook;
using Ediux.HomeSystem.Models.DTOs.PersonalCalendar;
using Ediux.HomeSystem.Models.DTOs.PluginModule;
using Ediux.HomeSystem.Models.DTOs.ProductKeysBook;

using System;
using System.Text;

namespace Ediux.HomeSystem
{
    public class HomeSystemApplicationAutoMapperProfile : Profile
    {
        public HomeSystemApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            CreateMap<PluginModuleDTO, AbpPlugins>()
                .ForMember(p => p.Path, a => a.MapFrom(s => s.PluginPath))
                .ReverseMap();

            CreateMap<ProductKeysBookDTO, ProductKeys>()
                .AfterMap((s, d) => { d.ProductKey = Convert.ToBase64String(Encoding.Default.GetBytes(s.ProductKey)); })
                .ReverseMap()
                .AfterMap((s, d) => { d.ProductKey = Encoding.Default.GetString(Convert.FromBase64String(s.ProductKey)); });


            CreateMap<PassworkBookDTO, UserPasswordStore>()
                .ForMember(p => p.Site, a => a.MapFrom(s => s.SiteURL))
                .ForMember(p => p.SiteName, a => a.MapFrom(s => s.SiteName))
                .ForMember(p => p.Account, a => a.MapFrom(s => s.LoginAccount))
                .ForMember(p => p.Password, a => a.MapFrom(s => s.Password))
                .ForMember(p => p.IsHistory, a => a.MapFrom(s => s.IsHistory))
                .AfterMap((s, d) => { d.Password = Convert.ToBase64String(Encoding.Default.GetBytes(s.Password)); })
                .ReverseMap()
                .AfterMap((s, d) =>
                {
                    d.Password = Encoding.Default.GetString(Convert.FromBase64String(s.Password));
                });

            CreateMap<MIMETypesDTO, MIMEType>().ReverseMap();


            CreateMap<PersonalCalendarItemDTO, Data.PersonalCalendar>()
                .ForMember(p => p.title, a => a.MapFrom(s => s.Title))
                .ForMember(p => p.allDay, a => a.MapFrom(s => s.AllDay))
                .ForMember(p => p.classNames, a => a.Ignore())
                .ForMember(p => p.ConcurrencyStamp, a => a.Ignore())
                .ForMember(p => p.groupId, a => a.Ignore())
                .ForMember(p => p.IsAdded, a => a.Ignore())
                .ForMember(p => p.IsDeleted, a => a.Ignore())
                .ForMember(p => p.LastModificationTime, a => a.Ignore())
                .ForMember(p => p.LastModifierId, a => a.Ignore())
                .AfterMap((M, E) =>
                {
                    if (M.StartTime.HasValue)
                    {
                        if (M.AllDay)
                        {
                            E.t_start = $"{M.StartTime.Value:yyyy/MM/dd} 00:00:00";
                        }
                        else
                        {                            
                            E.t_start = $"{M.StartTime.Value:yyyy/MM/dd HH:mm:ss}";
                        }

                    }

                    if (M.EndTime.HasValue)
                    {
                        if (M.AllDay)
                        {
                            E.t_end = $"{M.EndTime.Value:yyyy/MM/dd} 23:59:59"; // M.EndTime.Value.ToString("yyyy/MM/dd") + " 23:59:59";
                        }
                        else
                        {
                            E.t_end = $"{M.EndTime.Value:yyyy/MM/dd HH:mm:ss}";
                        }

                    }
                })
                .ReverseMap()
                .AfterMap((E, M) =>
                {

                    if (!string.IsNullOrWhiteSpace(E.t_start))
                    {
                        if (E.allDay)
                        {
                            M.StartTime = DateTime.Parse(E.t_start).Date;
                        }
                        else
                        {
                            M.StartTime = DateTime.Parse(E.t_start);
                        }

                    }
                    else
                    {
                        M.StartTime = null;
                    }

                    if (!string.IsNullOrEmpty(E.t_end))
                    {
                        if (E.allDay)
                        {
                            M.EndTime = DateTime.Parse(E.t_start).Date.AddDays(1).AddSeconds(-1);
                        }
                        else
                        {
                            M.EndTime = DateTime.Parse(E.t_end);
                        }
                    }
                    else
                    {
                        M.EndTime = null;
                    }
                });

            CreateMap<DashboardWidgets, DashBoardWidgetsDTO>()
                .ForMember(p => p.GlobalSettingName, a => a.Ignore())
                .ForMember(p => p.GlobalSettingDefaultValue, a => a.Ignore())
                .ReverseMap();

            CreateMap<DashboardWidgetUsers, DashBoardWidgetUserDTO>()
                .ReverseMap();
        }
    }
}
