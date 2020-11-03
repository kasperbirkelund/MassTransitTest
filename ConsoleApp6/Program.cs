using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration.Audit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using Shared;

namespace ConsoleApp6
{
    //public class MyDbContext : AuditDbContext
    //{

    //}
    public class Program
    {
        // be sure to set the C# language version to 7.3 or later
        public static async Task Main()
        {
            const string cons = @"Server=.\SQLEXPRESS;Database=Cv;Trusted_Connection=true";

            LoggerFactory f = new LoggerFactory();
            f.AddProvider(new ConsoleLoggerProvider(new MCalss()));

            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(Config.Host, x =>
                {
                    x.Password(Config.Password);
                    x.Username(Config.Username);
                });
                //cfg.Host().Settings
                cfg.SetLoggerFactory(f);
            });
            
            DbContextOptionsBuilder<AuditDbContext> builder = new DbContextOptionsBuilder<AuditDbContext>();
            builder.UseSqlServer(cons);
            var auditStore = new EntityFrameworkAuditStore(builder.Options, "rabbitAudit");
            //https://dev.to/etnicholson/developing-a-crudapi-with-asp-net-core-mongodb-docker-swagger-cf4

            busControl.ConnectSendAuditObservers(auditStore);
            //busControl.ConnectConsumeAuditObserver(auditStore);

            // Important! The bus must be started before using it!
            await busControl.StartAsync();
            try
            {
                do
                {
                    string value = await Task.Run(() =>
                    {
                        Console.WriteLine("Enter message (or quit to exit)");
                        Console.Write("> ");
                        return Console.ReadLine();
                    });

                    if ("quit".Equals(value, StringComparison.OrdinalIgnoreCase))
                    {
                        break;
                    }

                    await busControl.Publish<MyMessage>(new MyMessage
                    {
                        Text = value
                    }, ctx => {
                        ctx.Headers.Set("kaspersheader", "1234");
                    });
                }
                while (true);
            }
            finally
            {
                await busControl.StopAsync();
            }
        }
    }
}
