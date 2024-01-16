using Google.Protobuf.Protocol;

namespace Server.Game.Object
{
    public class Item : GameObject
    {
        private string _name;
        private int _damage;
        private int _ownerId;
        private int _itemNumber;


        public Item()
        {
            ObjectType = GameObjectType.Item;
        }

        public string Name 
        { 
            get => _name; 
            set => _name = value; 
        }

        public int Damage 
        { 
            get => _damage; 
            set => _damage = value; 
        }

        public int OwnerId 
        { 
            get => _ownerId; 
            set => _ownerId = value; 
        }

        public int ItemNumber
        {
            get => _itemNumber;
            set => _itemNumber = value;
        }
    }
}
