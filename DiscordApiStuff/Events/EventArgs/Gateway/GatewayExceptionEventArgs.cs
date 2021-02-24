using DiscordApiStuff.Events.EventArgs.Interfaces;
using System;

namespace DiscordApiStuff.Events.EventArgs.Gateway
{
    public struct GatewayExceptionEventArgs : IGatewayEventArgs
    {
        public Exception Exception { get; }
        public GatewayExceptionEventArgs(Exception exception)
        {
            Exception = exception;
        }
    }
}
