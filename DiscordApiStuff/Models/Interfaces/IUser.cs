using System.Threading.Tasks;

namespace DiscordApiStuff.Models.Interfaces
{
    internal interface IUser : IRestInteractable
    {
        Task BanAsync();
        Task KickAsync();
    }
}