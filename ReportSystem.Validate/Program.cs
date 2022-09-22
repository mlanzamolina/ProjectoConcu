using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ReportSystem.BranchOffices.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;

namespace ReportSystem.Validate
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("***VALIDATE***");
            char[] charsToTrim = { '[', ']' };

            string errores = " ";
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "validate",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    //Console.WriteLine(" [x] Received {0}", message);

                    salesDto sls = JsonConvert.DeserializeObject<salesDto>(message);
                    string status = "Invalid sale";

                    EmpleadoDto emp=new EmpleadoDto();
                    bool empHay = true;

                    if (sls.username != null)
                    {
                        string username = GetSellerUserName(sls.username);

                        string result = username.Trim(charsToTrim);
                        try
                        {

                            emp = JsonConvert.DeserializeObject<EmpleadoDto>(result);
                            if (emp == null)
                            {
                                empHay = false;
                            }
                            //Console.WriteLine(GetSellerUserName(emp.username).Trim(charsToTrim));
                        }
                        catch 
                        {
                        }

                        /*  Console.WriteLine(sls.division_id);
                         Console.WriteLine (emp.branchOfficeId);*/

                        if (username != null && empHay )
                        {
                            if (sls.division_id == emp.branchOfficeId) 
                            {
                                carsDto cartemp = JsonConvert.DeserializeObject<carsDto>(GetCarVin(sls.idCars));
                                Console.WriteLine(GetCarVin(sls.idCars));
                                if (sls.VIN == cartemp.VIN && emp.branchOfficeId == cartemp.branchOfficeId)
                                {

                                    if (sls.buyer_name != null && sls.buyer_id != null && sls.buyer_last_name != null)
                                    {

                                        //all good
                                        errores = "Sin Errores!!";

                                    }
                                    else
                                    {
                                        errores = " nombre del comprador incorrecto | id de comprador incorrecto | apellido del comprador incorrecto ";
                                    }
                                }
                                else
                                {
                                    errores = " VIN del auto incorrecto y/o el carro no pertenece a esa sucursal ";
                                }

                            }
                           
                        }
                        else
                        {
                            errores = " empleado username nulo y/o el empleado no pertenece a esa sucursal ";
                        }

                    }
                    else 
                    {
                        errores = " username incorrecto";
                    }
                    using (var connection = factory.CreateConnection())
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(queue: "gateway",
                                             durable: false,
                                             exclusive: false,
                                             autoDelete: false,
                                             arguments: null);

                        string message2 = errores + " Name: " + sls.buyer_name + " LastName: " + sls.buyer_last_name + " Car Price: " + sls.price;
                        var body2 = Encoding.UTF8.GetBytes(message2);

                        channel.BasicPublish(exchange: "",
                                             routingKey: "gateway",
                                             basicProperties: null,
                                             body: body2);
                        Console.WriteLine(" [x] Sent {0}", message2);
                    }
                };
                channel.BasicConsume(queue: "validate",
                autoAck: true,
                consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }

        public static string GetSellerUserName(string request)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44316/");
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            /// getUser
            HttpResponseMessage response = client.GetAsync($"api/empleados/?username={request}").Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                var products = response.Content.ReadAsStringAsync().Result;
                return products;
            }
            else
            {
                System.Console.WriteLine("{0} Empleado ({1})", (int)response.StatusCode, response.ReasonPhrase);
               
                return null;
            }
        }



        public static string GetCarVin(string request)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44316/");
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            /// getUser
            HttpResponseMessage response = client.GetAsync($"api/cars/{request}").Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                var products = response.Content.ReadAsStringAsync().Result;
                //System.Console.WriteLine(products);
                return products;
            }
            else
            {
                System.Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                return null;
            }
        }
    }
}
