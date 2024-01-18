public class Define
{
    public enum State
    {
        Idle,
        Moving,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }

    public enum UIEvent
    {
        Click,
    }

    public enum Scene
    {
        Unknown,
        Game,
        Start,
    }

    public enum ItemNumber
    {
        One,
        Two, 
        Three,
        None,
    }

    public enum DropItemType
    {
        Ammo, 
        Coin, 
        Grenade, 
        Heart, 
        Weapon,
    }

    public enum WeaponItemType
    {
        Melee,
        Range,
    }
}
