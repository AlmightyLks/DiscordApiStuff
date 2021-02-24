using System;
using System.Threading.Tasks;

namespace DiscordApiStuff.Events.Handlers
{
    public sealed class GatewayEventHandler
    {
        //No args.
        public event DiscordEvent Ready;
        public event DiscordEvent FirstConnect;
        public event DiscordEvent Reconnect;
        public event DiscordEvent DefiniteDisconnect;

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
