using GreenPipes;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
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
                        // add the consumer to the container
                        x.AddConsumer<MyMessageConsumer>();
                    });
                    services.AddTransient<IMyDependency, MyDependency>();
                    services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                    {
                        LoggerFactory f = new LoggerFactory();
                        f.AddProvider(new ConsoleLoggerProvider(new MCalss()));
                        cfg.SetLoggerFactory(f);
                        cfg.Host(Config.Host, x =>
                        {
                            x.Password(Config.Password);
                            x.Username(Config.Username);
                        });
                        cfg.ReceiveEndpoint("test_queue", e =>
                        {
                            e.Consumer<MyMessageConsumer>(provider);
                        });
                    }));

                    services.AddSingleton<IHostedService, BusService>();
                })
                .UseConsoleLifetime();
    }
}
