using System.Threading.Tasks;

namespace DiscordApiStuff.Models.Interfaces
{
    public interface ITextChannel : IChannel
    {
        Task SendMessageAsync();
    }
}
