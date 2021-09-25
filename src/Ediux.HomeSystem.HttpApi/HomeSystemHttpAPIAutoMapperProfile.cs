using AutoMapper;

using Ediux.HomeSystem.Models.DTOs.PluginModule;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ediux.HomeSystem
{
    public class HomeSystemHttpAPIAutoMapperProfile : Profile
    {
        public HomeSystemHttpAPIAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
           * Alternatively, you can split your mapping configurations
           * into multiple profile classes for a better organization. */
            CreateMap<PluginModuleCreateOrUpdateDTO, PluginModuleDTO>();               
            CreateMap<PluginModuleDTO, PluginModuleCreateOrUpdateDTO>();
        }
    }
}
