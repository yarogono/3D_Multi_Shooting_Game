using Google.Protobuf;
using Server.Session;
using ServerCore;

partial class PacketHandler
{
    public static void C_PongHandler(PacketSession session, IMessage packet)
    {
        ClientSession clientSession = (ClientSession)session;
        clientSession.HandlePong();
    }
}