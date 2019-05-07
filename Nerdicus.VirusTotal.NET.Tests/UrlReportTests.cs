using Nerdicus.VirusTotalNET.Exceptions;
using Nerdicus.VirusTotalNET.Models.Url;
using Nerdicus.VirusTotalNET.Tests.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Nerdicus.VirusTotalNET.Tests
{
    public class UrlReportTests : TestBase
    {
        [Fact]
        public async Task GetReportKnownUrl()
        {
            var urlReport = await VirusTotal.GetUrlReportAsync(TestData.KnownUrls.First());
            Assert.Equal("url", urlReport.Data.Type);
        }

        [Fact]
        public async Task GetMultipleReportKnownUrl()
        {
            var urlReports = await VirusTotal.GetUrlReportsAsync(TestData.KnownUrls);

            foreach (var urlReport in urlReports)
            {
                Assert.Equal("url", urlReport.Data.Type);
            }
        }

        [Fact]
        public async Task GetReportUnknownUrl()
        {
            await Assert.ThrowsAsync<NotFoundException>(async () => await VirusTotal.GetUrlReportAsync(TestData.GetUnknownUrls(1).First()));
        }

        [Fact]
        public async Task GetMultipleReportUnknownUrl()
        {
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                var urlReports = await VirusTotal.GetUrlReportsAsync(TestData.GetUnknownUrls(4));

                foreach (var urlReport in urlReports)
                {
                    Assert.Equal("url", urlReport.Data.Type);
                }
            });
        }

        [Fact]
        public async Task GetReportInvalidUrl()
        {
            await Assert.ThrowsAsync<InvalidResourceException>(async () => await VirusTotal.GetUrlReportAsync("."));
        }
    }
}
