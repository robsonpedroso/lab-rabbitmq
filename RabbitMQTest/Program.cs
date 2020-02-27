using RabbitMQ.Client;
using RabbitMQLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQQueue
{
 
    class Program
    {
        static void Main(string[] args)
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


            // Create Exchange
            channel.ExchangeDeclare("exchange.direct", ExchangeType.Direct);
            channel.ExchangeDeclare("exchange.topic", ExchangeType.Topic);
            channel.ExchangeDeclare("exchange.headers", ExchangeType.Headers);
            channel.ExchangeDeclare("exchange.fanout", ExchangeType.Fanout);

            // Create Queue
            channel.QueueDeclare("queue.direct", true, false, false, null);
            channel.QueueDeclare("queue.headers", true, false, false, null);
            channel.QueueDeclare("queue.fanout", true, false, false, null);
            channel.QueueDeclare("queue.topic", true, false, false, null);

            // Bind Queue to Exchange
            channel.QueueBind("queue.direct", "exchange.direct", "directexchange_key");
            channel.QueueBind("queue.headers", "exchange.headers", string.Empty, new Dictionary<string, object> { { "format", "txt" } });
            channel.QueueBind("queue.fanout", "exchange.fanout", string.Empty);
            channel.QueueBind("queue.topic", "exchange.topic", "one.topic.*");
            channel.QueueBind("queue.topic", "exchange.topic", "two.topic.*");
            channel.QueueBind("queue.topic", "exchange.topic", "*.atopic.*");


            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("format", "txt");

            IBasicProperties propertiesHead = channel.CreateBasicProperties();
            propertiesHead.Persistent = false;
            propertiesHead.Headers = dictionary;

            Head.SendQueue(channel: channel, properties: propertiesHead, exchange: "exchange.headers", body: json);

            Dictionary<string, object> dictionarybase = new Dictionary<string, object>();
            dictionarybase.Add("other", "info");

            IBasicProperties properties = channel.CreateBasicProperties();
            properties.Persistent = true;
            properties.Headers = dictionarybase;

            Direct.SendQueue(channel: channel, properties: properties, exchange: "exchange.direct", queueName: "directexchange_key", body: json);
            Fanout.SendQueue(channel: channel, properties: properties, exchange: "exchange.fanout", body: json);

            Topic.SendQueue(channel: channel, properties: properties, exchange: "exchange.topic", queueName: "one.topic.test", body: json);
            Topic.SendQueue(channel: channel, properties: properties, exchange: "exchange.topic", queueName: "two.topic.*", body: json);
            Topic.SendQueue(channel: channel, properties: properties, exchange: "exchange.topic", queueName: "other.atopic.test", body: json);

            Console.WriteLine("Mensagem enviadas para fila");
            Console.Read();
        }

        private static void SendQueue()
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
        }
    }
}
