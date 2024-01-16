using Google.Protobuf;
using Google.Protobuf.Protocol;
using Server.Game.Object;
using Server.Game.Room;
using Server.Session;
using ServerCore;

partial class PacketHandler
{
    public static void C_SwapWeaponItemHandler(PacketSession session, IMessage packet)
    {
        ClientSession clientSession = (ClientSession)session;
        C_SwapWeaponItem swapWeaponItemPacket = (C_SwapWeaponItem)packet;

        Player player = clientSession.MyPlayer;
        if (player == null)
            return;

        GameRoom room = player.Room;
        if (room == null)
            return;
        
        room.Push(player.SwapWeaponItem, swapWeaponItemPacket);
    }

    public static void C_LootItemHandler(PacketSession session, IMessage packet)
    {
        ClientSession clientSession = (ClientSession)session;
        C_LootItem lootItemPacket = (C_LootItem)packet;

        Player player = clientSession.MyPlayer;
        if (player == null)
            return;

        GameRoom room = player.Room;
        if (room == null)
            return;

        room.Push(player.LootItem, lootItemPacket);
    }
}