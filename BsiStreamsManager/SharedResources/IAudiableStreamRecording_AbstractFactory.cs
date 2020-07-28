using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources
{
    public interface IAudiableStreamRecording_AbstractFactory<G> : IStreamRecording_AbstractFactory<G>
    {
        IListener GetTargets_Listener();

        IHandleData_TemplateMethod GetAudiableStreamDataHandle_TemplateMethod(IDataBase<StreamSegmentsRawData> StreamDataBase,
                                                                              IDataBase<TargetsAudio> AudioDataBase);

        IDataBase<TargetsAudio> GetAudioDataBase();

        IHandleData_TemplateMethod GetTargetsDataHandle_TemplateMethod();
    }
}
