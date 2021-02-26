using DiscordApiStuff.Models.Classes;
using System.Threading.Tasks;

namespace DiscordApiStuff.Models.Interfaces
{
    internal interface IDiscordUser : IRestInteractable
    {
        Task DirectMessage(DiscordUser user);
    }
}