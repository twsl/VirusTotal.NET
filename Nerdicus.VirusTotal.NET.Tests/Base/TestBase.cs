using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Nerdicus.VirusTotalNET.Tests.Base
{
    public abstract class TestBase : IDisposable
    {

        protected TestBase()
        {
        }

        protected VirusTotal VirusTotal { get; } = new VirusTotal();

        public void Dispose()
        {
            VirusTotal.Dispose();
        }
    }
}
