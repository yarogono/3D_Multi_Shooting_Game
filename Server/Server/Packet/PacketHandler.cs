using Server;
using Server.Session;
using ServerCore;

class PacketHandler
{
    public static void C_PlayerLoginHandler(PacketSession session, IPacket packet)
    {
        C_PlayerLogin playerLoginPacket = packet as C_PlayerLogin;
        ClientSession clientSession = session as ClientSession;

        if (clientSession.Room == null)
            return;

        GameRoom room = clientSession.Room;
        room.Push(
            () => room.PlayerLogin(playerLoginPacket)
        );
    }

    public static void C_SavePlayerHandler(PacketSession session, IPacket packet)
    {
        C_SavePlayer playerLoginPacket = packet as C_SavePlayer;
        ClientSession clientSession = session as ClientSession;

        if (clientSession.Room == null)
            return;

        //GameRoom room = clientSession.Room;
        //room.Push(
        //    () => room.SavePlayer(playerLoginPacket)
        //);
    }
}
