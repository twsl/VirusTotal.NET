using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Nerdicus.VirusTotalNET.Models.File.Analysis;
using Nerdicus.VirusTotalNET.Tests.Base;
using Xunit;

namespace Nerdicus.VirusTotalNET.Tests
{
    public class FileScanResultTests : TestBase
    {
        [Fact]
        public async Task ScanKnownFile()
        {
            var fileResult = await VirusTotal.ScanFileAsync(TestData.EICARMalware, TestData.EICARFilename);

            Assert.Equal("analysis", fileResult.Data.Type);
            Assert.Equal(ScanFileResponseStatusCode.Queued, fileResult.Data.Attributes.Status);
        }

        [Fact]
        public async Task ScanTestFile()
        {
            var fileResult = await VirusTotal.ScanFileAsync(TestData.TestFile, TestData.TestFileName);

            Assert.Equal("analysis", fileResult.Data.Type);
            Assert.Equal(ScanFileResponseStatusCode.Queued, fileResult.Data.Attributes.Status);
        }

        [Fact]
        public async Task ScanUnknownFile()
        {
            var fileResult = await VirusTotal.ScanFileAsync(TestData.GetRandomFile(128, 1).First(), TestData.TestFileName);

            Assert.Equal("analysis", fileResult.Data.Type);
            Assert.Equal(ScanFileResponseStatusCode.Queued, fileResult.Data.Attributes.Status);
        }


        [Fact(Skip = "Takes too long and should only be called manually")]
        public async Task ScanKnownFileWaitForComplete()
        {
            var fileResult = await VirusTotal.ScanFileAsync(TestData.EICARMalware, TestData.EICARFilename);

            Assert.Equal("analysis", fileResult.Data.Type);
            Assert.Equal(ScanFileResponseStatusCode.Queued, fileResult.Data.Attributes.Status);

            Thread.Sleep(2000);
            FileScanResult result;
            do
            {
                result = await VirusTotal.AnalyzeFileAsync(fileResult.Data.Id);
                Thread.Sleep(2000);
            } while (result.Data.Attributes.Status != ScanFileResponseStatusCode.Completed);
        }
    }
}
