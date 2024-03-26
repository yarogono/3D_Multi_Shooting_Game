using System.Net;

namespace ServerCore
{
    public interface IListener
    {
        public void Init(IPEndPoint endPoint, Func<Session> sessionFactory, int register = 10, int backlog = 100);
    }
}
