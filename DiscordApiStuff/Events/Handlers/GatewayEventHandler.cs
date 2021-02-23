using System;

namespace DiscordApiStuff.Events.Handlers
{
    public sealed class GatewayEventHandler
    {
        //No args.
        public event Action Ready;

        internal void InvokeReady()
            => Ready?.Invoke();
    }
}
