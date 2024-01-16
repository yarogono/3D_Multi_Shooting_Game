using Google.Protobuf.Protocol;
using Google.Protobuf;
using ServerCore;

partial class PacketHandler
{
    public static void S_SpawnHandler(PacketSession session, IMessage packet)
    {
        S_Spawn spawnPacket = (S_Spawn)packet;

        foreach (ObjectInfo obj in spawnPacket.Objects)
            ObjectManager.Instance.Add(obj, isMyPlayer: false);
    }

    public static void S_DespawnHandler(PacketSession session, IMessage packet)
    {
        S_Despawn despawnPacket = (S_Despawn)packet;

        foreach (int playerId in despawnPacket.ObjectIds)
            ObjectManager.Instance.Remove(playerId);
    }
}
