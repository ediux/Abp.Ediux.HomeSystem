using Xunit;
using Ediux.HomeSystem.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Shouldly;
using Volo.Abp;

namespace Ediux.HomeSystem.Services.Tests
{
    public class PluginsManagerTests : HomeSystemDomainTestBase
    {
        private readonly PluginsManager _pluginsManager;

        public PluginsManagerTests()
        {
            _pluginsManager = GetRequiredService<PluginsManager>();
        }

        [Fact()]
        public async Task ReadConfigurationAsyncTest()
        {
            var plugins = await _pluginsManager.ReadConfigurationAsync();
            plugins.ShouldNotBeNull();
        }

        [Fact()]
        public void WriteConfigurationAsyncTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void LoadPluginsAsyncTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void LoadAssemblyFileAsyncTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void GetModulesTest()
        {
            Assert.True(false, "This test needs an implementation");
        }
    }
}