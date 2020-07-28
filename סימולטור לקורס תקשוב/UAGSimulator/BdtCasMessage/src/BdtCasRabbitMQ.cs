using GlobalResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BdtCasMessage
{
    public class BdtCasRabbitMQ : RabbitMQServer
    {
        public BdtCasRabbitMQ(string exchangeName) : base(exchangeName)
        {

        }
    }
}
