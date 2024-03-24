using System.Net.Sockets;
using System.Net;
using Microsoft.Extensions.Logging;
using ZLogger;

namespace ServerCore
{
    public class Connector
    {
        Func<Session> _sessionFactory;

        SocketAsyncEventArgsPool ReceiveEventArgsPool;
        SocketAsyncEventArgsPool SendEventArgsPool;

        private readonly ILogger<Connector> _logger;

        public Connector()
        {
            _logger = LoggerConfig.Factory.CreateLogger<Connector>();
        }

        public void Connect(IPEndPoint endPoint, Func<Session> sessionFactory, int count = 1)
        {
            CreateEventArgsPool(count);
            for (int i = 0; i < count; i++)
            {
                Socket socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                _sessionFactory = sessionFactory;

                SocketAsyncEventArgs args = new SocketAsyncEventArgs();
                args.Completed += OnConnectCompleted;
                args.RemoteEndPoint = endPoint;
                args.UserToken = socket;

                RegisterConnect(args);
            }
        }

        void RegisterConnect(SocketAsyncEventArgs args)
        {
            Socket socket = args.UserToken as Socket;
            if (socket == null)
                return;

            try
            {
                bool pending = socket.ConnectAsync(args);
                if (pending == false)
                    OnConnectCompleted(null, args);
            }
            catch (Exception ex)
            {
                _logger.ZLogError($"Connector: {ex.Message}");
            }
        }

        void OnConnectCompleted(object sender, SocketAsyncEventArgs args)
        {
            try
            {
                if (args.SocketError == SocketError.Success)
                {
                    Session session = _sessionFactory.Invoke();
                    session.SetEventArgs(ReceiveEventArgsPool.Pop(), SendEventArgsPool.Pop());
                    session.Start(args.ConnectSocket, ReceiveEventArgsPool, SendEventArgsPool);
                    session.OnConnected(args.RemoteEndPoint);
                }
                else
                {
                    _logger.ZLogError($"OnConnectCompleted Fail: {args.SocketError}");
                }
            }
            catch (Exception ex)
            {
                _logger.ZLogError($"Connector: {ex.Message}");
            }
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
        }
    }
}
