using System;
using System.Threading.Tasks;
using MassTransit;
using Shared;

namespace Consumer2
{
    public class MyMessageConsumer : IConsumer<MyMessage>
    {
        public async Task Consume(ConsumeContext<MyMessage> context)
        {
            await Console.Out.WriteLineAsync($"Received: {context.Message.Text}-{context.Headers.Get<string>("kaspersheader")}");
        }
    }
}