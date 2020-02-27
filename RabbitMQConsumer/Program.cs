using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQConsumer
{
    class Program
    {
        public class Account
        {
            public string Email { get; set; }
            public bool Active { get; set; }
            public DateTime CreatedDate { get; set; }
            public IList<string> Roles { get; set; }
        }

        static void Main(string[] args)
        {
            string hostName = "192.168.33.10";
            string userName = "admin";
            string password = "123";

            var factory = new ConnectionFactory()
            {
                HostName = hostName,
                UserName = userName,
                Password = password,
            };

            IConnection connection = factory.CreateConnection();
            IModel channel = connection.CreateModel();

            Consumer(channel, "queue.direct");
            Consumer(channel, "queue.fanout");
            Consumer(channel, "queue.headers");
            Consumer(channel, "queue.topic");
        }

        public static void Consumer(IModel channel, string queueName, int countWorkers = 3)
        {
            for (int i = 0; i < countWorkers; i++)
            {
                Task.Factory.StartNew(() =>
                {
                    lock (channel)
                    {
                        var consumer = new EventingBasicConsumer(channel);
                        consumer.ConsumerTag = Guid.NewGuid().ToString();
                        consumer.Received += (sender, ea) =>
                        {
                            var body = ea.Body;
                            var headers = ea.BasicProperties.Headers;

                            var otherInfo = headers["other"];

                            var brokerMessage = Encoding.Default.GetString(ea.Body);

                            Account account = JsonConvert.DeserializeObject<Account>(brokerMessage);
                            Console.WriteLine($"Fila: {queueName}; E-mail: {account.Email}; Outras informações: { otherInfo ?? string.Empty }");

                            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: true);
                        };

                        channel.BasicConsume(queueName, autoAck: false, consumer);
                    }
                });
            }
        }

        private static void ConsumerBase()
        {
            string hostName = "192.168.33.10";
            string userName = "admin";
            string password = "123";
            string queueName = "Message-Queue";
            const int NUMBER_OF_WORKROLES = 3;

            var factory = new ConnectionFactory()
            {
                HostName = hostName,
                UserName = userName,
                Password = password,
            };

            IConnection connection = factory.CreateConnection();
            IModel channel = connection.CreateModel();

            for (int i = 0; i < NUMBER_OF_WORKROLES; i++)
            {
                Task.Factory.StartNew(() =>
                {
                    lock (channel)
                    {
                        var consumer = new EventingBasicConsumer(channel);
                        consumer.ConsumerTag = Guid.NewGuid().ToString();
                        consumer.Received += (sender, ea) =>
                        {
                            var body = ea.Body;
                            var brokerMessage = Encoding.Default.GetString(ea.Body);

                            Account account = JsonConvert.DeserializeObject<Account>(brokerMessage);
                            Console.WriteLine($"Mensagem recebida com o valor: {account.Email}");

                            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: true);
                        };

                        channel.BasicConsume(queueName, autoAck: false, consumer);
                    }
                });
            }
            Console.Read();
        }
    }

}
