using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQQueue
{
 
    class Program
    {
        static void Main(string[] args)
        {
            string hostName = "192.168.33.10";
            string userName = "admin";
            string password = "123";
            string queueName = "Message-Queue";

            var factory = new ConnectionFactory()
            {
                HostName = hostName,
                UserName = userName,
                Password = password,
            };

            IConnection connection = factory.CreateConnection();
            IModel channel = connection.CreateModel();

            channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            for (int i = 1; i <= 50; i++)
            {

                string json = @"{
                  'Email': 'robson@test.com',
                  'Active': true,
                  'CreatedDate': '2020-02-17T00:00:00Z',
                  'Roles': [
                    'User',
                    'Admin'
                  ]
                }";

                byte[] body = Encoding.Default.GetBytes(json);

                IBasicProperties properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(exchange: String.Empty, routingKey: queueName, basicProperties: properties, body: body);
            }

            Console.WriteLine("Mensagem enviadas para fila");
            Console.Read();
        }
    }

}
