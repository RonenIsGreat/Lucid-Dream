using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources
{
    public class TargetsAudio
    {
        public List<TargetAudio> TargetsAudioList { get; set; }

        public DateTime Date { get; set; }
    }

    public class TargetAudio
    {
        public int TargetNum { get; set; }

        public byte[] Audio { get; set; }

        public TargetAudio(int targetNum, byte[] audio)
        {
            TargetNum = targetNum;
            Audio = audio;
        }
    }
}
