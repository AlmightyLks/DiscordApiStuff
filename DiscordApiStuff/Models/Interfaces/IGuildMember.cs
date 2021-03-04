using DiscordApiStuff.Models.Classes.Guild;
using System.Threading.Tasks;

namespace DiscordApiStuff.Models.Interfaces
{
    internal interface IGuildMember : IDiscordUser
    {
        Task BanAsync(GuildMember member);
        Task KickAsync(GuildMember member);
    }
}