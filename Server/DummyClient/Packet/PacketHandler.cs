using DummyClient;
using ServerCore;

class PacketHandler
{
	public static void S_PlayerInfoHandler(PacketSession session, IPacket packet)
	{
        //S_Chat chatPacket = packet as S_Chat;
        //ServerSession serverSession = session as ServerSession;

        //Console.WriteLine(chatPacket.chat);
        
    }

    public static void S_PlayerLoginHandler(PacketSession session, IPacket packet)
    {

    }

    public static void S_SavePlayerHandler(PacketSession session, IPacket packet)
    {

    }
}
