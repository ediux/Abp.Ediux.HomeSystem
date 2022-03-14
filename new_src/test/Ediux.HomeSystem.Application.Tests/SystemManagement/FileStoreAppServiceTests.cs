using Xunit;
using Ediux.HomeSystem.SystemManagement;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ediux.HomeSystem.SystemManagement.Tests
{
    public class FileStoreAppServiceTests : HomeSystemApplicationTestBase
    {
        private readonly IFileStoreAppService _fileStoreAppService;

        public FileStoreAppServiceTests()
        {
            _fileStoreAppService = GetRequiredService<IFileStoreAppService>();

        }
      

        [Fact()]
        public void GetAsyncTest()
        {

        }

        [Fact()]
        public void CreateAsyncTest()
        {

        }

        [Fact()]
        public void UpdateAsyncTest()
        {

        }

        [Fact()]
        public void DeleteAsyncTest()
        {

        }

        [Fact()]
        public void DownloadAsyncTest()
        {

        }

        [Fact()]
        public void GetListAsyncTest()
        {

        }

        [Fact()]
        public void GetStreamAsyncTest()
        {

        }

        [Fact()]
        public void IsExistsAsyncTest()
        {

        }

        [Fact()]
        public void IsExistsAsyncTest1()
        {

        }

        [Fact()]
        public void GetPhotosAsyncTest()
        {

        }
    }
}