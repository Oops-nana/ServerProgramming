using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerCore
{
    class Listener
    {
        Socket _listenerSocket;
        Action<Socket> _onAcceptHandler;
        
        public void Init(IPEndPoint endPoint, Action<Socket> onAcceptHandler)
        {
            _listenerSocket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _onAcceptHandler += onAcceptHandler;
            _listenerSocket.Bind(endPoint);

            //backlog : 최대 대기수
            _listenerSocket.Listen(10);

            //문지기 하나 생성
      
             SocketAsyncEventArgs args = new SocketAsyncEventArgs();
             args.Completed += new EventHandler<SocketAsyncEventArgs>(OnAcceptCompleted);
             RegisterAccept(args);
            
            
        }
        void RegisterAccept(SocketAsyncEventArgs args)
        {
            args.AcceptSocket = null;

            bool pending =  _listenerSocket.AcceptAsync(args); //성공하던 아니던 리턴을 해줌
            //누군가가 들어오면 비동기 콜백형식 연락을 줌.
            if (pending == false)
            {
                OnAcceptCompleted(null, args);
            }
        }

        void OnAcceptCompleted(object sender, SocketAsyncEventArgs args)
        {
            if (args.SocketError == SocketError.Success)
            {
                //TODO
                //유저가 들어왔을 경우 할 일
                _onAcceptHandler.Invoke(args.AcceptSocket);

            }
            else
            {
                Console.WriteLine(args.SocketError.ToString());
            }
            RegisterAccept(args);
        }

        public Socket Accept()
        {           
            return _listenerSocket;
        }
    }
}
