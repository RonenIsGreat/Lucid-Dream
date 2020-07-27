using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources
{
    public interface IDataBase<G>
    {
        void Connect();

        void Disconnect();

        void Save(G data);
    }
}
