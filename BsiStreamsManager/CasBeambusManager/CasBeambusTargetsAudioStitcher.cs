using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedResources;

namespace CasBeambusManager
{
    public class CasBeambusTargetsAudioStitcher : ITargets_Audio_Stitcher<CasBeambusStruct>
    {
        static CasBeambusTargetsAudioStitcher m_casBeambusTargetsAudioStitcher;
        SystemTracks m_targets;

        private CasBeambusTargetsAudioStitcher()
        {
        }

        public static CasBeambusTargetsAudioStitcher GetInstance()
        {
            if (m_casBeambusTargetsAudioStitcher == null)
            {
                m_casBeambusTargetsAudioStitcher = new CasBeambusTargetsAudioStitcher();
            }

            return m_casBeambusTargetsAudioStitcher;
        }

        public TargetsAudio GetAudioForTargets(byte[] segment, IInterpreter<CasBeambusStruct> segmentInterpreter)
        {
            TargetsAudio targetsAudio = new TargetsAudio();

            if (m_targets != null)
            {
                foreach (var target in m_targets.systemTracks)
                {
                    CasBeambusStruct casBeambus = segmentInterpreter.ByteArrayToStruct(segment);
                    byte[] audio = getTargetAudio(target, casBeambus);
                    TargetAudio targetAudio = new TargetAudio((int)target.trackID, audio);
                    targetsAudio.TargetsAudioList.Add(targetAudio);
                }
            }

            return targetsAudio;
        }

        public void UpdateTargetsData(byte[] targets, IInterpreter<SystemTracks> targetsInterpreter)
        {
            m_targets = targetsInterpreter.ByteArrayToStruct(targets);
        }

        private byte[] getTargetAudio(TrackData target, CasBeambusStruct casBeambus)
        {
            double trackDegree = (casBeambus.OwnHeading + target.relativeBearing) % 360;
            const double factor = 192.0 / 360.0;
            double beamNumber = trackDegree * factor;

            // Simplified version, select nearest CAS beam to the target and return it
            int selectedBeamNumber = (int)Math.Round(beamNumber);
            byte[] beam = casBeambus.Beams[selectedBeamNumber];
            return beam;

            /* Select 2 nearest beams and stitch their audio
            int beam1num = (int)(Math.Floor(beamNumber));
            int beam2num = (int)(Math.Ceiling(beamNumber));

            byte[] beam1 = casBeambus.Beams[beam1num];
            byte[] beam2 = casBeambus.Beams[beam2num];

            double beam1Precentage = Math.Abs(beam2num - beamNumber);
            double beam2Precentage = 1 - beam1Precentage;

            setVolume(beam1, beam1Precentage);
            setVolume(beam2, beam2Precentage);

            return stitchBeams(beam1, beam2);
            */
        }

        private void setVolume(byte[] buffer, double volume)
        {
            // scaling volume of buffer audio
            for (int i = 0; i < buffer.Length / 2; ++i)
            {
                // convert to 16-bit
                short sample = (short)((buffer[i * 2 + 1] << 8) | buffer[i * 2]);

                // scale
                double gain = volume; // value between 0 and 1.0
                sample = (short)(sample * gain + 0.5);

                // back to byte[]
                buffer[i * 2 + 1] = (byte)(sample >> 8);
                buffer[i * 2] = (byte)(sample & 0xff);
            }
        }

        private byte[] stitchBeams(byte[] buffer, byte[] buffer2)
        {
            byte[] mixedBuffer = new byte[buffer.Length];

            for (int i = 0; i < buffer.Length; ++i)
            {
                mixedBuffer[i] = (byte)(buffer[i] + buffer2[i]);
            }

            return mixedBuffer;
        }
    }
}
