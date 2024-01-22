using Assets.Scripts.UI.Scene;
using UnityEngine;

public class StartScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Start;
    }

    public override void Clear()
    {

    }
}
