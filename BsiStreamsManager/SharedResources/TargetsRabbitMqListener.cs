using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources
{
    public class TargetsRabbitMqListener : IListener
    {
        IConnection m_connection;
        string m_ip;
        string m_userName;
        string m_password;
        IModel m_trackDataChannel;

        public TargetsRabbitMqListener(string ip, string userName, string password)
        {
            m_ip = ip;
            m_userName = userName;
            m_password = password;
        }

        public void Connect()
        {
            var factory = new ConnectionFactory()
            {
                HostName = m_ip,
                UserName = m_userName,
                Password = m_password,
            };

            m_connection = factory.CreateConnection();
        }

        public void Disconnect()
        {
            m_connection.Close();
        }

        public void StartLintening(IHandleData_TemplateMethod handleData_Template_method)
        {
            m_trackDataChannel = m_connection.CreateModel();

            m_trackDataChannel.ExchangeDeclare(exchange: "LucidTrackData", type: ExchangeType.Fanout);

            m_trackDataChannel.QueueDeclare(queue: "track",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: true,
                                 arguments: null);

            m_trackDataChannel.QueueBind(queue: "track", exchange: "LucidTrackData", routingKey: "");

            var consumer = new EventingBasicConsumer(m_trackDataChannel);
            consumer.Received += (model, ea) =>
            {
                byte[] data = ea.Body.ToArray();
                handleData_Template_method.HandleData(data);
            };

            m_trackDataChannel.BasicConsume(queue: "track",
                                            autoAck: false,
                                            consumer: consumer);
        }

        public void StopLintening()
        {
            m_trackDataChannel.Close();
        }
    }
}
