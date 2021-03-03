using DiscordApiStuff.Events.EventArgs.Rest;
using DiscordApiStuff.Events.Handlers;
using DiscordApiStuff.Models.Classes.Message;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DiscordApiStuff.Core.Clients
{
    public sealed class DiscordRestClient
    {
        private HttpClient _httpClient;
        private DiscordClient _discordClient;
        private RestApiEventHandler _restApiEvents;
        internal DiscordRestClient(DiscordClient discordClient, RestApiEventHandler restApiEvents)
        {
            _restApiEvents = restApiEvents;
            _discordClient = discordClient;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bot {discordClient.DiscordClientConfiguration.Token}");
        }
        public async Task GetChannelAsync(string id)
        {

        }
        internal async Task DeleteMessageAsync(DiscordMessage message)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{DiscordApiInfo.DiscordRestApi}/channels/{message.ChannelId}/messages/{message.Id}");
                if (!response.IsSuccessStatusCode)
                {
                    var evArgs = new RestHttpRequestFailureEventArgs()
                    {
                        HttpResponseContent = await response.Content.ReadAsStringAsync(),
                        HttpStatusCode = (short)response.StatusCode,
                        TypeData = new KeyValuePair<Type, object>(typeof(DiscordMessage), message),
                        Exception = null
                    };
                    _restApiEvents.InvokeHttpRequestFailed(evArgs);
                }
            }
            catch (Exception e)
            {
                var evArgs = new RestHttpRequestFailureEventArgs()
                {
                    HttpResponseContent = string.Empty,
                    HttpStatusCode = 0,
                    TypeData = new KeyValuePair<Type, object>(typeof(DiscordMessage), message),
                    Exception = e
                };
                _restApiEvents.InvokeHttpRequestFailed(evArgs);
            }
        }
    }
}
