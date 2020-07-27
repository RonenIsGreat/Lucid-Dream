using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources
{
    public interface IStreamSegmentBuilder
    {
        void BuildTheStreamSegment(byte[] subsegment, Action<Segment> funcThatUseTheCreatedValidSegment);
    }
}
