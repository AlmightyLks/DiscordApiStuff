using System;

namespace DiscordApiStuff.Exceptions
{
    public sealed class InvalidOrEmptyTokenException : Exception
    {
        public string InvalidToken { get; }
        internal InvalidOrEmptyTokenException(string invalidToken, string message) : base(message)
        {
            InvalidToken = invalidToken;
        }
    }
}