using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedResources;

namespace CasBeambusManager
{
    public class CasBeambusHandleData : AbstractHandleData_TemplateMethod
    {
        IStreamSegmentBuilder m_stream_Segment_Builder;
        ITargets_Audio_Stitcher<CasBeambusStruct> m_audio_Stitcher;
        IInterpreter<CasBeambusStruct> m_streamInterpreter;
        IDataBase<StreamSegmentsRawData> m_streamDataBase;
        IDataBase<TargetsAudio> m_audioDataBase;

        public CasBeambusHandleData(IStreamSegmentBuilder stream_Segment_Builder,
                            ITargets_Audio_Stitcher<CasBeambusStruct> audio_Stitcher,
                            IInterpreter<CasBeambusStruct> streamInterpreter,
                            IDataBase<StreamSegmentsRawData> StreamDataBase,
                            IDataBase<TargetsAudio> AudioDataBase)
        {
            m_stream_Segment_Builder = stream_Segment_Builder;
            m_audio_Stitcher = audio_Stitcher;
            m_streamInterpreter = streamInterpreter;
            m_streamDataBase = StreamDataBase;
            m_audioDataBase = AudioDataBase;
        }

        public override void DataProcessing(byte[] recievedData)
        {
            m_stream_Segment_Builder.BuildTheStreamSegment(recievedData, (segment)=>
            {
                TargetsAudio targetsAudio = m_audio_Stitcher.GetAudioForTargets(recievedData, m_streamInterpreter);
                m_audioDataBase.Save(targetsAudio);
            });
        }

        public override void DataSerialize(byte[] recievedData)
        {
            var StreamSegmentsRawData = new StreamSegmentsRawData(recievedData, DateTime.Now);
            m_streamDataBase.Save(StreamSegmentsRawData);
        }

        public override bool IsDataValid(byte[] recievedData)
        {
            if(recievedData.Length == 1400)
                return true;
            else
                return false;
        }
    }
}
