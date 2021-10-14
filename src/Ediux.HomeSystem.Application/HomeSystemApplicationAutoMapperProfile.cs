﻿using AutoMapper;

using Ediux.HomeSystem.Data;
using Ediux.HomeSystem.Models.DTOs.MIMETypes;
using Ediux.HomeSystem.Models.DTOs.PassworkBook;
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
                .ForMember(p => p.Path, a => a.MapFrom(s => s.PluginPath));

            CreateMap<AbpPlugins, PluginModuleDTO>()
                .ForMember(p => p.PluginPath, a => a.MapFrom(s => s.Path));

            CreateMap<ProductKeysBookDTO, ProductKeys>()
                .AfterMap((s, d) => { d.ProductKey = Convert.ToBase64String(Encoding.Default.GetBytes(s.ProductKey)); });
            CreateMap<ProductKeys, ProductKeysBookDTO>()
                .AfterMap((s, d) => { d.ProductKey = Encoding.Default.GetString(Convert.FromBase64String(s.ProductKey)); });

            CreateMap<PassworkBookDTO, UserPasswordStore>()
                .ForMember(p => p.Site, a => a.MapFrom(s => s.SiteURL))
                .ForMember(p => p.SiteName, a => a.MapFrom(s => s.SiteName))
                .ForMember(p => p.Account, a => a.MapFrom(s => s.LoginAccount))
                .ForMember(p => p.Password, a => a.MapFrom(s => s.Password))
                .ForMember(p => p.IsHistory, a => a.MapFrom(s => s.IsHistory))
                .AfterMap((s, d) => { d.Password = Convert.ToBase64String(Encoding.Default.GetBytes(s.Password)); });

            CreateMap<UserPasswordStore, PassworkBookDTO>()
                .ForMember(p => p.SiteURL, a => a.MapFrom(s => s.Site))
                .ForMember(p => p.SiteName, a => a.MapFrom(s => s.SiteName))
                .ForMember(p => p.LoginAccount, a => a.MapFrom(s => s.Account))
                .ForMember(p => p.Password, a => a.MapFrom(s => s.Password))
                .ForMember(p => p.IsHistory, a => a.MapFrom(s => s.IsHistory))
                .AfterMap((s, d) =>
                {
                    d.Password = Encoding.Default.GetString(Convert.FromBase64String(s.Password));
                });

            CreateMap<MIMETypesDTO, MIMEType>();
            CreateMap<MIMEType, MIMETypesDTO>();



        }
    }
}
