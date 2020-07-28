using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace BeamBusFasTas
{
    public static class BeamBusFasTasSender
    {
        static int subSegmentNum;
        static byte[][] subSements;
        static UDPSocket client;

        public static void SendMessage()
        {
            subSements = FileEdit.GetRecording(Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, Properties.Settings.Default.Recording_path));
            client = new UDPSocket();
            client.Client(Properties.Settings.Default.IP,
                Properties.Settings.Default.Port);
            subSegmentNum = subSements.Length;

            while (true)
            {
                for (int j = 0; j < subSegmentNum; j++)
                {
                    client.Send(subSements[j]);
                    delayInMs(0.17066666);
                }

            }
        }

        public static void SendMessageOnce()
        {
            subSements = FileEdit.GetRecording(Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, Properties.Settings.Default.Recording_path));
            client = new UDPSocket();
            client.Client(Properties.Settings.Default.IP,
                Properties.Settings.Default.Port);
            subSegmentNum = subSements.Length;

            for (int j = 0; j < subSegmentNum; j++)
            {
                client.Send(subSements[j]);
                delayInMs(0.17066666);
            }
        }

        private static void delayInMs(double ms)
        {
            for (int i = 0; i < ms * 280000; i++)
            {

            }
        }
    }
}


