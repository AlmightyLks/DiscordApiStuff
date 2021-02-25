using System;

namespace DiscordApiStuff.Exceptions.Gateway
{
    internal sealed class AuthenticationFailedException : Exception
    {
        public string InvalidToken { get; }
        public int Code { get; }
        internal AuthenticationFailedException(string invalidToken, string message) : base(message)
        {
            InvalidToken = invalidToken;
            Code = 4004;
        }
    }
}