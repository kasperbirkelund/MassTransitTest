using System;
using System.Threading.Tasks;
using MassTransit;
using Shared;

namespace Consumer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IBusControl busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("rabbitmq://localhost");
                cfg.ReceiveEndpoint("test_queue", ep =>
                {
                    ep.Consumer<MyMessageConsumer>();
                });
            });

            await busControl.StartAsync();
            Console.WriteLine("Press any key to exit");
            await Task.Run(Console.ReadKey);

            await busControl.StopAsync();
        }
    }

    public class MyMessageConsumer : IConsumer<MyMessage>
    {
        public async Task Consume(ConsumeContext<MyMessage> context)
        {
            await Console.Out.WriteLineAsync($"Received: {context.Message.Text}-{context.Headers.Get<string>("kaspersheader")}");
        }
    }
}
