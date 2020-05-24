using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared;

namespace Consumer2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        x.AddBus(context => Bus.Factory.CreateUsingRabbitMq(cfg =>
                        {
                            cfg.Host(Config.Host, x =>
                            {
                                x.Password(Config.Password);
                                x.Username(Config.Username);
                            });
                            cfg.ReceiveEndpoint("test_queue", ep =>
                            {
                                ep.Consumer<MyMessageConsumer>();
                            });
                        }));
                    });

                    services.AddSingleton<IHostedService, BusService>();
                })
                .UseConsoleLifetime();
    }
}
