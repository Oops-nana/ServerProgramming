using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace DummyClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string host = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777); //식당주소, 포트번호
            
            while (true)
            {
                try
                {
                    //휴대폰 설정

                    Socket socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    //문지기에게 입장 문의
                    socket.Connect(endPoint); //서버가 못 받아주면 무한정 대기할 수 있음(블로킹)
                    Console.WriteLine($"Connected To {socket.RemoteEndPoint.ToString()}");

                    for (int i = 0; i < 5; i++)
                    {
                        byte[] sendBuff = Encoding.UTF8.GetBytes($"Hello World {i}");
                        int sendBytes = socket.Send(sendBuff);
                    }
                    //보낸다.


                    //보낸다.
                    byte[] recvBuff = new byte[1024];
                    int recvBytes = socket.Receive(recvBuff);
                    string recvData = Encoding.UTF8.GetString(recvBuff, 0, recvBytes);
                    Console.WriteLine($"[From Server] {recvData}");

                    //나간다.
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
                catch (Exception e)
                {
                    e.ToString();
                }
                Thread.Sleep(100);
            }
            //휴대폰 설정        S
        }
    }
}
