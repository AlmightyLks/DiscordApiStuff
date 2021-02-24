using DiscordApiStuff.Events.EventArgs.Interfaces;
using System.Threading.Tasks;

namespace DiscordApiStuff.Events
{
    public delegate void DiscordEvent<T>(T EventItem) where T : IDiscordEventArgs;
    public delegate void DiscordEvent();
    public delegate Task DiscordEventAsync<T>(T EventItem) where T : IDiscordEventArgs;
    public delegate Task DiscordEventAsync();
}
