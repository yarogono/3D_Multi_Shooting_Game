using ServerCore;
using UnityEngine;

class PacketHandler
{
    public static void S_PlayerInfoHandler(PacketSession session, IPacket packet)
    {
        
    }


    public static void S_PlayerLoginHandler(PacketSession session, IPacket packet)
    {

    }

    public static void S_SavePlayerHandler(PacketSession session, IPacket packet)
    {
        S_SavePlayer savePlayerPacket = packet as S_SavePlayer;

        Debug.Log(savePlayerPacket);
    }
}
