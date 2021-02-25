using System;

namespace DiscordApiStuff.Exceptions.Gateway
{
    internal sealed class AlreadyAuthenticatedException : Exception
    {
        public int Code { get; }
        internal AlreadyAuthenticatedException(string message) : base(message)
        {
            Code = 4005;
        }
    }
}
