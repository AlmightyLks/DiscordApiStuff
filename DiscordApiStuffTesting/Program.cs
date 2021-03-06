using DiscordApiStuff;
using DiscordApiStuff.Core.Caching;
using DiscordApiStuff.Events.EventArgs.Gateway;
using DiscordApiStuff.Events.EventArgs.Guild;
using DiscordApiStuff.Events.EventArgs.Message;
using DiscordApiStuff.Events.EventArgs.Rest;
using DiscordApiStuff.Models.Classes.Channel;
using DiscordApiStuff.Models.Classes.Message;
using DiscordApiStuff.Models.Enums;
using System;
using System.Threading.Tasks;

namespace DiscordApiStuffTesting
{
    class Program
    {
        static async Task Main()
        {
            DiscordClient client = new DiscordClient(new DiscordClientConfiguration()
            {
                Token = "ODEyNzU4MjYxNzkwNDA4NzI0.YDFaHQ._VKiNmAqfpmbKlB949p2d-uBIbQ",
                Intents = DiscordIntent.AllUnprivileged
            });

            client.GatewayEvents.ExceptionThrown += (GatewayExceptionEventArgs ev) =>
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"EXCEPTION THROWN:\n{ev.Exception}");
                Console.ResetColor();

                return Task.CompletedTask;
            };
            client.GatewayEvents.Identifying += () =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Identifying");
                Console.ResetColor();

                return Task.CompletedTask;
            };
            client.GatewayEvents.Ready += async (ReadyEventArgs ev) =>
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Ready!");
                Console.ResetColor();

                var smth = await client._discordRestClient.GetGuildChannelsAsync(813892541140697116);
            };

            client.GuildEvents.GuildCreated += (GuildCreatedEventArgs ev) =>
            {
                return Task.CompletedTask;
            };
            client.GuildEvents.GuildUpdated += (GuildUpdatedEventArgs ev) =>
            {
                return Task.CompletedTask;
            };
            client.GuildEvents.GuildDeleted += (GuildDeletedEventArgs ev) =>
            {
                return Task.CompletedTask;
            };

            client.MessageEvents.MessageCreated += (MessageCreatedEventArgs ev) =>
            {
                Console.WriteLine($"\n{ev.Message.Author.Username} said \"{ev.Message.Content}\" in {ev.Message.ChannelId}!\nTTS? {ev.Message.TextToSpeech}\n");
                return Task.CompletedTask;
            };
            client.MessageEvents.MessageEdited += (MessageEditedEventArgs ev) =>
            {
                return Task.CompletedTask;
            };
            client.MessageEvents.MessageDeleted += (MessageDeletedEventArgs ev) =>
            {
                return Task.CompletedTask;
            };


            await client.ConnectAsync();

            await Task.Delay(-1);
        }
    }
}
