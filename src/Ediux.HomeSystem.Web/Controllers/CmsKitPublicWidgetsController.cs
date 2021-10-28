using System.Threading.Tasks;

using Ediux.HomeSystem.Web.Pages.Components.Commenting;
using Ediux.HomeSystem.Web.Pages.Components.Rating;
using Ediux.HomeSystem.Web.Pages.Components.ReactionSelection;

using Microsoft.AspNetCore.Mvc;

using Volo.CmsKit.Public;

namespace Ediux.HomeSystem.Web.Controllers
{
    public class CmsKitPublicWidgetsController : CmsKitPublicControllerBase
    {
        public Task<IActionResult> ReactionSelection(string entityType, string entityId)
        {
            return Task.FromResult((IActionResult)ViewComponent(typeof(ReactionSelectionViewComponent), new {entityType, entityId}));
        }

        public Task<IActionResult> Commenting(string entityType, string entityId)
        {
            return Task.FromResult((IActionResult)ViewComponent(typeof(CommentingViewComponent), new {entityType, entityId}));
        }

        public Task<IActionResult> Rating(string entityType, string entityId)
        {
            return Task.FromResult((IActionResult) ViewComponent(typeof(RatingViewComponent), new {entityType, entityId}));
        }
    }
}
