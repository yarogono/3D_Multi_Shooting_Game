using System.Net.Sockets;
using System.Net;

namespace ServerCore
{
    public class Listener
    {
        Socket _listenSocket;
        Func<Session> _sessionFactory;

        SocketAsyncEventArgsPool ReceiveEventArgsPool;
        SocketAsyncEventArgsPool SendEventArgsPool;

        public void Init(IPEndPoint endPoint, Func<Session> sessionFactory, int register = 10, int backlog = 100)
        {
            _listenSocket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _sessionFactory += sessionFactory;

            _listenSocket.Bind(endPoint);

            _listenSocket.Listen(backlog);

            CreateEventArgsPool(register);

            for (int i = 0; i < register; i++)
            {
                SocketAsyncEventArgs args = new SocketAsyncEventArgs();
                args.Completed += new EventHandler<SocketAsyncEventArgs>(OnAcceptCompleted);
                RegisterAccept(args);
            }
        }

        void RegisterAccept(SocketAsyncEventArgs args)
        {
            args.AcceptSocket = null;

            try
            {
                bool pending = _listenSocket.AcceptAsync(args);
                if (pending == false)
                    OnAcceptCompleted(null, args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        void OnAcceptCompleted(object sender, SocketAsyncEventArgs args)
        {
            try
            {
                if (args.SocketError == SocketError.Success)
                {
                    Session session = _sessionFactory.Invoke();
                    session.SetEventArgs(ReceiveEventArgsPool.Pop(), SendEventArgsPool.Pop());
                    session.Start(args.AcceptSocket, ReceiveEventArgsPool, SendEventArgsPool); ;
                    session.OnConnected(args.AcceptSocket.RemoteEndPoint);
                }
                else
                    Console.WriteLine(args.SocketError.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            RegisterAccept(args);
        }

        void CreateEventArgsPool(int maxConnectionCount)
        {
            ReceiveEventArgsPool = new SocketAsyncEventArgsPool();

            SendEventArgsPool = new SocketAsyncEventArgsPool();

            SocketAsyncEventArgs arg;

            for (int i = 0; i < maxConnectionCount; i++)
            {
                // ReceiveEventArgsPool
                {
                    arg = new SocketAsyncEventArgs();
                    ReceiveEventArgsPool.Push(arg);
                }

                // SendEventArgsPool
                {
                    arg = new SocketAsyncEventArgs();
                    SendEventArgsPool.Push(arg);
                }
            }

            void OnSessionClosed(Session session)
            {
                ReceiveEventArgsPool.Push(session.ReceiveEventArgs);
                SendEventArgsPool.Push(session.SendEventArgs);

                session.SetEventArgs(null, null);
            }
        }
    }
}
