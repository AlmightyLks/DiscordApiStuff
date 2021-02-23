using System;

namespace DiscordApiStuff.Events.Handlers
{
    public sealed class GatewayEventHandler
    {
        //No args.
        public event Action Ready;
        public event Action FirstConnect;
        public event Action Reconnect;
        public event Action DefiniteDisconnect;

        internal void InvokeReady()
            => Ready?.Invoke();
        internal void InvokeDefiniteDisconnect()
            => DefiniteDisconnect?.Invoke();
        internal void InvokeFirstConnect()
            => FirstConnect?.Invoke();
        internal void InvokeReconnect()
            => Reconnect?.Invoke();
    }
}
