using AutoMapper;

using Ediux.HomeSystem.AdditionalSystemFunctions4Users;
using Ediux.HomeSystem.SystemManagement;

using System;

namespace Ediux.HomeSystem
{
    public class HomeSystemApplicationAutoMapperProfile : Profile
    {
        public HomeSystemApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            CreateMap<AbpPlugins, PluginModuleDTO>()
                .ForMember(x => x.Name, a => a.MapFrom(x => x.AssemblyName))
                .ForMember(x => x.PluginPath, a => a.Ignore())
                .ForMember(x => x.Disabled, a => a.MapFrom(x => x.Disabled))
                .ForMember(x => x.CreationTime, a => a.MapFrom(x => x.CreationTime))
                .ForMember(x => x.CreatorId, a => a.MapFrom(x => x.CreatorId))
                .ForMember(x => x.LastModificationTime, a => a.MapFrom(x => x.LastModificationTime))
                .ForMember(x => x.LastModifierId, a => a.MapFrom(x => x.LastModifierId))
                .AfterMap((s, d) =>
                {
                    if (s.ExtraProperties.ContainsKey(nameof(d.PluginPath)))
                    {
                        d.PluginPath = (string)s.ExtraProperties[nameof(d.PluginPath)];
                    }
                    else
                    {
                        d.PluginPath = null;
                    }
                })
                .ReverseMap()
                .AfterMap((d, s) =>
                {
                    if (d.PluginPath.IsNullOrWhiteSpace() == false)
                    {
                        if (s.ExtraProperties.ContainsKey(nameof(d.PluginPath)))
                        {
                            s.ExtraProperties[nameof(d.PluginPath)] = d.PluginPath;
                        }
                        else
                        {
                            s.ExtraProperties.Add(nameof(d.PluginPath), d.PluginPath);
                        }
                    }
                    else
                    {
                        if (s.ExtraProperties.ContainsKey(nameof(d.PluginPath)))
                        {
                            s.ExtraProperties.Remove(nameof(d.PluginPath));
                        }
                    }
                });

            CreateMap<PersonalCalendar, PersonalCalendarDto>()
                .ForMember(x => x.EventId, a => a.MapFrom(x => x.Id))
                .ForMember(x => x.groupId, a => a.MapFrom(x => x.RefenceEventId))
                .ForMember(x => x.title, a => a.MapFrom(x => x.SystemMessages.Subject))
                .ForMember(x => x.classNames, a => a.Ignore())
                .ForMember(x => x.durationEditable, a => a.Ignore())
                .ForMember(x => x.resourceEditable, a => a.Ignore())
                .ForMember(x => x.startEditable, a => a.Ignore())
                .ForMember(x => x.editable, a => a.Ignore())
                .ForMember(x => x.icon, a => a.Ignore())
                .ForMember(x => x.IsAdded, a => a.Ignore())
                .ForMember(x => x.url, a => a.Ignore())
                .ForMember(x => x.t_start, a => a.MapFrom(x => x.StartTime))
                .ForMember(x => x.t_end, a => a.MapFrom(x => x.EndTime))
                .ForMember(x => x.allDay, a => a.MapFrom(x => x.IsAllDay))
                .AfterMap((s, d) =>
                {
                    if (s.ExtraProperties.ContainsKey(nameof(d.classNames)))
                    {
                        d.classNames = (string)s.ExtraProperties[nameof(d.classNames)];
                    }
                    else
                    {
                        d.classNames = null;
                    }

                    if (s.ExtraProperties.ContainsKey(nameof(d.durationEditable)))
                    {
                        d.durationEditable = (bool)s.ExtraProperties[nameof(d.durationEditable)];
                    }
                    else
                    {
                        d.durationEditable = false;
                    }

                    if (s.ExtraProperties.ContainsKey(nameof(d.resourceEditable)))
                    {
                        d.resourceEditable = (bool)s.ExtraProperties[nameof(d.resourceEditable)];
                    }
                    else
                    {
                        d.resourceEditable = false;
                    }

                    if (s.ExtraProperties.ContainsKey(nameof(d.startEditable)))
                    {
                        d.startEditable = (bool)s.ExtraProperties[nameof(d.startEditable)];
                    }
                    else
                    {
                        d.startEditable = false;
                    }

                    if (s.ExtraProperties.ContainsKey(nameof(d.editable)))
                    {
                        d.editable = (bool)s.ExtraProperties[nameof(d.editable)];
                    }
                    else
                    {
                        d.editable = false;
                    }

                    if (s.ExtraProperties.ContainsKey(nameof(d.icon)))
                    {
                        d.icon = (string)s.ExtraProperties[nameof(d.icon)];
                    }
                    else
                    {
                        d.icon = null;
                    }

                    if (s.ExtraProperties.ContainsKey(nameof(d.IsAdded)))
                    {
                        d.IsAdded = (bool)s.ExtraProperties[nameof(d.IsAdded)];
                    }
                    else
                    {
                        d.IsAdded = false;
                    }

                    if (s.ExtraProperties.ContainsKey(nameof(d.url)))
                    {
                        d.url = (string)s.ExtraProperties[nameof(d.url)];
                    }
                    else
                    {
                        d.url = null;
                    }
                })
                .ReverseMap()
                .AfterMap((d, s) =>
                {
                    if (s.ExtraProperties.ContainsKey(nameof(d.durationEditable)))
                    {
                        s.ExtraProperties[nameof(d.durationEditable)] = d.durationEditable;
                    }
                    else
                    {
                        s.ExtraProperties.Add(nameof(d.durationEditable), d.durationEditable);
                    }

                    if (s.ExtraProperties.ContainsKey(nameof(d.resourceEditable)))
                    {
                        s.ExtraProperties[nameof(d.resourceEditable)] = d.resourceEditable;
                    }
                    else
                    {
                        s.ExtraProperties.Add(nameof(d.resourceEditable), d.resourceEditable);
                    }

                    if (s.ExtraProperties.ContainsKey(nameof(d.startEditable)))
                    {
                        s.ExtraProperties[nameof(d.startEditable)] = d.startEditable;
                    }
                    else
                    {
                        s.ExtraProperties.Add(nameof(d.startEditable), d.startEditable);
                    }

                    if (s.ExtraProperties.ContainsKey(nameof(d.editable)))
                    {
                        s.ExtraProperties[nameof(d.editable)] = d.editable;
                    }
                    else
                    {
                        s.ExtraProperties.Add(nameof(d.editable), d.editable);
                    }

                    if (s.ExtraProperties.ContainsKey(nameof(d.IsAdded)))
                    {
                        s.ExtraProperties[nameof(d.IsAdded)] = d.IsAdded;
                    }
                    else
                    {
                        s.ExtraProperties.Add(nameof(d.IsAdded), d.IsAdded);
                    }

                    if (d.classNames.IsNullOrWhiteSpace() == false)
                    {
                        if (s.ExtraProperties.ContainsKey(nameof(d.classNames)))
                        {
                            s.ExtraProperties[nameof(d.classNames)] = d.classNames;
                        }
                        else
                        {
                            s.ExtraProperties.Add(nameof(d.classNames), d.classNames);
                        }
                    }
                    else
                    {
                        if (s.ExtraProperties.ContainsKey(nameof(d.classNames)))
                        {
                            s.ExtraProperties.Remove(nameof(d.classNames));
                        }
                    }

                    if (d.icon.IsNullOrWhiteSpace() == false)
                    {
                        if (s.ExtraProperties.ContainsKey(nameof(d.icon)))
                        {
                            s.ExtraProperties[nameof(d.icon)] = d.icon;
                        }
                        else
                        {
                            s.ExtraProperties.Add(nameof(d.icon), d.icon);
                        }
                    }
                    else
                    {
                        if (s.ExtraProperties.ContainsKey(nameof(d.icon)))
                        {
                            s.ExtraProperties.Remove(nameof(d.icon));
                        }
                    }

                    if (d.url.IsNullOrWhiteSpace() == false)
                    {
                        if (s.ExtraProperties.ContainsKey(nameof(d.url)))
                        {
                            s.ExtraProperties[nameof(d.url)] = d.url;
                        }
                        else
                        {
                            s.ExtraProperties.Add(nameof(d.url), d.url);
                        }
                    }
                    else
                    {
                        if (s.ExtraProperties.ContainsKey(nameof(d.url)))
                        {
                            s.ExtraProperties.Remove(nameof(d.url));
                        }
                    }
                });

        }
    }
}
