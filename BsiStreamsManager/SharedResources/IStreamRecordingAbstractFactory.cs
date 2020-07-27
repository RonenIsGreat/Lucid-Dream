using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources
{
    public interface IStream_Recording_Abstract_Factory<G>
    {
        IListener GetListener();

        IHandleData_TemplateMethod GetDataHandle_Template_Method(ITargets_Audio_Stitcher<G> audio_Stitcher,
                                                                    IDataBase<StreamSegmentsRawData> StreamDataBase);


        IDataBase<StreamSegmentsRawData> GetStreamDataBase();
    }
}
