using Google.Protobuf.Protocol;
using Server.Session;

namespace Server.Game.Object
{
    public class Player : GameObject
    {
        public ClientSession Session { get; set; }
        public StatInfo Stat { get; private set; } = new StatInfo();

        public int Hp
        {
            get { return Stat.Hp; }
            set { Stat.Hp = Math.Clamp(value, 0, Stat.MaxHp); }
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
