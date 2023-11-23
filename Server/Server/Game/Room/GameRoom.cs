using Google.Protobuf;
using Google.Protobuf.Protocol;
using Server.Game.Job;
using Server.Game.Object;

namespace Server.Game.Room
{
    public class GameRoom : JobSerializer
    {
        public int RoomId { get; set; }

        Dictionary<int, Player> _players = new Dictionary<int, Player>();

        public void Init()
        {

        }

        // 누군가 주기적으로 호출해줘야 한다
        public void Update()
        {
            Flush();
        }

        public void EnterGame(GameObject gameObject)
        {
            if (gameObject == null)
                return;

            GameObjectType type = ObjectManager.GetObjectTypeById(gameObject.Id);

            if (type == GameObjectType.Player)
            {
                Player player = gameObject as Player;
                _players.Add(gameObject.Id, player);

                player.Room = this;

                // 본인한테 정보 전송
                {
                    S_EnterGame enterPacket = new S_EnterGame();
                    enterPacket.Player = player.Info;
                    player.Session.Send(enterPacket);

                    S_Spawn spawnPacket = new S_Spawn();
                    foreach (Player p in _players.Values)
                    {
                        if (player != p)
                            spawnPacket.Objects.Add(p.Info);
                    }

                    player.Session.Send(spawnPacket);
                }
            }

            // 타인한테 정보 전송
            {
                S_Spawn spawnPacket = new S_Spawn();
                spawnPacket.Objects.Add(gameObject.Info);
                Broadcast(spawnPacket, gameObject.Id);
            }
        }

        public void LeaveGame(int objectId)
        {
            GameObjectType type = ObjectManager.GetObjectTypeById(objectId);

            if (type == GameObjectType.Player)
            {
                Player player = null;
                if (_players.Remove(objectId, out player) == false)
                    return;

                player.Room = null;

                // 본인한테 정보 전송
                {
                    S_LeaveGame leavePacket = new S_LeaveGame();
                    leavePacket.PlayerId = player.Id;
                    player.Session.Send(leavePacket);
                }
            }

            // 타인한테 정보 전송
            {
                S_Despawn despawnPacket = new S_Despawn();
                despawnPacket.ObjectIds.Add(objectId);
                Broadcast(despawnPacket, objectId);
            }
        }

        public void HandleMove(Player player, C_Move movePacket)
        {
            if (player == null)
                return;

            ObjectInfo info = player.Info;

            PositionInfo posInfo = movePacket.PosInfo;
            info.PosInfo = posInfo;

            // 다른 플레이어한테도 알려준다
            S_Move resMovePacket = new S_Move();
            resMovePacket.ObjectId = player.Id;
            resMovePacket.PosInfo = new PositionInfo(posInfo);

            Broadcast(resMovePacket, player.Id);
        }

        public Player FindPlayer(Func<GameObject, bool> condition)
        {
            foreach (Player player in _players.Values)
            {
                if (condition.Invoke(player))
                    return player;
            }

            return null;
        }


        public void Broadcast(IMessage packet)
        {
            foreach (Player p in _players.Values)
            {
                p.Session.Send(packet);
            }
        }

        public void Broadcast(IMessage packet, int exceptPlayerId)
        {
            foreach (Player p in _players.Values)
            {
                if (p.Id != exceptPlayerId)
                    p.Session.Send(packet);
            }
        }
    }
}
