using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Reflection.Metadata;

namespace ReportSystem.Collector
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("***COLLECTOR***");
            while (true) {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "date-queue",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x] Received {0}", message);
                        Console.WriteLine("Sending {0}", message);
                        factory = new ConnectionFactory() { HostName = "localhost" };
                        using (var connectionSend = factory.CreateConnection())
                        using (var channelSend = connectionSend.CreateModel())
                        {
                            channelSend.QueueDeclare(queue: "validate",
                                                 durable: false,
                                                 exclusive: false,
                                                 autoDelete: false,
                                                 arguments: null);

                            var bodySend = Encoding.UTF8.GetBytes(message);

                            channelSend.BasicPublish(exchange: "",
                                                 routingKey: "validate",
                                                 basicProperties: null,
                                                 body: bodySend);
                            Console.WriteLine(" [x] Sent {0}", message);
                        }
                    };
                    channel.BasicConsume(queue: "date-queue",
                                         autoAck: true,
                                         consumer: consumer);
               

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}
