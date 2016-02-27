using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace yxOpcServer
{
    class class_socket
    {
        public static class_queue strList = new class_queue();
        public static class_queue strConfigList = new class_queue();
        public static bool bConfig = false;
        public Socket newclient;   // Socket 
        public Thread objthread;   // 监听线程
        public string ip;          // 服务器IP
        public int port;           // 服务器端口号
        public static bool bSocket = false;

        public void threadSocket(object socket)
        {
            byte[] data = new byte[1024];
            newclient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ie = new IPEndPoint(IPAddress.Parse(((class_socket)socket).ip), ((class_socket)socket).port);//服务器的IP和端口
            try
            {
                //因为客户端只是用来向特定的服务器发送信息，所以不需要绑定本机的IP和端口。不需要监听。
                newclient.Connect(ie);
            }
            catch (SocketException error)
            {
                MessageBox.Show(error.Message);
                bSocket = false;
                return;
            }
            while (bSocket)
            {
                class_queue qq = new class_queue();
                int recv = newclient.Receive(data);
                string stringdata = Encoding.ASCII.GetString(data, 0, recv);
                if (stringdata.Length > 0)
                {
                    strList.EnqueueItem(stringdata);
                    if (bConfig)
                        strConfigList.EnqueueItem(stringdata);
                }
            }
            newclient.Close();
        }

        public class_socket(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
        }

        public void Start()
        {
            objthread = new Thread(new ParameterizedThreadStart(threadSocket));
            bSocket = true;
            objthread.Start(this);
        }

        public void Stop()
        {
            if (bSocket)
                newclient.Shutdown(SocketShutdown.Both); ;
            bSocket = false;           
        }
    }
}
