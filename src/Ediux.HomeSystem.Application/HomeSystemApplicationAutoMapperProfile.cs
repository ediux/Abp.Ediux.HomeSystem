using AutoMapper;

using Ediux.HomeSystem.AdditionalSystemFunctions4Users;
using Ediux.HomeSystem.Features.Blogs;
using Ediux.HomeSystem.Features.Blogs.DTOs;
using Ediux.HomeSystem.Features.Common;
using Ediux.HomeSystem.Features.Commons.DTOs;
using Ediux.HomeSystem.SystemManagement;

using System;
using System.IO;
using System.Linq;
using System.Text;

using Volo.Abp.Data;
using Volo.Abp.Identity;
using Volo.Abp.TenantManagement;

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
                .ForMember(x => x.Name, a => a.Ignore())
                .AfterMap((d, s) =>
                {
                    s.Name = Path.GetFileNameWithoutExtension(d.Name);

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
                .ForMember(x => x.Description, a => a.MapFrom(x => x.SystemMessages.Message))
                .ForMember(x => x.Title, a => a.MapFrom(x => x.SystemMessages.Subject))
                .ForMember(x => x.UIAction, a => a.MapFrom(x => x.SystemMessages.ActionCallbackURL))
                .ForMember(x => x.Start, a => a.MapFrom(x => x.StartTime))
                .ForMember(x => x.End, a => a.MapFrom(x => x.EndTime))
                .ForMember(x => x.Color, a => a.MapFrom(x => x.Color))
                .MapExtraProperties()
                .ReverseMap()
                .ForPath(x => x.SystemMessages.Message, a => a.MapFrom(x => x.Description))
                .ForPath(x => x.SystemMessages.Subject, a => a.MapFrom(x => x.Title))
                .ForPath(x => x.SystemMessages.ActionCallbackURL, a => a.MapFrom(x => x.UIAction))
                .ForMember(x => x.Color, a => a.MapFrom(x => x.Color))
                .MapExtraProperties();

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
                        d.ProductKey = string.Empty;
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

                  if (s != null && s.ExtraProperties != null && s.ExtraProperties.Count > 0)
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

                          //if (d.ExtraInformation.IsNullOrWhiteSpace() == false)
                          //{
                          //    d.ExtraInformation += $"<br/>{key}:{s.ExtraProperties[key]}";
                          //}
                          //else
                          //{
                          //    d.ExtraInformation += $"{key}:{s.ExtraProperties[key]}";
                          //}
                      }
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

                  if (s != null && s.ExtraProperties != null && s.ExtraProperties.Count > 0)
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
                .ForPath(p => p.Size, a => a.MapFrom(x => x.Size))
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

                    //d.Description = s.GetProperty("Description", string.Empty);
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

            CreateMap<Blogs, BlogItemDto>()
                .ReverseMap()
                .ForMember(p => p.TenantId, a => a.Ignore())
                .ForMember(p => p.Posts, a => a.MapFrom(x => x.Posts))
                .AfterMap((s, d) =>
                {
                    if (s.Tenant != null)
                    {
                        d.TenantId = s.Tenant.Id;
                        d.Tenant = null;
                    }
                });

            CreateMap<BlogPosts, BlogPostItemDto>()
                .ForPath(p => p.Blog.Id, a => a.MapFrom(x => x.BlogId))
                .ForMember(p => p.Comments, a => a.Ignore())
                .ForMember(p => p.Context, a => a.MapFrom(x => x.Content))
                .AfterMap((s, d) =>
                {

                    if (d.Tenant == null)
                    {
                        if (s.TenantId.HasValue)
                        {
                            if (d.Tenant == null)
                            {
                                d.Tenant = Activator.CreateInstance<TenantDto>();
                            }

                            d.Tenant.Id = s.TenantId.Value;
                        }
                    }

                    if (d.CoverImageMedia == null)
                    {
                        if (s.CoverImageMediaId.HasValue)
                        {
                            if (d.CoverImageMedia == null)
                            {
                                d.CoverImageMedia = Activator.CreateInstance<FileStoreDto>();
                            }

                            d.CoverImageMedia.Id = s.CoverImageMediaId.Value;
                        }
                    }

                    if (d.Author == null)
                    {
                        if (s.AuthorId != Guid.Empty)
                        {
                            d.Author = Activator.CreateInstance<UserInforamtionDto>();
                            d.Author.Id = s.AuthorId;
                        }
                    }
                })
                .ReverseMap()
                .ForMember(c => c.BlogId, a => a.MapFrom(x => x.Blog.Id))
                .ForMember(p => p.Content, a => a.MapFrom(f => f.Context))
                .AfterMap((s, d) =>
                {
                    if (s.Blog != null)
                    {
                        d.BlogId = s.Blog.Id;
                    }

                    if (s.Tenant != null)
                    {
                        if (d.TenantId.HasValue == false)
                        {
                            d.TenantId = s.Tenant.Id;
                        }

                        d.Tenant = null;
                    }

                    if (s.CoverImageMedia != null)
                    {
                        if (d.CoverImageMediaId.HasValue == false)
                        {
                            d.CoverImageMediaId = s.CoverImageMedia.Id;
                        }

                        d.CoverImageMedia = null;
                    }

                    if (s.Author != null)
                    {
                        if (d.AuthorId != s.Author.Id)
                        {
                            d.AuthorId = s.Author.Id;

                        }

                        d.Author = null;
                    }
                });

            CreateMap<Comments, CommentDto>()
                .ForMember(p => p.Post, a => a.Ignore())
                .AfterMap((s, d) =>
                {
                    if (d.Tenant == null)
                    {
                        if (s.TenantId.HasValue)
                        {
                            d.Tenant = Activator.CreateInstance<TenantDto>();
                            d.Tenant.Id = s.TenantId.Value;
                        }
                    }

                    if (d.RepliedComment == null)
                    {
                        if (s.RepliedCommentId.HasValue)
                        {
                            d.RepliedComment = Activator.CreateInstance<CommentDto>();
                            d.RepliedComment.Id = s.RepliedCommentId.Value;
                        }
                    }

                    if (d.EntityId.IsNullOrEmpty() == false)
                    {
                        if (d.Post == null)
                        {
                            d.Post = Activator.CreateInstance<BlogPostItemDto>();
                            d.Post.Id = Guid.Parse(s.EntityId);
                        }
                    }
                })
                .ReverseMap()
                .AfterMap((s, d) =>
                {
                    if (s.Tenant != null)
                    {
                        if (d.TenantId.HasValue == false)
                        {
                            d.TenantId = s.Tenant.Id;
                            d.Tenant = null;
                        }
                    }

                    if (s.RepliedComment != null)
                    {
                        if (d.RepliedCommentId.HasValue == false)
                        {
                            d.RepliedCommentId = s.RepliedComment.Id;
                            d.RepliedComment = null;
                        }
                    }
                });

            CreateMap<Tags, TagDto>()
                .AfterMap((s, d) =>
                {
                    if (d.Tenant == null)
                    {
                        if (s.TenantId.HasValue)
                        {
                            d.Tenant = Activator.CreateInstance<TenantDto>();
                            d.Tenant.Id = s.TenantId.Value;
                        }
                    }
                })
                .ReverseMap()
                .AfterMap((s, d) =>
                {
                    if (d.Tenant != null)
                    {
                        if (d.TenantId.HasValue == false)
                        {
                            d.TenantId = s.Tenant.Id;
                            d.Tenant = null;
                        }
                    }
                });

            CreateMap<EntityTags, TagEntityMapperDto>()
                .AfterMap((s, d) =>
                {
                    if (d.Tenant == null)
                    {
                        if (s.TenantId.HasValue)
                        {
                            d.Tenant = Activator.CreateInstance<TenantDto>();
                            d.Tenant.Id = s.TenantId.Value;
                        }
                    }

                    if (d.Post == null)
                    {
                        if (!string.IsNullOrEmpty(s.EntityId))
                        {
                            if (s.Tags != null)
                            {
                                switch (s.Tags.EntityType)
                                {
                                    case "BlogPost":
                                        d.Post = Activator.CreateInstance<BlogPostItemDto>();
                                        d.Post.Id = Guid.Parse(s.EntityId);
                                        break;
                                    case "Photo":
                                        d.Photo = Activator.CreateInstance<FileStoreDto>();
                                        d.Photo.Id = Guid.Parse(s.EntityId);
                                        break;
                                    case "Blog":
                                        d.Blog = Activator.CreateInstance<BlogItemDto>();
                                        d.Blog.Id = Guid.Parse(s.EntityId);
                                        break;
                                }
                            }
                        }
                    }

                })
                .ReverseMap()
                .ForMember(p => p.TagId, a => a.MapFrom(x => x.Tag.Id))
                .AfterMap((s, d) =>
                {
                    if (d.Tenant != null)
                    {
                        if (d.TenantId.HasValue == false)
                        {
                            d.TenantId = s.Tenant.Id;
                            d.Tenant = null;
                        }
                    }

                    if (s.Post != null)
                    {
                        d.EntityId = s.Post.Id.ToString();
                    }

                    if (s.Photo != null)
                    {
                        d.EntityId = s.Photo.Id.ToString();
                    }

                    if (s.Blog != null)
                    {
                        d.EntityId = s.Blog.Id.ToString();
                    }

                });

            CreateMap<Ratings, RatingsDto>()
                .AfterMap((s, d) =>
                {
                    if (d.Tenant == null)
                    {
                        if (s.TenantId.HasValue)
                        {
                            d.Tenant = Activator.CreateInstance<TenantDto>();
                            d.Tenant.Id = s.TenantId.Value;
                        }
                    }
                })
                .ReverseMap()
                .AfterMap((s, d) =>
                {
                    if (d.Tenant != null)
                    {
                        if (d.TenantId.HasValue == false)
                        {
                            d.TenantId = s.Tenant.Id;
                            d.Tenant = null;
                        }
                    }

                });

            CreateMap<Consortiums, ConsortiumDto>()
                .ReverseMap();
        }
    }
}
