using Ediux.HomeSystem.Features.Blogs.DTOs;

using System.Threading.Tasks;

namespace Ediux.HomeSystem.Blazor.Pages
{
    public partial class BlogsManager
    {

        protected string GetUrl(BlogItemDto item)
        {
            return $"/BlogPost/{item.Id}";
        }
    }
}
