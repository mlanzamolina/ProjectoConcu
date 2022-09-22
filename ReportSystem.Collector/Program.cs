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
using System.IO;
using ReportSystem.Collector.Models;
using System.Text.Json;
using ReportSystem.Collector.Models;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace ReportSystem.Collector
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("***COLLECTOR***");
            while (true)
            {
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

                    consumer.Received += async (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x] Received {0}", message);
                        try
                        {
                            var sales = JsonConvert.DeserializeObject<List<salesDto>>(File.ReadAllText(@"c:\MOCK_DATA.json"));

                            var result = sales.Select(obj => JsonConvert.SerializeObject(obj)).ToArray();


                            for (int j = 0; j < result.Length; j++)
                            {

                                string json = JsonConvert.SerializeObject(sales);
                                message = result[j];
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

                            }
                        }
                        catch (Exception)
                        {
                            var factory = new ConnectionFactory() { HostName = "localhost" };
                            using (var connection = factory.CreateConnection())
                            using (var channel = connection.CreateModel())
                            {
                                channel.QueueDeclare(queue: "gateway",
                                                     durable: false,
                                                     exclusive: false,
                                                     autoDelete: false,
                                                     arguments: null);

                                string m = "NO SE ENCONTRO ARCHIVO CON ESE NOMBRE";
                                var b = Encoding.UTF8.GetBytes(m);

                                channel.BasicPublish(exchange: "",
                                                     routingKey: "gateway",
                                                     basicProperties: null,
                                                     body: b);
                                Console.WriteLine(" [x] Sent {0}", m);
                            }
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
