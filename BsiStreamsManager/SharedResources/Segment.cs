using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources
{
    public class Segment
    {
        public byte[] Data;
        public DateTime Time;

        public Segment(byte[] data, DateTime time)
        {
            this.Data = data;
            this.Time = time;
        }
    }
}
