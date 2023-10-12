public class Define
{
    public enum State
    {
        Idle,
        Moving,
    }

    public enum MoveDir
    {
        Up,
        Down,
        Left,
        Right,
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
}
