using SharedResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CasBeambusManager
{
    class CasBeambusUdpListener : IListener
    {
        Socket m_socket;
        string m_localIpAddress;
        string m_multicastGroup;
        const int MULTICAST_PORT = 25104;
        const int SUBSEGMENT_LENGHT = 1400;
        Thread m_worker;
        volatile bool m_workerFlag;

        public CasBeambusUdpListener(string localIpAddress, string multicastGroup)
        {
            m_localIpAddress = localIpAddress;
            m_multicastGroup = multicastGroup;
        }

        public void Connect()
        {
            m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPAddress localIPAddress = IPAddress.Parse(m_localIpAddress);
            EndPoint m_localEndPoint = (EndPoint)new IPEndPoint(localIPAddress, MULTICAST_PORT);
            m_socket.Bind(m_localEndPoint);
            IPAddress mcastAddress = IPAddress.Parse(m_multicastGroup);
            MulticastOption mcastOption = new MulticastOption(mcastAddress, localIPAddress);
            m_socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, mcastOption);
        }

        public void Disconnect()
        {
            m_socket.Disconnect(false);
            m_socket.Dispose();
        }

        public void StartLintening(IHandleData_TemplateMethod handleData_Template_method)
        {
            m_worker = new Thread(()=>
            {
                while (m_workerFlag)
                {
                    byte[] buffer = new byte[SUBSEGMENT_LENGHT];
                    m_socket.Receive(buffer);
                    handleData_Template_method.HandleData(buffer);
                }
            });

            m_workerFlag = true;
            m_worker.Start();
        }

        public void StopLintening()
        {
            m_workerFlag = false;
        }
    }
}
