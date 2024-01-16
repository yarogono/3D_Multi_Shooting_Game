using Google.Protobuf.Protocol;
using Server.Data;
using Server.Session;

namespace Server.Game.Object
{
    public class Player : GameObject
    {
        private ClientSession _session;
        private string _playerNickname;
        private int _hp;
        private Dictionary<int, Item> _items;
        private Item _equipWeaponItem;

        public ClientSession Session 
        { 
            get => _session; 
            set => _session = value; 
        }

        public string PlayerNickname
        {
            get => _playerNickname;
            set => _playerNickname = value;
        }

        public int Hp
        {
            get => _hp;
            set => _hp = Math.Clamp(value, 0, _hp); 
        }

        public Dictionary<int, Item> Items 
        { 
            get => _items;
        }

        public Item EquipWeaponItem
        {
            get => _equipWeaponItem;
            set => _equipWeaponItem = value;
        }

        public Player()
        {
            ObjectType = GameObjectType.Player;
            _items = new Dictionary<int, Item>();
        }

        public void LeaveGame(int objectId)
        {
            GameObjectType type = ObjectManager.GetObjectTypeById(objectId);

            if (type == GameObjectType.Player)
            {
                Player player = Room.RemovePlayer(objectId);

                if (player == null)
                    return;

                // 본인한테 정보 전송
                {
                    S_LeaveGame leavePacket = new S_LeaveGame();
                    leavePacket.PlayerId = player.Id;
                    player.Session.Send(leavePacket);
                }

                // 타인한테 정보 전송
                {
                    S_Despawn despawnPacket = new S_Despawn();
                    despawnPacket.ObjectIds.Add(objectId);
                    Room.Broadcast(despawnPacket, objectId);
                }

                player.Room = null;
            }
        }

        public void HandleMove(C_Move movePacket)
        {
            ObjectInfo info = this.Info;

            Vec3 posInfo = movePacket.PosInfo;
            info.PosInfo = posInfo;

            // 다른 플레이어한테도 알려준다
            S_Move resMovePacket = new S_Move();
            resMovePacket.ObjectId = this.Id;
            resMovePacket.PosInfo = new Vec3(posInfo);
            resMovePacket.MoveSpeed = movePacket.MoveSpeed;
            DateTimeOffset pingTime = DateTimeOffset.UtcNow;
            resMovePacket.ServerTimestamp = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTimeOffset(pingTime);

            Room.Broadcast(resMovePacket, this.Id);
        }

        public void SwapWeaponItem(C_SwapWeaponItem weaponItem)
        {
            if (_items.Count == 0)
                return;

            Item playerItem = null;
            if (_items.TryGetValue(weaponItem.ItemId, out playerItem) == false)
                return;

            S_SwapWeaponItem swapWeaponItemPacket = new S_SwapWeaponItem()
            {
                PlayerId = this.Id,
                WeaponItemNumber = weaponItem.WeaponItemNumber,
            };

            EquipWeaponItem = playerItem;

            Room.Broadcast(swapWeaponItemPacket, this.Id);
        }

        internal void LootItem(C_LootItem lootItem)
        {
            S_LootItem lootItemPacket = new S_LootItem()
            {
                PlayerId = this.Id,
                ItemId = lootItem.ItemId,
                WeaponItemNumber = lootItem.WeaponItemNumber,
            };

            WeaponItem weaponItem = null;
            if (DataManager.ItemDict.TryGetValue(lootItem.WeaponItemNumber, out weaponItem) == false)
                return;

            Item playerItem = new Item()
            {
                Id = lootItem.ItemId,
                Damage = weaponItem.damage,
                Name = weaponItem.name,
                ItemNumber = weaponItem.id,
            };

            _items.Add(lootItem.WeaponItemNumber, playerItem);

            Room.RemoveItem(lootItem.ItemId);

            Room.Broadcast(lootItemPacket, this.Id);
        }
    }
}
