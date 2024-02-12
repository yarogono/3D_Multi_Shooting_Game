using Google.Protobuf;
using Google.Protobuf.Protocol;
using Server.Data;
using Server.Game.Job;
using Server.Game.Object;

namespace Server.Game.Room
{
    public class GameRoom : JobSerializer
    {
        public int RoomId { get; set; }

        Dictionary<int, Player> _players = new Dictionary<int, Player>();
        Dictionary<int, Item> _items = new Dictionary<int, Item>();


        public void Init()
        {
            GameIRoomItemsSetting();
        }

        private void GameIRoomItemsSetting()
        {
            var itemDict = DataManager.ItemDict;
            foreach (WeaponItem weaponItem in itemDict.Values)
            {
                Item item = new Item();
                item.Damage = weaponItem.damage;
                item.Id = weaponItem.id;
                item.Room = this;
                Vec3 posInfo = new Vec3()
                {
                    X = weaponItem.posX,
                    Y = weaponItem.posY,
                    Z = weaponItem.posZ
                };

                item.Info = new ObjectInfo()
                {
                    ObjectId = ObjectManager.Instance.GenerateId(item.ObjectType),
                    Name = weaponItem.name,
                    PosInfo = posInfo
                };

                _items.Add(item.Id, item);
            }
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
                Player player = (Player)gameObject;
                _players.Add(gameObject.Id, player);

                player.Room = this;

                player.EnterGame(_players, _items);
            }

            // 타인한테 정보 전송
            {
                S_Spawn spawnPacket = new S_Spawn();
                spawnPacket.Objects.Add(gameObject.Info);
                Broadcast(spawnPacket, gameObject.Id);
            }
        }

        public Item RemoveItem(int objectId)
        {
            Item item = null;
            if (_items.Remove(objectId, out item) == false)
                return item;

            return item;
        }

        public Player RemovePlayer(int objectId)
        {
            Player player = null;
            if (_players.Remove(objectId, out player) == false)
                return player;

            return player;
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
