using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedResources;

namespace CasBeambusManager
{
    public class CasBeambusFactory : IAudiable_Stream_Recording_Abstract_Factory<CasBeambusStruct, TargetsAudio>
    {
        public IListener GetListener()
        {
            string localIpAddress = "192.168.98.211";
            string multicastGroup = "239.0.3.4";
            return new CasBeambusUdpListener(localIpAddress, multicastGroup);
        }

        public IListener GetTargets_Listener()
        {
            string ip = "223.234.217.6";
            string userName = "user";
            string password = "user";
            return new TargetsRabbitMqListener(ip, userName, password);
        }

        public ITargets_Audio_Stitcher<CasBeambusStruct> GetTargets_Audio_Stitcher()
        {
            return new CasBeambusTargetsAudioStitcher();
        }

        public IDataBase<StreamSegmentsRawData> GetStreamDataBase()
        {
            string ip = "223.234.217.6";
            string dbName = "LucidDream";
            string collectionName = "CasBeambusRawData";
            return new CasBeambusDataBase(ip, dbName, collectionName);
        }

        public IDataBase<TargetsAudio> GetAudioDataBase()
        {
            string ip = "223.234.217.6";
            string dbName = "LucidDream";
            string collectionName = "CasAudio";
            return new CasAudioDataBase(ip, dbName, collectionName);
        }

        public IHandleData_TemplateMethod GetDataHandle_Template_Method(ITargets_Audio_Stitcher<CasBeambusStruct> audio_Stitcher,
                                                                            IDataBase<StreamSegmentsRawData> StreamDataBase)
        {
            const int CasSegmentsNumber = 10;
            var stream_Segment_Builder = new GenericStreamSegmentBuilder(CasSegmentsNumber);
            var streamInterpreter = new CasBeambusInterpreter();
            return new CasBeambusHandleData(stream_Segment_Builder, audio_Stitcher, streamInterpreter, StreamDataBase, null);
        }

        public IHandleData_TemplateMethod GetDataHandle_Template_Method(ITargets_Audio_Stitcher<CasBeambusStruct> audio_Stitcher,
                                                                     IDataBase<StreamSegmentsRawData> StreamDataBase,
                                                                     IDataBase<TargetsAudio> AudioDataBase)
        {
            const int CasSegmentsNumber = 10;
            var stream_Segment_Builder = new GenericStreamSegmentBuilder(CasSegmentsNumber);
            var streamInterpreter = new CasBeambusInterpreter();
            return new CasBeambusHandleData(stream_Segment_Builder, audio_Stitcher, streamInterpreter, StreamDataBase, AudioDataBase);
        }

        public IHandleData_TemplateMethod GetTargets_DataHandle_Template_Method(ITargets_Audio_Stitcher<CasBeambusStruct> targets_Audio_Stitcher)
        {
            var targetsInterpreter = new Targets_Interpreter();
            return new Targets_HandleData_Template_Method<CasBeambusStruct>(targets_Audio_Stitcher, targetsInterpreter);
        }
    }
}
