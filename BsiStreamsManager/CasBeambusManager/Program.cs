using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedResources;

namespace CasBeambusManager
{
    class Program
    {
        static IDataBase<StreamSegmentsRawData> m_streamDataBase;
        static IDataBase<TargetsAudio> m_audioDataBase;
        static IHandleData_TemplateMethod m_stream_TemplateMethod;
        static IListener m_stream_listener;
        static IListener m_targets_Listener;
        static IHandleData_TemplateMethod m_targets_HandleData_Template_Method;

        static void Main(string[] args)
        {
            var factory = new CasBeambusFactory();
            m_stream_listener = factory.GetListener();
            m_streamDataBase = factory.GetStreamDataBase();
            m_audioDataBase = factory.GetAudioDataBase();
            m_stream_TemplateMethod = factory.GetAudiableStreamDataHandle_TemplateMethod(m_streamDataBase, m_audioDataBase);
            m_targets_Listener = factory.GetTargets_Listener();
            m_targets_HandleData_Template_Method = factory.GetTargetsDataHandle_TemplateMethod();
            start();
        }

        static void start()
        {
            m_targets_Listener.Connect();
            m_stream_listener.Connect();
            m_streamDataBase.Connect();
            m_audioDataBase.Connect();
            m_targets_Listener.StartLintening(m_targets_HandleData_Template_Method);
            m_stream_listener.StartLintening(m_stream_TemplateMethod);
        }
    }
}
