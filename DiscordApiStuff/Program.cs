using DiscordApiStuff.Events.EventArgs.Interfaces;
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
                //Token = "e",
                Intents = DiscordIntent.AllUnprivileged
            });
            client.GatewayEvents.Ready += () =>
            {
                Console.WriteLine("It sent ready!");
            };
            await client.ConnectAsync();

            await Task.Delay(-1);
        }
    }
}