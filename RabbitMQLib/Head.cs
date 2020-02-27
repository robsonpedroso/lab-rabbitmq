using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQLib
{
    public static class Head
    {
        public static void SendQueue(ConnectionFactory factory, string exchange, string body, Dictionary<string, object> headers)
        {
            IConnection connection = factory.CreateConnection();
            IModel channel = connection.CreateModel();

            IBasicProperties properties = channel.CreateBasicProperties();
            properties.Persistent = true;
            properties.Headers = headers;

            SendQueue(channel, properties, exchange, body);
        }

        public static void SendQueue(IModel channel, IBasicProperties properties, string exchange, string body)
        {
            byte[] content = Encoding.Default.GetBytes(body);

            channel.BasicPublish(exchange: exchange, routingKey: string.Empty, basicProperties: properties, body: content);
        }
    }
}
