using DiscordApiStuff;
using DiscordApiStuff.Events.EventArgs.Gateway;
using DiscordApiStuff.Events.EventArgs.Message;
using DiscordApiStuff.Events.EventArgs.Rest;
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
            client.GatewayEvents.Ready += () =>
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Ready!");
                Console.ResetColor();

                return Task.CompletedTask;
            };
            client.GatewayEvents.Resuming += () =>
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Resumed");
                Console.ResetColor();

                return Task.CompletedTask;
            };
            client.RestApiEvents.HttpRequestFailure += (RestHttpRequestFailureEventArgs ev) =>
            {
                Console.WriteLine("----------------");
                Console.WriteLine(
                    $"Exception: {ev.Exception}\n" +
                    $"HttpStatusCode: {ev.HttpStatusCode}\n" +
                    $"HttpResponseContent: {ev.HttpResponseContent}\n" +
                    $"TypeData: {ev.TypeData.Key},{ev.TypeData.Value}\n"
                    );
                Console.WriteLine("----------------");
                return Task.CompletedTask;
            };
            
            client.MessageEvents.MessageCreated += async (MessageCreatedEventArgs ev) =>
            {
                await ev.Message.DeleteAsync();
                Console.WriteLine($"\n{ev.Message.Author.Username} said \"{ev.Message.Content}\" in {ev.Message.ChannelId}!\nTTS? {ev.Message.TextToSpeech}\n");
            };
            await client.ConnectAsync();

            await Task.Delay(-1);
        }
    }
}
