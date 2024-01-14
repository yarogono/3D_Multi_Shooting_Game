using Google.Protobuf.Protocol;
using Server.Game.Room;

namespace Server.Game.Object
{
    public class GameObject
    {
        private GameRoom _room;
        private ObjectInfo _objectInfo;
        private GameObjectType _objectType;
        private Vec3 _posInfo;

        public GameObjectType ObjectType 
        { 
            get => _objectType; 
            protected set => _objectType = value; 
        }

        public int Id
        {
            get => Info.ObjectId;
            set => Info.ObjectId = value;
        }

        public GameRoom Room 
        { 
            get => _room;
            set => _room = value; 
        }

        public ObjectInfo Info 
        { 
            get => _objectInfo; 
            set => _objectInfo = value; 
        }

        public Vec3 PosInfo 
        { 
            get => _posInfo; 
            private set => _posInfo = value; 
        }


        public GameObject()
        {
            _objectType = GameObjectType.None;
            _objectInfo = new ObjectInfo();
            _posInfo = new Vec3();
            Info.PosInfo = PosInfo;
        }
    }
}
