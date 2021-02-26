using DiscordApiStuff.Models.Interfaces;
using DiscordApiStuff.Models.Structs;
using System.Net.Http;
using System.Threading.Tasks;

namespace DiscordApiStuff.Core.Clients
{
    internal class DiscordRestClient : IGuildMember, IChannel, IGuild, IMessage
    {
        private HttpClient _httpClient;
        internal DiscordRestClient()
        {
            _httpClient = new HttpClient();
        }
        async Task IDiscordUser.DirectMessage(DiscordUser user)
        {

        }
        async Task IGuildMember.BanAsync(GuildMember member)
        {

        }
        async Task IGuildMember.KickAsync(GuildMember member)
        {

        }
        async Task IMessage.DeleteAsync()
        {

        }
        async Task IChannel.SendMessageAsync()
        {

        }
    }
}
