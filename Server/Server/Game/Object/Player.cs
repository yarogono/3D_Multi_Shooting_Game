using Google.Protobuf.Protocol;
using Server.Session;

namespace Server.Game.Object
{
    public class Player : GameObject
    {
        public ClientSession Session { get; set; }
        public StatInfo Stat { get; private set; } = new StatInfo();

        public Player()
        {
            ObjectType = GameObjectType.Player;
            Info.StatInfo = Stat;
        }

        public virtual void HpDamage()
        {

        }

        public virtual void OnDead(GameObject attacker)
        {

        }
    }
}
