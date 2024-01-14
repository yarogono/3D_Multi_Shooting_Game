using Google.Protobuf.Protocol;
using Server.Session;

namespace Server.Game.Object
{
    public class Player : GameObject
    {
        private ClientSession _session;
        private string _playerNickname;
        private int _hp;
        private List<Item> _items;
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

        public List<Item> Items 
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
        }


        public virtual void HpDamage()
        {

        }

        public virtual void OnDead(GameObject attacker)
        {

        }
    }
}
