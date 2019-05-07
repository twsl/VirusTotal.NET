using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nerdicus.VirusTotalNET.Models.File.Analysis;
using Nerdicus.VirusTotalNET.Models.Url;
using Nerdicus.VirusTotalNET.Tests.Base;
using Xunit;

namespace Nerdicus.VirusTotalNET.Tests
{
    public class UrlScanResultTests : TestBase
    {
        [Fact]
        public async Task ScanKnownUrl()
        {
            var fileResult = await VirusTotal.ScanUrlAsync(TestData.KnownUrls.First());
            //Assert.Equal(UrlScanResponseCode.Queued, fileResult.ResponseCode);
        }

        [Fact]
        public async Task ScanMultipleKnownUrls()
        {
            var urlScans = await VirusTotal.ScanUrlsAsync(TestData.KnownUrls);

            foreach (var urlScan in urlScans)
            {
                //Assert.Equal(UrlScanResponseCode.Queued, urlScan.ResponseCode);
            }
        }

        [Fact]
        public async Task ScanUnknownUrl()
        {
            var fileResult = await VirusTotal.ScanUrlAsync(TestData.GetUnknownUrls(1).First());
            //Assert.Equal(UrlScanResponseCode.Queued, fileResult.ResponseCode);
        }

        [Fact]
        public async Task ScanMultipleUnknownUrl()
        {
            var urlScans = await VirusTotal.ScanUrlsAsync(TestData.GetUnknownUrls(5));

            foreach (var urlScan in urlScans)
            {
                Assert.Equal(ScanFileResponseStatusCode.Queued, urlScan.Data.Attributes.Status);
            }
        }
    }
}