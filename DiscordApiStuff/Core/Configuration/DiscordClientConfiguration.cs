using DiscordApiStuff.Models.Enums;

namespace DiscordApiStuff
{
    public struct DiscordClientConfiguration
    {
        public string Token { get; init; }
        public DiscordIntent Intents { get; init; }
        public bool AutoReconnect { get; init; }
        public DiscordClientConfiguration(string token, DiscordIntent intents, bool autoReconnect)
        {
            Token = token;
            Intents = intents;
            AutoReconnect = autoReconnect;
        }
    }
}