using DiscordApiStuff.Events.EventArgs.Interfaces;

namespace DiscordApiStuff.Events
{
    public delegate void DiscordEvent<T>(ref T EventItem) where T : IDiscordEventArgs;
}
