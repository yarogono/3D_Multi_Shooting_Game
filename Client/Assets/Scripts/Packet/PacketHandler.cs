using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;
using UnityEngine;

class PacketHandler
{
    public static void S_EnterGameHandler(PacketSession session, IMessage packet)
    {
        S_EnterGame enterGamePacket = packet as S_EnterGame;

        ObjectManager.Instance.Add(enterGamePacket.Player, isMyPlayer: true);
    }

    public static void S_LeaveGameHandler(PacketSession session, IMessage packet)
    {
        S_LeaveGame leaveGamePacket = packet as S_LeaveGame;

        int myPlayerId = ObjectManager.Instance.MyPlayer.Id;
        if (myPlayerId != leaveGamePacket.PlayerId)
            return;

        ObjectManager.Instance.Clear();
    }


    public static void S_SpawnHandler(PacketSession session, IMessage packet)
    {
        S_Spawn spawnPacket = packet as S_Spawn;

        foreach (ObjectInfo obj in spawnPacket.Objects)
            ObjectManager.Instance.Add(obj, isMyPlayer: false);
    }

    public static void S_DespawnHandler(PacketSession session, IMessage packet)
    {
        S_Despawn despawnPacket = packet as S_Despawn;

        foreach (int playerId in despawnPacket.ObjectIds)
            ObjectManager.Instance.Remove(playerId);
    }

    public static void S_MoveHandler(PacketSession session, IMessage packet)
    {
        S_Move movePacket = (S_Move)packet;

        GameObject gameObject = ObjectManager.Instance.FindById(movePacket.ObjectId);

        if (gameObject == null)
            return;

        if (ObjectManager.Instance.MyPlayer.Id == movePacket.ObjectId)
            return;

        
        if (!gameObject.TryGetComponent<PlayerSyncTransform>(out var enemyPlayer))
            return;

        enemyPlayer.PosInfo = movePacket.PosInfo;
    }

    public static void S_SpawnItemHandler(PacketSession session, IMessage packet)
    {
        S_Move movePacket = (S_Move)packet;

        GameObject gameObject = ObjectManager.Instance.FindById(movePacket.ObjectId);

        if (gameObject == null)
            return;

        if (ObjectManager.Instance.MyPlayer.Id == movePacket.ObjectId)
            return;


        if (!gameObject.TryGetComponent<PlayerSyncTransform>(out var enemyPlayer))
            return;

        enemyPlayer.PosInfo = movePacket.PosInfo;
    }
}
