using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedResources;

namespace CasBeambusManager
{
    public class CasBeambusInterpreter : IInterpreter<CasBeambusStruct>
    {
        Dictionary<int, BeamsInSubSegmentENUM> m_beamsAndSubSegmentsDictionary;

        public CasBeambusInterpreter()
        {
            dictionaryInit();
        }

        public CasBeambusStruct ByteArrayToStruct(byte[] data)
        {
            const int beamsNumber = 192;
            byte[][] beams = new byte[beamsNumber][];

            // Get the 192 CAS beams from the segment data
            for (int currentBeamNumber = 1; currentBeamNumber <= beamsNumber; currentBeamNumber++)
            {
                byte[] beam = GetBeam(data, currentBeamNumber);
                beams[currentBeamNumber - 1] = beam;
            }

            // Get the own heading
            double heading = getHeading(data);

            CasBeambusStruct casBeambus = new CasBeambusStruct(beams, heading);
            return casBeambus;
        }

        private void dictionaryInit()
        {
            m_beamsAndSubSegmentsDictionary = new Dictionary<int, BeamsInSubSegmentENUM>();
            for (int i = 1; i <= 17; i++)
            {
                m_beamsAndSubSegmentsDictionary.Add(i, BeamsInSubSegmentENUM.SubSegment1);
            }
            m_beamsAndSubSegmentsDictionary.Add(18, BeamsInSubSegmentENUM.SubSegment1_2);

            for (int i = 19; i <= 39; i++)
            {
                m_beamsAndSubSegmentsDictionary.Add(i, BeamsInSubSegmentENUM.SubSegment2);
            }
            m_beamsAndSubSegmentsDictionary.Add(40, BeamsInSubSegmentENUM.SubSegment2_3);

            for (int i = 41; i <= 60; i++)
            {
                m_beamsAndSubSegmentsDictionary.Add(i, BeamsInSubSegmentENUM.SubSegment3);
            }
            m_beamsAndSubSegmentsDictionary.Add(61, BeamsInSubSegmentENUM.SubSegment3_4);

            for (int i = 62; i <= 82; i++)
            {
                m_beamsAndSubSegmentsDictionary.Add(i, BeamsInSubSegmentENUM.SubSegment4);
            }

            for (int i = 83; i <= 103; i++)
            {
                m_beamsAndSubSegmentsDictionary.Add(i, BeamsInSubSegmentENUM.SubSegment5);
            }
            m_beamsAndSubSegmentsDictionary.Add(104, BeamsInSubSegmentENUM.SubSegment5_6);

            for (int i = 105; i <= 124; i++)
            {
                m_beamsAndSubSegmentsDictionary.Add(i, BeamsInSubSegmentENUM.SubSegment6);
            }
            m_beamsAndSubSegmentsDictionary.Add(125, BeamsInSubSegmentENUM.SubSegment6_7);

            for (int i = 126; i <= 146; i++)
            {
                m_beamsAndSubSegmentsDictionary.Add(i, BeamsInSubSegmentENUM.SubSegment7);
            }
            m_beamsAndSubSegmentsDictionary.Add(147, BeamsInSubSegmentENUM.SubSegment7_8);

            for (int i = 148; i <= 167; i++)
            {
                m_beamsAndSubSegmentsDictionary.Add(i, BeamsInSubSegmentENUM.SubSegment8);
            }
            m_beamsAndSubSegmentsDictionary.Add(168, BeamsInSubSegmentENUM.SubSegment8_9);

            for (int i = 169; i <= 188; i++)
            {
                m_beamsAndSubSegmentsDictionary.Add(i, BeamsInSubSegmentENUM.SubSegment9);
            }
            m_beamsAndSubSegmentsDictionary.Add(189, BeamsInSubSegmentENUM.SubSegment9_10);

            for (int i = 190; i < 193; i++)
            {
                m_beamsAndSubSegmentsDictionary.Add(i, BeamsInSubSegmentENUM.SubSegment10);
            }
        }

        private byte[] GetBeam(byte[] beamBusCasData, int beamNumber)
        {
            byte[] beamToReturn = new byte[64];

            switch (m_beamsAndSubSegmentsDictionary[beamNumber])
            {
                case BeamsInSubSegmentENUM.SubSegment1:
                    Array.Copy(beamBusCasData, 256 + (64 * (beamNumber - 1)), beamToReturn, 0, 64);
                    break;

                case BeamsInSubSegmentENUM.SubSegment1_2:
                    Array.Copy(beamBusCasData, 1344, beamToReturn, 0, 56);
                    Array.Copy(beamBusCasData, 1432, beamToReturn, 56, 8);

                    break;

                case BeamsInSubSegmentENUM.SubSegment2:
                    Array.Copy(beamBusCasData, 1440 + (64 * (beamNumber - 19)), beamToReturn, 0, 64);
                    break;

                case BeamsInSubSegmentENUM.SubSegment2_3:
                    Array.Copy(beamBusCasData, 2784, beamToReturn, 0, 16);
                    Array.Copy(beamBusCasData, 2832, beamToReturn, 16, 48);
                    break;

                case BeamsInSubSegmentENUM.SubSegment3:
                    Array.Copy(beamBusCasData, 2880 + (64 * (beamNumber - 41)), beamToReturn, 0, 64);
                    break;

                case BeamsInSubSegmentENUM.SubSegment3_4:
                    Array.Copy(beamBusCasData, 4160, beamToReturn, 0, 40);
                    Array.Copy(beamBusCasData, 4232, beamToReturn, 40, 24);

                    break;

                case BeamsInSubSegmentENUM.SubSegment4:
                    Array.Copy(beamBusCasData, 4257 + (64 * (beamNumber - 62)), beamToReturn, 0, 64);
                    break;

                case BeamsInSubSegmentENUM.SubSegment4_5:
                    break;

                case BeamsInSubSegmentENUM.SubSegment5:
                    Array.Copy(beamBusCasData, 5632 + (64 * (beamNumber - 83)), beamToReturn, 0, 64);
                    break;

                case BeamsInSubSegmentENUM.SubSegment5_6:
                    Array.Copy(beamBusCasData, 6976, beamToReturn, 0, 24);
                    Array.Copy(beamBusCasData, 7032, beamToReturn, 24, 40);
                    break;

                case BeamsInSubSegmentENUM.SubSegment6:
                    Array.Copy(beamBusCasData, 7072 + (64 * (beamNumber - 105)), beamToReturn, 0, 64);
                    break;

                case BeamsInSubSegmentENUM.SubSegment6_7:
                    Array.Copy(beamBusCasData, 8352, beamToReturn, 0, 48);
                    Array.Copy(beamBusCasData, 8432, beamToReturn, 48, 16);
                    break;

                case BeamsInSubSegmentENUM.SubSegment7:
                    Array.Copy(beamBusCasData, 8448 + (64 * (beamNumber - 126)), beamToReturn, 0, 64);
                    break;

                case BeamsInSubSegmentENUM.SubSegment7_8:
                    Array.Copy(beamBusCasData, 9792, beamToReturn, 0, 8);
                    Array.Copy(beamBusCasData, 9832, beamToReturn, 8, 56);
                    break;

                case BeamsInSubSegmentENUM.SubSegment8:
                    Array.Copy(beamBusCasData, 9888 + (64 * (beamNumber - 148)), beamToReturn, 0, 64);
                    break;

                case BeamsInSubSegmentENUM.SubSegment8_9:
                    Array.Copy(beamBusCasData, 11168, beamToReturn, 0, 32);
                    Array.Copy(beamBusCasData, 11232, beamToReturn, 32, 32);
                    break;

                case BeamsInSubSegmentENUM.SubSegment9:
                    Array.Copy(beamBusCasData, 11264 + (64 * (beamNumber - 169)), beamToReturn, 0, 64);
                    break;

                case BeamsInSubSegmentENUM.SubSegment9_10:
                    Array.Copy(beamBusCasData, 12544, beamToReturn, 0, 56);
                    Array.Copy(beamBusCasData, 12632, beamToReturn, 56, 8);
                    break;

                case BeamsInSubSegmentENUM.SubSegment10:
                    Array.Copy(beamBusCasData, 12640 + (64 * (beamNumber - 190)), beamToReturn, 0, 64);
                    break;
            }

            return beamToReturn;
        }

        private double getHeading(byte[] beamBusCasData)
        {
            byte[] heading = new byte[64];
            Array.Copy(beamBusCasData, 13856, heading, 0, 64);
            return BitConverter.ToDouble(heading, 0);
        }

        enum BeamsInSubSegmentENUM
        {
            SubSegment1 = 0,
            SubSegment1_2 = 1,
            SubSegment2 = 2,
            SubSegment2_3 = 3,
            SubSegment3 = 4,
            SubSegment3_4 = 5,
            SubSegment4 = 6,
            SubSegment4_5 = 7,
            SubSegment5 = 8,
            SubSegment5_6 = 9,
            SubSegment6 = 10,
            SubSegment6_7 = 11,
            SubSegment7 = 12,
            SubSegment7_8 = 13,
            SubSegment8 = 14,
            SubSegment8_9 = 15,
            SubSegment9 = 16,
            SubSegment9_10 = 17,
            SubSegment10 = 18,
        }
    }
}
