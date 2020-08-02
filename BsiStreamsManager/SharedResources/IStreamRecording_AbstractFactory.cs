using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources
{
    public interface IStreamRecording_AbstractFactory<G>
    {
        IListener GetListener();

        IDataBase<StreamSegmentsRawData> GetStreamDataBase();
    }
}
