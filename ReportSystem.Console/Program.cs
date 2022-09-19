using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;

namespace ReportSystem.Console
{
    internal class Program
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly EventingBasicConsumer _consumer;
        static void Main(string[] args)
        {
            while (true)
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
            }
        }
    }
}
