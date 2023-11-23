using Google.Protobuf;
using Google.Protobuf.Protocol;
using Server.Game.Object;
using Server.Game.Room;
using Server.Session;
using ServerCore;

class PacketHandler
{
    public static void C_EnterGameHandler(PacketSession session, IMessage packet)
    {
        C_EnterGame enterGamePacket = (C_EnterGame)packet;
        ClientSession clientSession = (ClientSession)session;


        Player sessionPlayer = clientSession.MyPlayer;


        var packetPlayer = enterGamePacket.Player;

        Console.WriteLine($"Enter User : {packetPlayer.Name} plyer");
        Player player = ObjectManager.Instance.Add<Player>();
        {
            player.Info.Name = enterGamePacket.Player.Name;
            player.Info.PosInfo = packetPlayer.PosInfo;

            StatInfo stat = enterGamePacket.Player.StatInfo;
            player.Stat.MergeFrom(stat);

            player.Session = clientSession;
        }

        clientSession.MyPlayer = player;

        GameLogic.Instance.Push(() =>
        {
            GameRoom room = GameLogic.Instance.Find(1);
            room.Push(room.EnterGame, player);
        });
    }

    public static void C_LeaveGameHandler(PacketSession session, IMessage packet)
    {

    }

    public static void C_MoveHandler(PacketSession session, IMessage packet)
    {
        C_Move movePacket = (C_Move)packet;
        ClientSession clientSession = (ClientSession)session;

        Player player = clientSession.MyPlayer;
        if (player == null)
            return;

        GameRoom room = player.Room;
        if (room == null)
            return;

        room.Push(room.HandleMove, player, movePacket);
    }
}
