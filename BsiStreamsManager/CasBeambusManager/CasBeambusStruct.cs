using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasBeambusManager
{
    public class CasBeambusStruct
    {
        public byte[][] Beams { get; set; }

        public double OwnHeading { get; set; }

        public CasBeambusStruct(byte[][] beams, double ownHeading)
        {
            Beams = beams;
            OwnHeading = ownHeading;
        }
    }
}
