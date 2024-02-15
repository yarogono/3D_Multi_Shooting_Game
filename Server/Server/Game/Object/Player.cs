using Google.Protobuf.Protocol;
using Server.Data;
using Server.Session;
using Server.Utils;
using System.Numerics;

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
            this.Hp = 100;
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
            if (_items.TryGetValue(weaponItem.WeaponItemNumber, out playerItem) == false)
                return;

            S_SwapWeaponItem swapWeaponItemPacket = new S_SwapWeaponItem()
            {
                PlayerId = this.Id,
                WeaponItemNumber = weaponItem.WeaponItemNumber,
            };

            EquipWeaponItem = playerItem;

            Room.Broadcast(swapWeaponItemPacket);
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
                Damage = weaponItem.damage,
                Name = weaponItem.name,
                ItemNumber = weaponItem.id,
            };

            _items.Add(lootItem.WeaponItemNumber, playerItem);

            Room.RemoveItem(lootItem.ItemId);

            Room.Broadcast(lootItemPacket, this.Id);
        }

        public void MeleeAttack(C_MeleeAttack reqMeleeAttackPacket)
        {
            S_MeleeAttack resMeleeAttackPacket = new S_MeleeAttack()
            {
                AttackPlayerId = reqMeleeAttackPacket.AttackPlayerId,
            };

            Room.Broadcast(resMeleeAttackPacket, this.Id);
        }

        public void GunAttack(C_GunAttack reqGunAttack)
        {
            S_GunAttack resGunAttackPacket = new S_GunAttack()
            {
                AttackPlayerId = reqGunAttack.AttackPlayerId,
            };

            Room.Broadcast(resGunAttackPacket, this.Id);
        }

        internal void DamageMelee(C_DamageMelee reqDamageMelee, Vec3 attackerPosInfo)
        {
            float targetDistanceMatch = MathUtils.Vector3Distance(reqDamageMelee.TargetPosInfo, this.PosInfo);
            if (targetDistanceMatch > 5f)
                return;

            float attackDistance = MathUtils.Vector3Distance(this.PosInfo, attackerPosInfo);
            var meleeItemInfo = DataManager.ItemDict[reqDamageMelee.MeleeItemNumber];
            if (attackDistance > meleeItemInfo.attackRange)
                return;

            int getDamagedHp = this.Hp - reqDamageMelee.Damage;
            this.Hp = getDamagedHp;

            S_DamageMelee resDamageMelee = new S_DamageMelee()
            {
                TargetPlayerId = reqDamageMelee.TargetPlayerId,
                Damage = reqDamageMelee.Damage,
            };

            Room.Broadcast(resDamageMelee);
        }

        public void EnterGame(Dictionary<int, Player> _roomPlayers, Dictionary<int, Item> _roomItems)
        {
            S_EnterGame enterPacket = new S_EnterGame();
            enterPacket.Player = this.Info;
            this.Session.Send(enterPacket);

            S_Spawn enemyPlayersSpawnPacket = new S_Spawn();
            foreach (Player p in _roomPlayers.Values)
            {
                if (this != p)
                    enemyPlayersSpawnPacket.Objects.Add(p.Info);
            }

            this.Session.Send(enemyPlayersSpawnPacket);

            S_SpawnItem itemsSpawnPacket = new S_SpawnItem();
            foreach (Item item in _roomItems.Values)
                itemsSpawnPacket.Objects.Add(item.Info);

            this.Session.Send(itemsSpawnPacket);
        }

        public void DamageBullet(C_DamageBullet reqDamageBullet, Vec3 attackerPosInfo)
        {
            float targetDistanceMatch = MathUtils.Vector3Distance(reqDamageBullet.TargetPosInfo, this.PosInfo);
            if (targetDistanceMatch > 5f)
                return;

            int getDamagedHp = this.Hp - reqDamageBullet.Damage;
            this.Hp = getDamagedHp;

            S_DamageBullet resDamageBullet = new S_DamageBullet()
            {
                TargetPlayerId = reqDamageBullet.TargetPlayerId,
                Damage = reqDamageBullet.Damage,
            };

            Room.Broadcast(resDamageBullet);
        }
    }
}
