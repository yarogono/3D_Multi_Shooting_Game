using ServerCore;
using System.Net;

namespace DummyClient
{
    class Program
    {
        static int DummyClientCount { get; } = 500;

        static void Main(string[] args)
        {
            Thread.Sleep(5000);

            string host = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

            Connector connector = new Connector();

            connector.Connect(endPoint, 
                () => { return SessionManager.Instance.Generate(); },
                Program.DummyClientCount);

            while (true)
            {
                Thread.Sleep(10000);
            }
        }
    }
}