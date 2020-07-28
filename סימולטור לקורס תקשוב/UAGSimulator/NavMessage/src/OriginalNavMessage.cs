using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GlobalResources.BasicData;

namespace NavMessage
{
    public class OriginalNavMessage
    {
        public TimeType timeStamp;
        public int timeZone;
        public float heading;
        public float headingRate;
        public float roll;
        public float rollRate;
        public float pitch;
        public float pitchRate;
        public float heave;
        public float heaveRate;
        public float course_overe_ground;
    }
}
