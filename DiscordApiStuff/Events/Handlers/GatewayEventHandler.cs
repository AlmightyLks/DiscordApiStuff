using DiscordApiStuff.Events.EventArgs.Gateway;

namespace DiscordApiStuff.Events.Handlers
{
    internal sealed class GatewayEventHandler
    {
        public event DiscordEventAsync Ready;
        public event DiscordEventAsync Identifying;
        public event DiscordEventAsync Resuming;
        public event DiscordEventAsync<GatewayExceptionEventArgs> ExceptionThrown;

        internal void InvokeReady()
            => Ready?.Invoke();
        internal void InvokeIdentifying()
            => Identifying?.Invoke();
        internal void InvokeResuming()
            => Resuming?.Invoke();
        internal void InvokeExceptionThrown(GatewayExceptionEventArgs ev)
            => ExceptionThrown?.Invoke(ev);
    }
}
