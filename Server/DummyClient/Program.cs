using ServerCore;
using System.Net;

namespace DummyClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string host = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

            Connector connector = new Connector();

            connector.Connect(endPoint, () => { return new ServerSession(); });

            while (true)
            {
                try
                {
                    SessionManager.Instance.SendForEach();
                }
                catch
                (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                Thread.Sleep(1000);
            }
        }
    }
}