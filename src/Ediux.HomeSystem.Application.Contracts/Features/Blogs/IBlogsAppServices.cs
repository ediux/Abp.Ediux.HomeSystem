using Ediux.HomeSystem.Features.Blogs.DTOs;

using System;
using System.Collections.Generic;
using System.Text;

using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.Features.Blogs
{
    public  interface IBlogsAppServices : ICrudAppService<BlogItemDto, Guid, BlogSearchRequestDto>, ITransientDependency
    {
    }
}
