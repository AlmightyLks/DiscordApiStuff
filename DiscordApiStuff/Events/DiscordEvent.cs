using DiscordApiStuff.Events.EventArgs.Interfaces;
using System.Threading.Tasks;

namespace DiscordApiStuff.Events
{
    public delegate void DiscordEvent<T>(ref T EventItem) where T : IDiscordEventArgs;
    public delegate void DiscordEvent();
    public delegate Task DiscordEventAsync<T>(ref T EventItem) where T : IDiscordEventArgs;
    public delegate Task DiscordEventAsync();
}
