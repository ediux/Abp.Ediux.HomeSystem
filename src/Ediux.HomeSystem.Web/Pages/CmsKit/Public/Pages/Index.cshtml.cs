using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Volo.CmsKit.Public.Pages;

namespace Ediux.HomeSystem.Web.Pages.CmsKit.Public.Pages
{
    public class IndexModel : CommonPageModel
    {
        [BindProperty(SupportsGet = true)] 
        public string Slug { get; set; }
        
        protected IPagePublicAppService PagePublicAppService { get; }

        public PageDto DynamicPage;
        
        public IndexModel(IPagePublicAppService pagePublicAppService)
        {
            PagePublicAppService = pagePublicAppService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            DynamicPage = await PagePublicAppService.FindBySlugAsync(Slug);

            if (DynamicPage == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}