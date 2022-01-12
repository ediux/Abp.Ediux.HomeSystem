using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.Plugins.HololivePages.HoloInforamtions
{
    public class YTuberVideoDto : AuditedEntityDto<Guid>, ITransientDependency
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
    }
}
