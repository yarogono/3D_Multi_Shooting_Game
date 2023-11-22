using Assets.Scripts.UI.Scene;
using UnityEngine;

public class StartScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Start;
        UIManager.Instance.ShowSceneUI<UI_Scene>("StartCanvas");
    }

    public override void Clear()
    {

    }
}
