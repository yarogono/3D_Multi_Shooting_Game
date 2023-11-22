public class GameManager : CustomSingleton<GameManager>
{

    public void Clear()
    {
        SoundManager.Instance.Clear();
    }
}
