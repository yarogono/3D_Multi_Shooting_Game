using DummyClient;
using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;

partial class PacketHandler
{
    public static void S_PingHandler(PacketSession session, IMessage packet)
    {
        S_Ping pingPacket = (S_Ping)packet;
        ServerSession serverSession = (ServerSession)session;
        serverSession.Send(pingPacket);
    }
}