using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources
{
    public class StreamSegmentsRawData
    {
        public byte[] SegmentData { get; set; }

        public DateTime Date { get; set; }

        public StreamSegmentsRawData(byte[] segmentData, DateTime date)
        {
            SegmentData = segmentData;
            Date = date;
        }
    }
}
