using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nerdicus.VirusTotalNET.Exceptions;
using Nerdicus.VirusTotalNET.Models.IP;
using Nerdicus.VirusTotalNET.Tests.Base;
using Xunit;

namespace Nerdicus.VirusTotalNET.Tests
{
    public class IPReportTests : TestBase
    {
        [Fact]
        public async Task GetIPReportKnownIPv4()
        {
            IPReport report = await VirusTotal.GetIPReportAsync(TestData.KnownIPv4s.First());
            //Assert.Equal(IPReportResponseCode.Present, report.ResponseCode);
        }

        [Fact]
        public async Task GetIPReportUnknownIPv4()
        {
            await Assert.ThrowsAsync<NotFoundException>(async () => await VirusTotal.GetIPReportAsync("128.168.238.14"));
        }

        [Fact]
        public async Task GetIPReportRandomIPv6()
        {
            //IPv6 is not supported
            await Assert.ThrowsAsync<InvalidResourceException>(async () => await VirusTotal.GetIPReportAsync(TestData.GetRandomIPv6s(1).First()));
        }
    }
}
