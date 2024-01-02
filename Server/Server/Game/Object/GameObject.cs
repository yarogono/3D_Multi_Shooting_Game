using Google.Protobuf.Protocol;
using Server.Game.Room;

namespace Server.Game.Object
{
    public class GameObject
    {
        public GameObjectType ObjectType { get; protected set; } = GameObjectType.None;
        public int Id
        {
            get { return Info.ObjectId; }
            set { Info.ObjectId = value; }
        }

        public GameRoom Room { get; set; }

        public ObjectInfo Info { get; set; } = new ObjectInfo();

        public Vec3 PosInfo { get; private set; } = new Vec3();


        public GameObject()
        {
            Info.PosInfo = PosInfo;
        }
    }
}
