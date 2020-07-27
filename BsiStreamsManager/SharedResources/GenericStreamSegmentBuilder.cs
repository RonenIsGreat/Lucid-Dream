using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources
{
    public class GenericStreamSegmentBuilder : IStreamSegmentBuilder
    {
        const int SUBSEGMENT_LENGHT = 1400;
        int m_subSegmentsNumber;
        byte[] m_segment_data;
        int m_expected_subsegment_num;

        public GenericStreamSegmentBuilder(int subsegmentsNumber)
        {
            m_subSegmentsNumber = subsegmentsNumber;
            m_segment_data = new byte[SUBSEGMENT_LENGHT * subsegmentsNumber];
            m_expected_subsegment_num = 1;
        }

        public void BuildTheStreamSegment(byte[] subsegment, Action<Segment> funcThatUseTheCreatedValidSegment)
        {
            // Current subsegment number recieved
            int subsegment_number = subsegment[4];

            // The recieved subsegment is not the next one in order
            if (subsegment_number != m_expected_subsegment_num)
            {
                m_expected_subsegment_num = 1;
            }
            else
            {
                // Append the subsegment array to the segment array
                int subSegmentIndex = subsegment_number - 1;
                Array.Copy(subsegment, 0, m_segment_data, subSegmentIndex * SUBSEGMENT_LENGHT, SUBSEGMENT_LENGHT);

                // Keep building the segment
                if (subsegment_number < m_subSegmentsNumber)
                {
                    m_expected_subsegment_num++;
                }
                // Segment is created
                else
                {
                    Segment segment = new Segment(m_segment_data, DateTime.Now);
                    funcThatUseTheCreatedValidSegment(segment);
                    m_expected_subsegment_num = 1;
                }
            }
        }
    }
}
