using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Ediux.HomeSystem.Pages
{
    public class Index_Tests : HomeSystemWebTestBase
    {
        [Fact]
        public async Task Welcome_Page()
        {
            var response = await GetResponseAsStringAsync("/");
            response.ShouldNotBeNull();
        }
    }
}
