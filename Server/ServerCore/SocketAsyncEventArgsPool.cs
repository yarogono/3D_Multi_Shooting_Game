using System.Collections.Concurrent;
using System.Net.Sockets;

namespace ServerCore
{
    public class SocketAsyncEventArgsPool
    {
        ConcurrentBag<SocketAsyncEventArgs> Pool = new();

        public void Push(SocketAsyncEventArgs arg)
        {
            Pool.Add(arg);
        }

        public SocketAsyncEventArgs Pop()
        {
            if (Pool.TryTake(out var result))
            {
                return result;
            }

            return null;
        }
    }
}
