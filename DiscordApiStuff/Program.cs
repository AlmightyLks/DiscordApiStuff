using DiscordApiStuff.Events.EventArgs.Gateway;
using DiscordApiStuff.Payloads.Models.Enums;
using System;
using System.Threading.Tasks;

namespace DiscordApiStuff
{
    class Program
    {
        static async Task Main()
        {
            DiscordClient client = new DiscordClient(new DiscordClientConfiguration()
            {
                Token = "ODEyNzU4MjYxNzkwNDA4NzI0.YDFaHQ.6YeNQbfOcqiS4Ns0dkCdpUJ1fhk",
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

            await client.ConnectAsync();

            await Task.Delay(-1);
        }
    }
}