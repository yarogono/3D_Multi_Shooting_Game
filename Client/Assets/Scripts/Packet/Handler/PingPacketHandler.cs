using Google.Protobuf.Protocol;
using Google.Protobuf;
using ServerCore;

partial class PacketHandler
{
    public static void S_PingHandler(PacketSession session, IMessage packet)
    {
        S_Ping pingPacket = (S_Ping)packet;
        NetworkManager.Instance.PingCheck(pingPacket);

        C_Pong pongPacket = new C_Pong();
        NetworkManager.Instance.Send(pongPacket);
    }
}
