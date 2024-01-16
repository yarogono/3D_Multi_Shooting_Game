using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;
using UnityEngine;

partial class PacketHandler
{
    public static void S_EnterGameHandler(PacketSession session, IMessage packet)
    {
        S_EnterGame enterGamePacket = (S_EnterGame)packet;

        ObjectManager.Instance.Add(enterGamePacket.Player, isMyPlayer: true);
    }

    public static void S_LeaveGameHandler(PacketSession session, IMessage packet)
    {
        S_LeaveGame leaveGamePacket = (S_LeaveGame)packet;

        int myPlayerId = ObjectManager.Instance.MyPlayer.Id;
        if (myPlayerId != leaveGamePacket.PlayerId)
            return;

        ObjectManager.Instance.Clear();
    }


    public static void S_MoveHandler(PacketSession session, IMessage packet)
    {
        S_Move movePacket = (S_Move)packet;

        GameObject gameObject = ObjectManager.Instance.FindById(movePacket.ObjectId);

        if (gameObject == null)
            return;

        if (ObjectManager.Instance.MyPlayer.Id == movePacket.ObjectId)
            return;

        PlayerSyncTransform enemyPlayerSyncTransform = gameObject.GetComponent<PlayerSyncTransform>();
        if (enemyPlayerSyncTransform == null)
            return;

        enemyPlayerSyncTransform.OnSync(movePacket);
    }
}
