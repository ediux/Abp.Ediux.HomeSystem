using System;
using System.Collections.Generic;
using System.Text;

using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.Models.DTOs.AutoSave
{
    public class AutoSaveDTO : EntityDto<string>, ITransientDependency
    {
        public string entityType { get; set; }

        public string elementId { get; set; }

        public string data { get; set; }
    }
}
