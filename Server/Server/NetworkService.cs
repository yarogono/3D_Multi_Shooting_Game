using ServerCore;
using System.Net;

namespace Server;

public class NetworkService
{
    Listener ClientListener = new();

    Int64 ConnectdSessionCount;

    private ServerOption _serverOption;
    
    public ServerOption ServerOption
    {  
        get => _serverOption; 
        private set => _serverOption = value; 
    }

    public NetworkService()
    {
        ServerOption = new();
    }

    public void Star()
    {
        string host = Dns.GetHostName();
        IPHostEntry ipHost = Dns.GetHostEntry(host);
        IPAddress ipAddr = ipHost.AddressList[0];
        IPEndPoint endPoint = new IPEndPoint(ipAddr, _serverOption.Port);

        ClientListener.Init(endPoint, () => { return SessionManager.Instance.Generate(); }, _serverOption.MaxConnectionCount);
    }
}
