using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;
using System.Net;

namespace DummyClient
{
    public class ServerSession : PacketSession
    {
        public int DummyId { get; set; }

        public void Send(IMessage packet)
        {
            string msgName = packet.Descriptor.Name.Replace("_", string.Empty);
            MsgId msgId = (MsgId)Enum.Parse(typeof(MsgId), msgName);
            ushort size = (ushort)packet.CalculateSize();
            byte[] sendBuffer = new byte[size + 4];
            Array.Copy(BitConverter.GetBytes((ushort)(size + 4)), 0, sendBuffer, 0, sizeof(ushort));
            Array.Copy(BitConverter.GetBytes((ushort)msgId), 0, sendBuffer, 2, sizeof(ushort));
            Array.Copy(packet.ToByteArray(), 0, sendBuffer, 4, size);

            Send(new ArraySegment<byte>(sendBuffer));
        }

        public override void OnConnected(EndPoint endPoint)
        {
            Console.WriteLine($"OnConnected : {endPoint}");

            Vec3 posInfo = new Vec3() { X = 0, Y = 1, Z = 0 };
            CreatureState state = CreatureState.Idle;
            ObjectInfo myPlayer = new ObjectInfo() { Name = "MyPlayer", PosInfo = posInfo, State = state };
            C_EnterGame enterGamePacket = new C_EnterGame() { Player = myPlayer };
            Send(enterGamePacket);

            //Thread.Sleep(5000);

            //C_LeaveGame leaveGamePacket = new C_LeaveGame()
            //{
            //    PlayerId = DummyId,
            //};

            //Send(leaveGamePacket);
        }

        public override void OnDisconnected(EndPoint endPoint)
        {
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
