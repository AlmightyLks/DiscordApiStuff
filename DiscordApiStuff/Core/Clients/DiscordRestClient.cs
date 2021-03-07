using DiscordApiStuff.Events.EventArgs.Rest;
using DiscordApiStuff.Events.Handlers;
using DiscordApiStuff.Models.Classes.Channel;
using DiscordApiStuff.Models.Classes.Guild;
using DiscordApiStuff.Models.Classes.Message;
using DiscordApiStuff.Models.Enums;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DiscordApiStuff.Core.Clients
{
    public sealed partial class DiscordRestClient
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
        internal async Task<DiscordChannel> DeleteChannelAsync(ulong channelId)
        {
            DiscordChannel channel = null;
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{DiscordApiInfo.DiscordRestApi}/channels/{channelId}");
                if (!response.IsSuccessStatusCode)
                {
                    return channel;
                }
                string responseStr = await response.Content.ReadAsStringAsync();
                channel = JsonSerializer.Deserialize<DiscordChannel>(responseStr);
                switch (channel.Type)
                {
                    case ChannelType.GuildText:
                        {
                            GuildTextChannel guildTextChannel = JsonSerializer.Deserialize<GuildTextChannel>(responseStr);
                            break;
                        }
                    case ChannelType.DirectMessage:
                        {
                            PrivateChannel privateChannel = JsonSerializer.Deserialize<PrivateChannel>(responseStr);
                            break;
                        }
                    case ChannelType.GuildVoice:
                        {
                            GuildVoiceChannel guildVoiceChannel = JsonSerializer.Deserialize<GuildVoiceChannel>(responseStr);
                            break;
                        }
                    case ChannelType.GroupDM:
                        {
                            GroupPrivateMessage groupPrivateMessage = JsonSerializer.Deserialize<GroupPrivateMessage>(responseStr);
                            break;
                        }
                    case ChannelType.GuildCategory:
                        {
                            GuildChannel guildChannel = JsonSerializer.Deserialize<GuildChannel>(responseStr);
                            break;
                        }
                }
            }
            catch (Exception e)
            {

            }
            return channel;
        }
        internal async Task<DiscordChannel> GetChannelAsync(ulong channelId)
        {
            DiscordChannel channel = null;
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{DiscordApiInfo.DiscordRestApi}/channels/{channelId}");
                if (!response.IsSuccessStatusCode)
                {
                    return channel;
                }
                string responseStr = await response.Content.ReadAsStringAsync();
                channel = JsonSerializer.Deserialize<DiscordChannel>(responseStr);
                switch (channel.Type)
                {
                    case ChannelType.GuildText:
                        {
                            channel = JsonSerializer.Deserialize<GuildTextChannel>(responseStr);
                            channel.DiscordRestClient = this;
                            break;
                        }
                    case ChannelType.DirectMessage:
                        {
                            channel = JsonSerializer.Deserialize<PrivateChannel>(responseStr);
                            channel.DiscordRestClient = this;
                            break;
                        }
                    case ChannelType.GuildVoice:
                        {
                            channel = JsonSerializer.Deserialize<GuildVoiceChannel>(responseStr);
                            channel.DiscordRestClient = this;
                            break;
                        }
                    case ChannelType.GroupDM:
                        {
                            channel = JsonSerializer.Deserialize<GroupPrivateMessage>(responseStr);
                            channel.DiscordRestClient = this;
                            break;
                        }
                    case ChannelType.GuildCategory:
                        {
                            channel = JsonSerializer.Deserialize<GuildChannel>(responseStr);
                            channel.DiscordRestClient = this;
                            break;
                        }
                }
            }
            catch (Exception e)
            {

            }
            return channel;
        }
        public async Task<IEnumerable<GuildChannel>> GetGuildChannelsAsync(ulong guildId)
        {
            IEnumerable<GuildChannel> channels = new List<GuildChannel>();
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{DiscordApiInfo.DiscordRestApi}/guilds/{guildId}/channels");
                if (!response.IsSuccessStatusCode)
                {
                    return channels;
                }
                string responseStr = await response.Content.ReadAsStringAsync();
                //Losing derived types' property info this way.
                channels = JsonSerializer.Deserialize<IEnumerable<GuildChannel>>(responseStr);
            }
            catch (Exception e)
            {

            }
            return channels;
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
        public async Task<DiscordGuild> GetGuildAsync(ulong guildId)
        {
            DiscordGuild guild = null;
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{DiscordApiInfo.DiscordRestApi}/guilds/{guildId}");
                if (!response.IsSuccessStatusCode)
                {
                    return guild;
                }
                string responseStr = await response.Content.ReadAsStringAsync();
                guild = JsonSerializer.Deserialize<DiscordGuild>(responseStr);
                
            }
            catch (Exception e)
            {

            }
            return guild;
        }
    }

    public sealed partial class DiscordRestClient
    {
    }
}
