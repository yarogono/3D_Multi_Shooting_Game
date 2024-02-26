using DummyClient;
using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;

partial class PacketHandler
{
    public static void S_EnterGameHandler(PacketSession session, IMessage packet)
    {
        S_EnterGame enterGamePacket = (S_EnterGame)packet;
        ServerSession serverSession = (ServerSession)session;

        int playerId = enterGamePacket.Player.ObjectId;
        serverSession.DummyId = playerId;
    }

    public static void S_SpawnHandler(PacketSession session, IMessage packet)
    {

    }

    public static void S_DespawnHandler(PacketSession session, IMessage packet)
    {

    }

    public static void S_LeaveGameHandler(PacketSession session, IMessage packet)
    {

    }

    public static void S_MoveHandler(PacketSession session, IMessage packet)
    {

    }
}
