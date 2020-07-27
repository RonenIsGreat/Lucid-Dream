using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources
{
    public interface IAudiable_Stream_Recording_Abstract_Factory<G,T> : IStream_Recording_Abstract_Factory<G>
    {
        IHandleData_TemplateMethod GetTargets_DataHandle_Template_Method(ITargets_Audio_Stitcher<G> targets_Audio_Stitcher);

        IListener GetTargets_Listener();

        IHandleData_TemplateMethod GetDataHandle_Template_Method(ITargets_Audio_Stitcher<G> audio_Stitcher,
                                                                    IDataBase<StreamSegmentsRawData> StreamDataBase,
                                                                    IDataBase<T> AudioDataBase);

        ITargets_Audio_Stitcher<G> GetTargets_Audio_Stitcher();

        IDataBase<T> GetAudioDataBase();
    }
}
