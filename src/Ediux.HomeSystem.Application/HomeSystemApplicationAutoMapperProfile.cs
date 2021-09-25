using AutoMapper;

using Ediux.HomeSystem.Data;
using Ediux.HomeSystem.Models.DTOs.PluginModule;

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
        }
    }
}
