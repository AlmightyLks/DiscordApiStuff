using DiscordApiStuff.Events.EventArgs.Gateway;

namespace DiscordApiStuff.Events.Handlers
{
    public sealed class GatewayEventHandler
    {
        public event DiscordEventAsync<ReadyEventArgs> Ready;
        public event DiscordEventAsync Identifying;
        public event DiscordEventAsync Resuming;
        public event DiscordEventAsync<GatewayExceptionEventArgs> ExceptionThrown;
        
        internal GatewayEventHandler() { }
        
        internal void InvokeReady(ReadyEventArgs ev)
            => Ready?.Invoke(ev);
        internal void InvokeIdentifying()
            => Identifying?.Invoke();
        internal void InvokeResuming()
            => Resuming?.Invoke();
        internal void InvokeExceptionThrown(GatewayExceptionEventArgs ev)
            => ExceptionThrown?.Invoke(ev);
    }
}
