using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQLib
{
    public static class Topic
    {
        public static void SendQueue(ConnectionFactory factory, string exchange, string queueName, string body)
        {
            IConnection connection = factory.CreateConnection();
            IModel channel = connection.CreateModel();

            IBasicProperties properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            SendQueue(channel, properties, exchange, queueName, body);
        }

        public static void SendQueue(IModel channel, IBasicProperties properties, string exchange, string queueName, string body)
        {
            byte[] content = Encoding.Default.GetBytes(body);
            channel.BasicPublish(exchange: exchange, routingKey: queueName, basicProperties: properties, body: content);
        }
    }
}
