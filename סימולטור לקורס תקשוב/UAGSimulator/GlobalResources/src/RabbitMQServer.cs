using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Newtonsoft.Json;

namespace GlobalResources
{
    public class RabbitMQServer
    {
        ConnectionFactory Factory { get; set; }

        IConnection Connection { get; set; }

        IModel Channel { get; set; }

        protected string _exchangeName;

        public RabbitMQServer(string exchangName)
        {
            _exchangeName = exchangName;
            Factory = new ConnectionFactory() { HostName = "localhost" };
            Connection = Factory.CreateConnection();
            Channel = Connection.CreateModel();
            Channel.ExchangeDeclare(exchange: _exchangeName, type: ExchangeType.Fanout);
        }

        public void SendMessage(object message)
        {
            string json = JsonConvert.SerializeObject(message);

            var body = Encoding.UTF8.GetBytes(json);

            Channel.BasicPublish(exchange: _exchangeName,
                                 routingKey: "",
                                 basicProperties: null,
                                 body: body);
        }
    }
}
