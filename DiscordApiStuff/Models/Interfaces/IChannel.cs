using DiscordApiStuff.Models.Classes.Channel;
using System.Threading.Tasks;

namespace DiscordApiStuff.Models.Interfaces
{
    public interface IChannel : IRestInteractable
    {
        Task<DiscordChannel> DeleteAsync();
    }
}