using DiscordApiStuff.Events.EventArgs.Interfaces;
using System.Threading.Tasks;

namespace DiscordApiStuff.Events
{
    internal delegate void DiscordEvent<T>(T EventItem) where T : IDiscordEventArgs;
    internal delegate void DiscordEvent();
    internal delegate Task DiscordEventAsync<T>(T EventItem) where T : IDiscordEventArgs;
    internal delegate Task DiscordEventAsync();
}
