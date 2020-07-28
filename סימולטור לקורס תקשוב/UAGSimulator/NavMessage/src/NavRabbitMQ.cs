using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalResources;

namespace NavMessage
{
    public class NavRabbitMQ : RabbitMQServer
    {
        public NavRabbitMQ(string exchangeName) : base(exchangeName)
        {

        }
    }
}
