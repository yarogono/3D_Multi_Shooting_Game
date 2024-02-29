using System.Net;
using System.Net.Sockets;

namespace ServerCore;

public class NetworkService
{
    SocketAsyncEventArgsPool ReceiveEventArgsPool;
    SocketAsyncEventArgsPool SendEventArgsPool; 

    Listener ClientListener = new();

    Socket _listenSocket;

    public ServerOption ServerOpt { get; private set; }

    Int64 ConnectdSessionCount;

    public NetworkService(ServerOption serverOption)
    {
        ServerOpt = serverOption;
    }

    public void Initialize()
    {
        CreateEventArgsPool(ServerOpt.MaxConnectionCount, ServerOpt.ReceiveBufferSize);
    }

    public void Start(string host, int port, int backlog, bool isNonDelay, Func<Session> sessionFactory)
    {

        IPHostEntry ipHost = Dns.GetHostEntry(host);
        IPAddress ipAddr = ipHost.AddressList[0];
        IPEndPoint endPoint = new IPEndPoint(ipAddr, port);

        ClientListener = new Listener();
        ClientListener.Init(endPoint, sessionFactory);
    }

    void CreateEventArgsPool(int maxConnectionCount, int receiveBufferSize)
    {
        const int pre_alloc_count = 1;
        int argsCount = maxConnectionCount * pre_alloc_count;
        int argsBufferSize = receiveBufferSize;

        ReceiveEventArgsPool = new SocketAsyncEventArgsPool();

        SendEventArgsPool = new SocketAsyncEventArgsPool();

        SocketAsyncEventArgs arg;

        for (int i = 0; i < maxConnectionCount; i++)
        {
            // ToDo: receive pool
            {
                arg = new SocketAsyncEventArgs();
                //arg.Completed += new EventHandler<SocketAsyncEventArgs>(ReceiveCompleted);

                ReceiveEventArgsPool.Push(arg);
            }


            // ToDo: send pool
            {
                //Pre-allocate a set of reusable SocketAsyncEventArgs
                arg = new SocketAsyncEventArgs();
                //arg.Completed += new EventHandler<SocketAsyncEventArgs>(OnSendCompleted);

                SendEventArgsPool.Push(arg);
            }
        }
    }

    void OnSessionClosed(Session session)
    {
        ReceiveEventArgsPool.Push(session.ReceiveEventArgs);
        SendEventArgsPool.Push(session.SendEventArgs);

        session.SetEventArgs(null, null);
    }
}
