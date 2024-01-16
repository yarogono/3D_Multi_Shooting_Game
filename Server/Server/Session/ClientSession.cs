using ServerCore;
using System.Net;
using Server.Game.Room;
using Server.Game.Object;
using Google.Protobuf.Protocol;
using Google.Protobuf;

namespace Server.Session
{
    public class ClientSession : PacketSession
    {
        public Player MyPlayer { get; set; }
        public int SessionId { get; set; }

        object _lock = new object();
        List<ArraySegment<byte>> _reserveQueue = new List<ArraySegment<byte>>();

        #region Ping Pong Check
        long _pingpongTick = 0;
        public void Ping()
        {
            if (_pingpongTick > 0)
            {
                long delta = (System.Environment.TickCount64 - _pingpongTick);
                if (delta > 30 * 1000)
                {
                    Console.WriteLine("Disconnected by PingCheck");
                    Disconnect();
                    return;
                }
            }

            S_Ping pingPacket = new S_Ping();
            DateTimeOffset pingTime = DateTimeOffset.UtcNow;
            pingPacket.ServerTimestamp = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTimeOffset(pingTime);
            Send(pingPacket);

            GameLogic.Instance.PushAfter(5000, Ping);
        }

        public void HandlePong()
        {
            _pingpongTick = System.Environment.TickCount64;
        }
        #endregion

        // 패킷 모아 보내기
        int _reservedSendBytes = 0;
        long _lastSendTick = 0;

        private const int SizeOffset = 0;
        private const int MsgIdOffset = 2;
        private const int HeaderSize = 4;
        // 예약만 하고 보내지는 않는다
        public void Send(IMessage packet)
        {
            string msgName = packet.Descriptor.Name.Replace("_", string.Empty);
            MsgId msgId = (MsgId)Enum.Parse(typeof(MsgId), msgName);
            ushort size = (ushort)packet.CalculateSize();
            byte[] sendBuffer = new byte[size + 4];
            Buffer.BlockCopy(BitConverter.GetBytes((ushort)(size + HeaderSize)), 0, sendBuffer, SizeOffset, sizeof(ushort));
            Buffer.BlockCopy(BitConverter.GetBytes((ushort)msgId), 0, sendBuffer, MsgIdOffset, sizeof(ushort));
            Buffer.BlockCopy(packet.ToByteArray(), 0, sendBuffer, HeaderSize, size);

            lock (_lock)
            {
                _reserveQueue.Add(sendBuffer);
                _reservedSendBytes += sendBuffer.Length;
            }
        }

        // 실제 Network IO 보내는 부분
        public void FlushSend()
        {
            List<ArraySegment<byte>> sendList = null;

            lock (_lock)
            {
                // 0.1초가 지났거나, 너무 패킷이 많이 모일 때 (1만 바이트)
                long delta = (System.Environment.TickCount64 - _lastSendTick);
                if (delta < 100 && _reservedSendBytes < 10000)
                    return;

                // 패킷 모아 보내기
                _reservedSendBytes = 0;
                _lastSendTick = System.Environment.TickCount64;

                sendList = _reserveQueue;
                _reserveQueue = new List<ArraySegment<byte>>();
            }

            Send(sendList);
        }

        public override void OnConnected(EndPoint endPoint)
        {
            Console.WriteLine($"OnConnected : {endPoint}");

            GameLogic.Instance.PushAfter(5000, Ping);
        }

        public override void OnDisconnected(EndPoint endPoint)
        {
            if (MyPlayer == null)
                return;

            GameLogic.Instance.Push(() =>
            {
                GameRoom room = GameLogic.Instance.Find(1);
                room.Push(MyPlayer.LeaveGame, MyPlayer.Info.ObjectId);
            });

            SessionManager.Instance.Remove(this);

            Console.WriteLine($"OnDisconnected : {endPoint}");
        }

        public override void OnRecvPacket(ArraySegment<byte> buffer)
        {
            PacketManager.Instance.OnRecvPacket(this, buffer);
        }

        public override void OnSend(int numOfBytes)
        {
            Console.WriteLine($"Transferred bytes: {numOfBytes}");
        }
    }
}
