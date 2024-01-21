using Google.Protobuf.Protocol;
using Google.Protobuf;
using Server.Game.Object;
using Server.Game.Room;
using Server.Session;
using ServerCore;

partial class PacketHandler
{
    public static void C_MeleeAttackHandler(PacketSession session, IMessage packet)
    {
        ClientSession clientSession = (ClientSession)session;
        C_MeleeAttack meleeAttackPacket = (C_MeleeAttack)packet;

        Player player = clientSession.MyPlayer;
        if (player == null)
            return;

        GameRoom room = player.Room;
        if (room == null)
            return;

        room.Push(player.MeleeAttack, meleeAttackPacket);
    }
}