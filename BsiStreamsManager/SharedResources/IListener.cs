using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources
{
    public interface IListener
    {
        void Connect();

        void Disconnect();

        void StartLintening(IHandleData_TemplateMethod handleData_Template_method);

        void StopLintening();
    }
}
