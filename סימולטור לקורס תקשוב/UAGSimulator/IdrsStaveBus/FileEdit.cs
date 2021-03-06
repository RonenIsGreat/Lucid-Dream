﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDRS
{
    public static class FileEdit
    {
        public static byte[][] GetRecording(string path)
        {
            int subSegmentsNum;
            byte[] casBeamBusRecording;
            byte[][] SubSegments;


            casBeamBusRecording = File.ReadAllBytes(path);
            subSegmentsNum = casBeamBusRecording.Length / 1400;
            SubSegments = new byte[subSegmentsNum][];

            for (int i = 0; i < subSegmentsNum; i++)
            {
                SubSegments[i] = new byte[1400];
                Array.Copy(casBeamBusRecording, i * 1400, SubSegments[i], 0, 1400);
            }

            return (SubSegments);
        }
    }
}
