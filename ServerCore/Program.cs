using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerCore
{
    class Program
    {
        static void Main(string[] args)
        {
            //DNS
            string host = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777); //식당주소, 포트번호
            
            //문지기 휴대폰만들기
            Socket listenSocket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp); //TCP는 소켓타입을 스트림으로 만들어줘야함

            try
            {
                //문지기 교육
                listenSocket.Bind(endPoint);

                //영업 시작
                //bagLog 최대 대기수
                listenSocket.Listen(10);

                while (true)
                {
                    Console.WriteLine("Listening...");
                    //손님 입장시키기
                    Socket clientSocket = listenSocket.Accept();

                    //받는다
                    byte[] recvBuff = new byte[1024];
                    int recvBytes = clientSocket.Receive(recvBuff);
                    string recvData = Encoding.UTF8.GetString(recvBuff, 0, recvBytes);
                    Console.WriteLine($"[From Client] {recvData}");

                    //보낸다
                    byte[] sencBuff = Encoding.UTF8.GetBytes("Welcome to MMORPG Server");
                    clientSocket.Send(sencBuff);

                    //종료
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                }
            }
            catch(Exception e)
            {
                e.ToString();
            }
            
        }
    }
}
