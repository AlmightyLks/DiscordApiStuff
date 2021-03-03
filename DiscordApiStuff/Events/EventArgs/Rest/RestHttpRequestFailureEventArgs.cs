using DiscordApiStuff.Events.EventArgs.Interfaces;
using System;
using System.Collections.Generic;

namespace DiscordApiStuff.Events.EventArgs.Rest
{
    public struct RestHttpRequestFailureEventArgs : IMessageEventArgs
    {
        public Exception Exception { get; internal set; }
        public short HttpStatusCode { get; internal set; }
        public string HttpResponseContent { get; internal set; }
        public KeyValuePair<Type, object> TypeData { get; internal set; }
    }
}
