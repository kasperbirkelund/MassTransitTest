using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Audit;
using MassTransit.EntityFrameworkCoreIntegration.Audit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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

            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("rabbitmq://localhost");
            });
            
            DbContextOptionsBuilder<AuditDbContext> builder = new DbContextOptionsBuilder<AuditDbContext>();
            builder.UseSqlServer(cons);
            var auditStore = new EntityFrameworkAuditStore(builder.Options, "rabbitAudit");
            
            
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
