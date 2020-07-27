using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources
{
    public class Targets_HandleData_Template_Method<G> : AbstractHandleData_TemplateMethod
    {
        ITargets_Audio_Stitcher<G> m_targets_Audio_Stitcher;
        IInterpreter<SystemTracks> m_targetsInterpreter;

        public Targets_HandleData_Template_Method(ITargets_Audio_Stitcher<G> targets_Audio_Stitcher,
            IInterpreter<SystemTracks> targetsInterpreter)
        {
            m_targets_Audio_Stitcher = targets_Audio_Stitcher;
            m_targetsInterpreter = targetsInterpreter;
        }

        public override void DataProcessing(byte[] recievedData)
        {
            m_targets_Audio_Stitcher.UpdateTargetsData(recievedData, m_targetsInterpreter);
        }

        public override void DataSerialize(byte[] recievedData)
        {
        }

        public override bool IsDataValid(byte[] recievedData)
        {
            return true;
        }
    }
}
