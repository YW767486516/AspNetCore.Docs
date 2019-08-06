using System;
using System.Threading.Tasks;
using Greet;
using Grpc.Core;
using Grpc.Net.Client;

namespace GrpcGreeterClient
{
    class Program
    {
        #region snippet
        static async Task Main(string[] args)
        {
            var address = new Uri("https://localhost:5001");

            var channel = ChannelBuilder
                .ForHttpClient(new HttpClient { BaseAddress = address })
                .SetSendMaxMessageSize(2 * 1024 * 1024) // 2 MB
                .SetReceiveMaxMessageSize(5 * 1024 * 1024) // 5 MB
                .Build();
            var client = new Greeter.GreeterClient(channel);

            var reply = await client.SayHelloAsync(
                              new HelloRequest { Name = "GreeterClient" });
            Console.WriteLine("Greeting: " + reply.Message);
            await channel.ShutdownAsync();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
        #endregion
    }
}
