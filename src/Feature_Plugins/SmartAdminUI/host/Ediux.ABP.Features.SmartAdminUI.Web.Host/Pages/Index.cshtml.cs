using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Ediux.ABP.Features.SmartAdminUI.Pages
{
    public class IndexModel : SmartAdminUIPageModel
    {
        public void OnGet()
        {
            
        }

        public async Task OnPostLoginAsync()
        {
            await HttpContext.ChallengeAsync("oidc");
        }
    }
}