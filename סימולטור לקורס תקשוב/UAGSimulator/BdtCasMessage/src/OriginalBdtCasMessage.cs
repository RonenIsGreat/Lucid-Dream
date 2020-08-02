using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GlobalResources.BasicData;


namespace BdtCasMessage
{
   public class OriginalBdtCasMessage
    {
        public TimeType timeStamp;
        public List<TrackData> systemTracks;
    }

    public struct TrackData
    {
        public long trackID;
        public State trackState;
        public TimeType creationTime;
        public float relativeBearing;
        public float relativeBearingRate;
    }
    public enum State
    {
        NewTrack,
        UpdateTrack,
        DeleteTrack
    }

}
