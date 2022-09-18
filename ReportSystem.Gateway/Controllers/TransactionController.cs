using Microsoft.AspNetCore.Mvc;
using ReportSystem.Gateway.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace ReportSystem.Gateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : Controller
    {
     
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDto>> GetbranchofficesDto(string id)
        {
            var transactionDto = new TransactionDto();
            transactionDto.fecha = id;
            transactionDto.transactionId = Guid.NewGuid().ToString();
            transactionDto.errors = "no hay errores aun";
            string json = JsonConvert.SerializeObject(transactionDto);

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "date-queue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);


                string message = json;
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "date-queue",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }



            return transactionDto;
           
        }

    }
}
