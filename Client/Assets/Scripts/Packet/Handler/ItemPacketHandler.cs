using Google.Protobuf.Protocol;
using Google.Protobuf;
using ServerCore;
using UnityEngine;

partial class PacketHandler
{
    public static void S_SpawnItemHandler(PacketSession session, IMessage packet)
    {
        S_SpawnItem itemsSpawnPacket = (S_SpawnItem)packet;

        if (itemsSpawnPacket == null)
            return;

        foreach (ObjectInfo obj in itemsSpawnPacket.Objects)
            ObjectManager.Instance.Add(obj);
    }

    public static void S_SwapWeaponItemHandler(PacketSession session, IMessage packet)
    {
        S_SwapWeaponItem swapWeaponItemPacket = (S_SwapWeaponItem)packet;

        if (swapWeaponItemPacket == null)
            return;

        GameObject player = ObjectManager.Instance.FindById(swapWeaponItemPacket.PlayerId);

        if (player == null)
            return;

        PlayerSyncItem playerSyncItem = player.GetComponent<PlayerSyncItem>();

        if (playerSyncItem == null)
            return;

        playerSyncItem.OnSync(swapWeaponItemPacket);
    }

    public static void S_LootItemHandler(PacketSession session, IMessage packet)
    {
        S_LootItem lootItemPacket = (S_LootItem)packet;

        if (lootItemPacket == null)
            return;

        GameObject player = ObjectManager.Instance.FindById(lootItemPacket.PlayerId);

        if (player == null)
            return;

        PlayerSyncItem playerSyncItem = player.GetComponent<PlayerSyncItem>();

        if (playerSyncItem == null)
            return;

        playerSyncItem.OnSync(lootItemPacket);
    }
}
