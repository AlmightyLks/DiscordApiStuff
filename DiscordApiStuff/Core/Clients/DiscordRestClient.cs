using DiscordApiStuff.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DiscordApiStuff.Core.Clients
{
    internal class DiscordRestClient : IUser, IChannel, IGuild, IMessage
    {
        private HttpClient _httpClient;
        internal DiscordRestClient()
        {
            _httpClient = new HttpClient();
        }
        public Task BanAsync()
        {
            throw new NotImplementedException();
        }
        public Task DeleteAsync()
        {
            throw new NotImplementedException();
        }
        public Task KickAsync()
        {
            throw new NotImplementedException();
        }
        public Task SendMessageAsync()
        {
            throw new NotImplementedException();
        }
    }
}
