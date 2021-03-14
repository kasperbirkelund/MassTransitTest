using System;
using System.Threading.Tasks;
using MassTransit;
using Shared;
//Test

namespace Consumer2
{
    public class MyMessageConsumer2 : IConsumer<MyMessage>
    {
        public async Task Consume(ConsumeContext<MyMessage> context)
        {
            await Console.Out.WriteLineAsync($"Received: {context.Message.Text}-{context.Headers.Get<string>("kaspersheader")}");
        }
    }

    public class MyMessageConsumer : IConsumer<MyMessage>
    {
        private readonly IMyDependency _myDependency;

        public MyMessageConsumer(IMyDependency myDependency, IEventHandler<>)
        {
            _myDependency = myDependency;
        }
        public async Task Consume(ConsumeContext<MyMessage> context)
        {
            _handler.Handle()
            _myDependency.WorkBefore();
            await Console.Out.WriteLineAsync($"Received: {context.Message.Text}-{context.Headers.Get<string>("kaspersheader")}");
            _myDependency.WorkAfter();
        }
    }

    public interface IMyDependency
    {
        void WorkBefore();
        void WorkAfter();
    }

    public class MyDependency : IMyDependency
    {
        public void WorkBefore()
        {
            Console.WriteLine("min dependency");
        }

        public void WorkAfter()
        {
            Console.WriteLine("after");
        }
    }
}
