using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources
{
    public interface ITargets_Audio_Stitcher<G>
    {
        void UpdateTargetsData(byte[] targets, IInterpreter<SystemTracks> targetsInterpreter);

        TargetsAudio GetAudioForTargets(byte[] segment, IInterpreter<G> segmentInterpreter);
    }
}
