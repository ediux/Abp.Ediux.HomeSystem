using AutoMapper;

using Ediux.HomeSystem.AdditionalSystemFunctions4Users;
using Ediux.HomeSystem.SystemManagement;

using System;
using System.Linq;
using System.Text;

using Volo.Abp.Identity;

namespace Ediux.HomeSystem
{
    public class HomeSystemApplicationAutoMapperProfile : Profile
    {
        public HomeSystemApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            CreateMap<AbpPlugins, PluginModuleDto>()
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

            CreateMap<DashboardWidgets, DashBoardWidgetsDto>()
               .ReverseMap();

            CreateMap<DashboardWidgetUsers, UserInforamtionDto>()
                .ForMember(p => p.Id, a => a.MapFrom(x => x.UserId))
                .ForMember(p => p.UserName, a => a.MapFrom(x => x.User.UserName))
                .ForMember(p => p.Name, a => a.MapFrom(x => x.User.Name))
                .ForMember(p => p.Surname, a => a.MapFrom(x => x.User.Surname))
                .ForMember(p => p.Fullname, a => a.Ignore())
                .ReverseMap()
                .ForPath(p => p.User.UserName, a => a.MapFrom(x => x.UserName))
                .ForPath(p => p.User.Name, a => a.MapFrom(x => x.Name))
                .ForPath(p => p.User.Surname, a => a.MapFrom(x => x.Surname));

            CreateMap<IdentityUser, UserInforamtionDto>()
                .ForMember(p => p.Fullname, a => a.Ignore())
                .ReverseMap();

            CreateMap<ProductKeys, ProductKeysBookDto>()
                .ForMember(p => p.ExtraInformation, a => a.Ignore())
                .MapExtraProperties()
                .AfterMap((s, d) =>
                {
                    if (s != null && s.ProductKey.IsNullOrWhiteSpace() == false)
                    {
                        d.ProductKey = Encoding.Default.GetString(Convert.FromBase64String(s.ProductKey));
                    }
                    else
                    {
                        d.ProductKey = String.Empty;
                    }
                    if (s != null && s.ExtraProperties != null && s.ExtraProperties.Count > 0)
                    {
                        foreach (string key in s.ExtraProperties.Keys)
                        {
                            d.ExtraProperties.Add(key, s.ExtraProperties[key]);

                            if (d.ExtraInformation.IsNullOrWhiteSpace() == false)
                            {
                                d.ExtraInformation += $"<br/>{key}:{s.ExtraProperties[key]}";
                            }
                            else
                            {
                                d.ExtraInformation += $"{key}:{s.ExtraProperties[key]}";
                            }
                        }
                    }

                })
              .ReverseMap()
              .AfterMap((s, d) =>
              {
                  if (s != null && s.ProductKey.IsNullOrWhiteSpace() == false)
                  {
                      d.ProductKey = Convert.ToBase64String(Encoding.Default.GetBytes(s.ProductKey));
                  }
                  else
                  {
                      d.ProductKey = string.Empty;
                  }

                  if (d != null && d.ExtraProperties != null && d.ExtraProperties.Count > 0)
                  {
                      foreach (string key in d.ExtraProperties.Keys)
                      {
                          if (s.ExtraProperties.ContainsKey(key))
                          {
                              s.ExtraProperties[key] = d.ExtraProperties[key];
                          }
                          else
                          {
                              s.ExtraProperties.Add(key, s.ExtraProperties[key]);
                          }
                      }
                  }
              });

            CreateMap<UserPasswordStore, PasswordStoreDto>()
              .ForMember(p => p.SiteURL, a => a.MapFrom(s => s.Site))
              .ForMember(p => p.SiteName, a => a.MapFrom(s => s.SiteName))
              .ForMember(p => p.LoginAccount, a => a.MapFrom(s => s.Account))
              .ForMember(p => p.Password, a => a.MapFrom(s => s.Password))
              .ForMember(p => p.IsHistory, a => a.MapFrom(s => s.IsHistory))
              .MapExtraProperties()
              .AfterMap((s, d) =>
              {
                  if (s.Password.IsNullOrWhiteSpace() == false)
                  {
                      d.Password = Encoding.Default.GetString(Convert.FromBase64String(s.Password));
                  }
              })
              .ReverseMap()
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
              });

            CreateMap<MIMEType, MIMETypesDto>()
                .ForMember(p => p.ContentType, a => a.MapFrom(x => x.TypeName))
                .ReverseMap()
                .ForMember(p => p.Files, a => a.Ignore());

            CreateMap<FileStoreClassification, FileClassificationDto>()
                .ForPath(p => p.Parent.Id, a => a.MapFrom(x => x.ParentClassificationId))
                .ForMember(p => p.Parent, a => a.Ignore())
                .ReverseMap()
                .ForMember(p => p.ParentClassificationId, a => a.MapFrom(x => x.Parent.Id))
                .ForMember(p => p.Parent, a => a.Ignore());



            CreateMap<File_Store, FileStoreDto>()
                .ForMember(p => p.ExtName, a => a.MapFrom(x => x.MIME.RefenceExtName))
                .ForPath(p => p.MIMETypes.Id, a => a.MapFrom(x => x.MIMETypeId))
                .ForPath(p => p.MIMETypes.ContentType, a => a.MapFrom(x => x.MIME.TypeName))
                .ForPath(p => p.MIMETypes.Description, a => a.MapFrom(x => x.MIME.Description))
                .ForMember(p => p.Description, a => a.Ignore())
                .ForPath(p => p.Blob.BlobContainerName, a => a.MapFrom(x => x.BlobContainerName))
                .ForMember(p => p.Creator, a => a.Ignore())
                .ForMember(p => p.CreatorDate, a => a.MapFrom(x => x.CreationTime))
                .ForMember(p => p.ModifierId, a => a.MapFrom(x => x.LastModifierId))
                .ForMember(p => p.ModifierDate, a => a.MapFrom(x => x.LastModificationTime))
                .ForMember(p => p.Modifier, a => a.Ignore())
                .ForMember(p => p.ShareInformation, a => a.Ignore())
                .ForPath(p => p.Classification.Id, a => a.MapFrom(x => x.FileClassificationId))
                .MapExtraProperties()
                .AfterMap((s, d) =>
                {
                    foreach (string key in s.ExtraProperties.Keys)
                    {
                        if (d.ExtraProperties.ContainsKey(key))
                        {
                            d.ExtraProperties[key] = s.ExtraProperties[key];
                        }
                        else
                        {
                            d.ExtraProperties.Add(key, s.ExtraProperties[key]);
                        }
                    }

                })
                .ReverseMap()
                .ForMember(p => p.MIME, a => a.Ignore())
                .ForMember(p => p.FileClassificationId, a => a.MapFrom(x => x.Classification.Id))
                .ForMember(p => p.Classification, a => a.Ignore())
                .ForMember(p => p.MIMETypeId, a => a.MapFrom(x => x.MIMETypes.Id))
                .ForMember(p => p.BlobContainerName, a => a.MapFrom(x => x.Blob.BlobContainerName))
                .AfterMap((s, d) =>
                {
                    var addKeys = s.ExtraProperties.Keys.Except(d.ExtraProperties.Keys);
                    var removeKeys = d.ExtraProperties.Keys.Except(s.ExtraProperties.Keys);
                    var samekeys = s.ExtraProperties.Keys.Intersect(d.ExtraProperties.Keys);

                    foreach (string removekey in removeKeys)
                    {
                        d.ExtraProperties.Remove(removekey);
                    }

                    foreach (string addkey in addKeys)
                    {
                        d.ExtraProperties.Add(addkey, s.ExtraProperties[addkey]);
                    }

                    foreach (string key in samekeys)
                    {
                        if (d.ExtraProperties[key] != s.ExtraProperties[key])
                        {
                            d.ExtraProperties[key] = s.ExtraProperties[key];
                        }
                    }
                });

        }
    }
}
