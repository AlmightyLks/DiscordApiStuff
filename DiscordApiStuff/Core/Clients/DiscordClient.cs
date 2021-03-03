﻿using DiscordApiStuff.Core.Clients;
using DiscordApiStuff.Events.Handlers;
using DiscordApiStuff.Models.Classes;
using DiscordApiStuff.Payloads.Connection;
using System;
using System.Threading.Tasks;

namespace DiscordApiStuff
{
    public sealed class DiscordClient
    {
        public GuildEventHandler GuildEvents { get; }
        public ChannelEventHandler ChannelEvents { get; }
        public MemberEventHandler MemberEvents { get; }
        public MessageEventHandler MessageEvents { get; }
        public RoleEventHandler RoleEvents { get; }
        public GatewayEventHandler GatewayEvents { get; }
        public RestApiEventHandler RestApiEvents { get; }
        public DiscordRestClient DiscordRestClient { get; set; }

        internal DiscordClientConfiguration DiscordClientConfiguration;
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
            GatewayEvents = new GatewayEventHandler();
            RestApiEvents = new RestApiEventHandler();

            DiscordClientConfiguration = discordClientConfiguration;
            DiscordRestClient = new DiscordRestClient(
                this,
                RestApiEvents
                );
            _discordWebSocket = new DiscordWebSocket(
                DiscordClientConfiguration,
                GuildEvents,
                ChannelEvents,
                MemberEvents,
                MessageEvents,
                RoleEvents,
                GatewayEvents,
                DiscordRestClient
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
                Console.WriteLine($"ConnectAsync failed\n{e}");
            }
        }
        public void Disconnect()
        {
            try
            {
                _discordWebSocket.Disconnect();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Disconnect failed\n{e}");
            }
        }
    }
}