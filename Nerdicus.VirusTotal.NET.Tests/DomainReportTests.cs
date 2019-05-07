using Nerdicus.VirusTotalNET.Exceptions;
using Nerdicus.VirusTotalNET.Models.Domain;
using Nerdicus.VirusTotalNET.Tests.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Nerdicus.VirusTotalNET.Tests
{
    public class DomainReportTests : TestBase
    {
        [Fact]
        public async Task GetDomainReportKnownDomain()
        {
            DomainReport report = await VirusTotal.GetDomainReportAsync(TestData.KnownDomains.First());
            Assert.Equal("domain", report.Data.Type);
        }

        [Fact]
        public async Task GetDomainReportUnknownDomain()
        {
            await Assert.ThrowsAsync<NotFoundException>(async () => await VirusTotal.GetDomainReportAsync(TestData.GetUnknownDomains(1).First()));
        }
    }
}
