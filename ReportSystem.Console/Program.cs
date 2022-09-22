using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace ReportSystem.Console
{
    internal class Program
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly EventingBasicConsumer _consumer;
        static void Main(string[] args)
        {

            System.Console.WriteLine("***FRONTEND***");
            System.Console.WriteLine("Report system\nIngrese la fecha para subir las sales: ");
            var date = System.Console.ReadLine();
            date = DateTime.Parse(date).ToString("yyyyMMdd");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44353/");
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // List all Names.
            HttpResponseMessage response = client.GetAsync($"api/transaction/{date}").Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                var products = response.Content.ReadAsStringAsync().Result;
                System.Console.WriteLine(products);
            }
            else
            {
                System.Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "gateway",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    System.Console.WriteLine(" [x] Received {0}", message);
                };
                channel.BasicConsume(queue: "gateway",
                                     autoAck: true,
                                     consumer: consumer);

                System.Console.WriteLine(" Press [enter] to exit.");
                System.Console.ReadLine();

                string fileName = @"C:\MOCK_DATA.json";

                if (File.Exists(fileName))
                {
                    try
                    {
                        File.Delete(fileName);
                    }
                    catch (Exception e)
                    {
                        //Console.WriteLine("The deletion failed: {0}", e.Message);
                    }
                }
                else
                {
                    //Console.WriteLine("Specified file doesn't exist");
                }

            }
        }
    }
}
