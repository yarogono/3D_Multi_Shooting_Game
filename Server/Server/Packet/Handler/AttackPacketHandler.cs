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

        if (meleeAttackPacket == null)
            return;

        Player player = clientSession.MyPlayer;
        if (player == null)
            return;

        GameRoom room = player.Room;
        if (room == null)
            return;

        room.Push(player.MeleeAttack, meleeAttackPacket);
    }

    public static void C_DamageMeleeHandler(PacketSession session, IMessage packet)
    {
        ClientSession clientSession = (ClientSession)session;
        C_DamageMelee damageMeleePacket = (C_DamageMelee)packet;

        if (damageMeleePacket == null)
            return;

        Player player = clientSession.MyPlayer;
        if (player == null)
            return;

        GameRoom room = player.Room;
        if (room == null)
            return;

        Player targetPlayer = ObjectManager.Instance.Find(damageMeleePacket.TargetPlayerId);
        if (targetPlayer == null)
            return;

        room.Push(targetPlayer.DamageMelee, damageMeleePacket, targetPlayer.PosInfo);
    }

    public static void C_DamageBulletHandler(PacketSession session, IMessage packet)
    {
        ClientSession clientSession = (ClientSession)session;
        C_DamageBullet damageBulletPacket = (C_DamageBullet)packet;

        if (damageBulletPacket == null)
            return;

        Player player = clientSession.MyPlayer;
        if (player == null)
            return;

        GameRoom room = player.Room;
        if (room == null)
            return;

        Player targetPlayer = ObjectManager.Instance.Find(damageBulletPacket.TargetPlayerId);
        if (targetPlayer == null)
            return;

        room.Push(targetPlayer.DamageBullet, damageBulletPacket, targetPlayer.PosInfo);
    }

    public static void C_GunAttackHandler(PacketSession session, IMessage packet)
    {
        ClientSession clientSession = (ClientSession)session;
        C_GunAttack gunAttackPacket = (C_GunAttack)packet;

        if (gunAttackPacket == null)
            return;

        Player player = clientSession.MyPlayer;
        if (player == null)
            return;

        GameRoom room = player.Room;
        if (room == null)
            return;

        room.Push(player.GunAttack, gunAttackPacket);
    }
}