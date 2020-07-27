using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources
{
    public abstract class AbstractHandleData_TemplateMethod : IHandleData_TemplateMethod
    {
        public void HandleData(byte[] recievedData)
        {
            if (IsDataValid(recievedData))
            {
                DataSerialize(recievedData);
                DataProcessing(recievedData);
            }
        }

        abstract public bool IsDataValid(byte[] recievedData);

        abstract public void DataSerialize(byte[] recievedData);

        abstract public void DataProcessing(byte[] recievedData);
    }
}
