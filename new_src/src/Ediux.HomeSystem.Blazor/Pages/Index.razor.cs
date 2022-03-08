using System.Threading.Tasks;

namespace Ediux.HomeSystem.Blazor.Pages
{
    public partial class Index 
    {
        protected static System.Timers.Timer aTimer;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            aTimer = new System.Timers.Timer(1000);
            aTimer.Elapsed += CountDownTimer;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                StartTimer();
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                aTimer.Enabled = false;
                aTimer.Stop();
                aTimer.Dispose();
            }
        }
    }
}
