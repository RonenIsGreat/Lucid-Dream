using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources
{
    public interface IHandleData_TemplateMethod
    {
        void HandleData(byte[] recievedData);
    }
}
