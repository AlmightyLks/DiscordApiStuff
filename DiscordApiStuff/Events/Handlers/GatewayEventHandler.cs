using DiscordApiStuff.Events.EventArgs.Gateway;

namespace DiscordApiStuff.Events.Handlers
{
    public sealed class GatewayEventHandler
    {
        public event DiscordEventAsync Ready;
        public event DiscordEventAsync FirstConnect;
        public event DiscordEventAsync Reconnect;
        public event DiscordEventAsync DefiniteDisconnect;
        public event DiscordEventAsync<GatewayExceptionEventArgs> ExceptionThrown;

        internal void InvokeReady()
            => Ready?.Invoke();
        internal void InvokeDefiniteDisconnect()
            => DefiniteDisconnect?.Invoke();
        internal void InvokeFirstConnect()
            => FirstConnect?.Invoke();
        internal void InvokeReconnect()
            => Reconnect?.Invoke();
        internal void InvokeExceptionThrown(GatewayExceptionEventArgs ev)
            => ExceptionThrown?.Invoke(ev);
    }
}
