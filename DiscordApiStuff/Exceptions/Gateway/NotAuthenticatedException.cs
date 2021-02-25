using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordApiStuff.Exceptions.Gateway
{
    internal sealed class NotAuthenticatedException : Exception
    {
        public int Code { get; }
        internal NotAuthenticatedException(string message) : base(message)
        {
            Code = 4003;
        }
    }
}
