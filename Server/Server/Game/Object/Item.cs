using Google.Protobuf.Protocol;

namespace Server.Game.Object
{
    public class Item : GameObject
    {

        public Item()
        {
            ObjectType = GameObjectType.Item;
        }

        public string Name { get; set; }

        public int damage { get; set; }
    }
}
