using DiscordApiStuff.Events.Handlers;
using DiscordApiStuff.Payloads.Gateway;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordApiStuff
{
    class DiscordClient
    {
        public GuildEventHandler GuildEvents { get; }
        public ChannelEventHandler ChannelEvents { get; }
        public MemberEventHandler MemberEvents { get; }
        public MessageEventHandler MessageEvents { get; }
        public RoleEventHandler RoleEvents { get; }

        private CancellationTokenSource _cancellationTokenSource;
        private DiscordClientConfiguration _discordClientConfiguration;
        private DiscordWebSocket _discordWebSocket;
        internal static readonly MinIdentification.Properties Properties;
        static DiscordClient()
        {
            Properties = new MinIdentification.Properties()
            {
                OperatingSystem = GetOsType(),
                Browser = "Wholesome",
                Device = "Wholesome"
            };

            static string GetOsType()
            {
                PlatformID pid = Environment.OSVersion.Platform;
                switch (pid)
                {
                    case PlatformID.Win32NT:
                        return "windows";
                    case PlatformID.Unix:
                        return "unix";
                    default:
                        return "unknown";
                }
            }
        }

        public DiscordClient(DiscordClientConfiguration discordClientConfiguration)
        {
            GuildEvents = new GuildEventHandler();
            ChannelEvents = new ChannelEventHandler();
            MemberEvents = new MemberEventHandler();
            MessageEvents = new MessageEventHandler();
            RoleEvents = new RoleEventHandler();

            _cancellationTokenSource = new CancellationTokenSource();
            _discordClientConfiguration = discordClientConfiguration;
            _discordWebSocket = new DiscordWebSocket(
                _discordClientConfiguration, 
                _cancellationTokenSource.Token,
                GuildEvents,
                ChannelEvents,
                MemberEvents,
                MessageEvents,
                RoleEvents
                );
        }

        public async Task ConnectAsync()
        {
            try
            {
                await _discordWebSocket.ConnectAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("Something failed.");
            }
        }
        public async Task DisconnectAsync()
        {
            try
            {
                await _discordWebSocket.DisconnectAsync();
                _cancellationTokenSource.Cancel();
                Console.WriteLine("Closed connection");
            }
            catch (Exception e)
            {
                Console.WriteLine("Something failed.");
            }
        }
    }
}