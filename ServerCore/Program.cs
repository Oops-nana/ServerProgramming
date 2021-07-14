using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ServerCore
{
    class Program
    {
        static Listener _listener = new Listener();

        static void OnAcceptHandler(Socket clientSocket)
        {
            try
            {
                Session session = new Session();
                session.Start(clientSocket);
                
                byte[] sencBuff = Encoding.UTF8.GetBytes("Welcome to MMORPG Server");
                session.Send(sencBuff);

                Thread.Sleep(1000);

                session.Disconnect();
            } 
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
           
        }
        static void Main(string[] args)
        {
            //DNS
            string host = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777); //식당주소, 포트번호
            
            //문지기 휴대폰만들기
            //Socket listenSocket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp); //TCP는 소켓타입을 스트림으로 만들어줘야함

            
            _listener.Init(endPoint, OnAcceptHandler);
            Console.WriteLine("Listening...");
            //문지기 교육
            //_listener.Bind(endPoint);

            //영업 시작
            //bagLog 최대 대기수
            //_listener.Listen(10);

            while (true)
                {

                //손님 입장시키기
                //Socket clientSocket = _listener.Accept();
                ;
                    
                }
            }
            
        }
    }
