using System;
using System.Collections.Generic;
using System.Text;

namespace Nerdicus.VirusTotalNET.Exceptions
{
    public class InvalidResourceException : Exception
    {
        public InvalidResourceException(string message) : base(message) { }
    }
}
