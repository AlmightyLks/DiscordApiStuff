using System.Threading.Tasks;

namespace DiscordApiStuff.Models.Interfaces
{
    internal interface IChannel : IRestInteractable
    {
        Task SendMessageAsync();
    }
}