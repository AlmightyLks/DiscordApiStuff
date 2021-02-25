namespace DiscordApiStuff.Core
{
    internal sealed class DiscordApiInfo
    {
        //Easy configurable, easy to find
        internal static readonly string DiscordApiGatewayVersion = "8";
        internal static readonly string DiscordApiEncoding = "json";

        //Easy to interpolate
        internal static readonly string DiscordWebSocketGateway_V8 = $"wss://gateway.discord.gg/?v={DiscordApiGatewayVersion}&encoding={DiscordApiEncoding}";
    }
}