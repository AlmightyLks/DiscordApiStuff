using System.Threading.Tasks;

namespace DiscordApiStuff.Models.Interfaces
{
    internal interface IMessage : IRestInteractable
    {
        Task DeleteAsync();
    }
}