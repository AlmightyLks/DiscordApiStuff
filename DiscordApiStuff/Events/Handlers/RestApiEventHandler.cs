using DiscordApiStuff.Events.EventArgs.Rest;

namespace DiscordApiStuff.Events.Handlers
{
    public sealed class RestApiEventHandler
    {
        public event DiscordEventAsync<RestHttpRequestFailureEventArgs> HttpRequestFailure;
        
        internal RestApiEventHandler() { }
        
        internal void InvokeHttpRequestFailed(RestHttpRequestFailureEventArgs ev)
            => HttpRequestFailure?.Invoke(ev);
    }
}
