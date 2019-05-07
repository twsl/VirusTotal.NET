using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Nerdicus.VirusTotalNET.Exceptions;
using Nerdicus.VirusTotalNET.Helpers;
using Nerdicus.VirusTotalNET.Models.File.Analysis;
using Nerdicus.VirusTotalNET.Tests.Base;
using Xunit;

namespace Nerdicus.VirusTotalNET.Tests
{
    public class FileReportTests : TestBase
    {
        [Fact]
        public async Task GetReportKnownFile()
        {
            var fileReport = await VirusTotal.GetFileReportAsync(TestData.EICARMalware);

            Assert.Equal("file", fileReport.Data.Type);
        }

        [Fact]
        public async Task GetMultipleReportForKnownFiles()
        {
            var results = await VirusTotal.GetFileReportsAsync(TestData.KnownHashes);

            foreach (var fileReport in results)
            {
                Assert.Equal("file", fileReport.Data.Type);
            }
        }

        [Fact]
        public async Task GetReportForUnknownFile()
        {
            // Should throw not found exception, since it should not be in the database yet
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                var fileReport = await VirusTotal.GetFileReportAsync(TestData.GetRandomSHA1s(1).First());
                Assert.Equal("file", fileReport.Data.Type);
            });
        }

        [Fact]
        public async Task GetMultipleReportForUnknownFiles()
        {
            // Should throw not found exception, since it should not be in the database yet
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                var results = await VirusTotal.GetFileReportsAsync(TestData.GetRandomSHA1s(3));

                foreach (var fileReport in results)
                {
                    Assert.Equal("file", fileReport.Data.Type);
                }
            });
        }

        [Fact]
        public async void GetReportForRecentFile()
        {
            var file = TestData.GetRandomFile(16, 1).First();
            var fileResult = await VirusTotal.ScanFileAsync(file, TestData.TestFileName);

            Assert.Equal("analysis", fileResult.Data.Type);
            Assert.Equal(ScanFileResponseStatusCode.Queued, fileResult.Data.Attributes.Status);

            // Crawl untill processing is done, otherwise the link won't be valid
            Thread.Sleep(2000);
            FileScanResult result;
            do
            {
                result = await VirusTotal.AnalyzeFileAsync(fileResult.Data.Id);
                Thread.Sleep(2000);
            } while (result.Data.Attributes.Status != ScanFileResponseStatusCode.Completed);

            var fileReport = await VirusTotal.GetFileReportAsync(result.Meta?.FileInfo.Sha256 ?? HashHelper.GetSHA256(file));

            Assert.Equal("file", fileReport.Data.Type);
        }

        [Fact]
        public async void GetReportForInvalidResource()
        {
            await Assert.ThrowsAsync<InvalidResourceException>(async () => await VirusTotal.GetFileReportAsync("aaaaaaaaaaa"));
        }
    }
}
