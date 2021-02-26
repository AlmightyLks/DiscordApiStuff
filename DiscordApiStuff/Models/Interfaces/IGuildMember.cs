using DiscordApiStuff.Models.Structs;
using System.Threading.Tasks;

namespace DiscordApiStuff.Models.Interfaces
{
    internal interface IGuildMember : IDiscordUser
    {
        Task BanAsync(GuildMember member);
        Task KickAsync(GuildMember member);
    }
}