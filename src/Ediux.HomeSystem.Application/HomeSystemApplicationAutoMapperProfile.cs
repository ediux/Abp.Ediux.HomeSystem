using AutoMapper;

using Ediux.HomeSystem.Data;
using Ediux.HomeSystem.Models.DTOs.DashBoard;
using Ediux.HomeSystem.Models.DTOs.Files;
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
                .MapExtraProperties()
                .AfterMap((s, d) => { d.ProductKey = Convert.ToBase64String(Encoding.Default.GetBytes(s.ProductKey)); })
                .ReverseMap()
                .AfterMap((s, d) => { d.ProductKey = Encoding.Default.GetString(Convert.FromBase64String(s.ProductKey)); });


            CreateMap<PassworkBookDTO, UserPasswordStore>()
                .ForMember(p => p.Site, a => a.MapFrom(s => s.SiteURL))
                .ForMember(p => p.SiteName, a => a.MapFrom(s => s.SiteName))
                .ForMember(p => p.Account, a => a.MapFrom(s => s.LoginAccount))
                .ForMember(p => p.Password, a => a.MapFrom(s => s.Password))
                .ForMember(p => p.IsHistory, a => a.MapFrom(s => s.IsHistory))
                .AfterMap((s, d) =>
                {
                    if (s.Password.IsNullOrWhiteSpace() == false)
                    {
                        string b64SecurityCode = Convert.ToBase64String(Encoding.Default.GetBytes(s.Password));

                        if (b64SecurityCode != d.Password)
                        {
                            d.Password = b64SecurityCode;
                        }
                    }
                })
                .ReverseMap()
                .AfterMap((s, d) =>
                {
                    if (s.Password.IsNullOrWhiteSpace() == false)
                    {
                        d.Password = Encoding.Default.GetString(Convert.FromBase64String(s.Password));
                    }
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

            CreateMap<File_Store, FileStoreDTO>()
                .ForMember(p => p.ModifierId, a => a.MapFrom(p => p.LastModifierId))
                .ForMember(p => p.ModifierDate, a => a.MapFrom(p => p.LastModificationTime))
                .ForMember(p => p.ContentType, a => a.MapFrom(p => p.MIME.MIME))
                .ForMember(p => p.CreatorDate, a => a.MapFrom(p => p.CreationTime))
                .ForMember(p => p.CreatorId, a => a.MapFrom(p => p.CreatorId))
                .ForMember(p => p.Description, a => a.Ignore())
                .ForMember(p => p.ExtName, a => a.MapFrom(p => p.ExtName))
                .ForMember(p => p.IsDeleted, a => a.MapFrom(p => p.IsDeleted))
                .ForMember(p => p.FileContent, a => a.Ignore())
                .ForMember(p => p.Name, a => a.MapFrom(p => p.Name))
                .ForMember(p => p.Size, a => a.MapFrom(p => p.Size))
                .ForMember(p => p.MIMETypeId, a => a.MapFrom(p => p.MIMETypeId))
                .ForMember(p => p.OriginFullPath, a => a.MapFrom(p => p.OriginFullPath))
                .AfterMap((s, d) =>
                {
                    if (!s.ExtName.IsNullOrWhiteSpace())
                    {
                        if (s.ExtName.Trim() == "*")
                        {
                            d.ExtName = "";
                        }
                    }

                    if (s.ExtraProperties.ContainsKey("Description"))
                    {
                        d.Description = (string)s.ExtraProperties["Description"];
                    }

                    if (s.ExtraProperties.ContainsKey("IsAutoSaveFile"))
                    {
                        d.IsAutoSaveFile = (bool)s.ExtraProperties["IsAutoSaveFile"];
                    }
                    else
                    {
                        d.IsAutoSaveFile = false;
                    }
                })
                .ReverseMap()
                .ForMember(p => p.IsCrypto, a => a.Ignore())
                .ForMember(p => p.InRecycle, a => a.Ignore())
                .ForMember(p => p.SMBFullPath, a => a.Ignore())
                .ForMember(p => p.SMBLoginId, a => a.Ignore())
                .ForMember(p => p.SMBPassword, a => a.Ignore())
                .AfterMap((d, s) =>
                {
                    if (d.ExtName.Trim().IsNullOrWhiteSpace())
                    {
                        s.ExtName = "*";
                    }

                    if (s.ExtraProperties.ContainsKey("Description"))
                    {
                        s.ExtraProperties["Description"] = d.Description;
                    }
                    else
                    {
                        s.ExtraProperties.Add("Description", d.Description);
                    }

                    if (s.ExtraProperties.ContainsKey("IsAutoSaveFile"))
                    {
                        s.ExtraProperties["IsAutoSaveFile"] = d.IsAutoSaveFile;
                    }
                    else
                    {
                        s.ExtraProperties.Add("IsAutoSaveFile", d.IsAutoSaveFile);
                    }
                });
        }
    }
}
