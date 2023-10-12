using UnityEngine;

public class StartScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Start;
    }

    public void LoginButtonClicked()
    {
        UIManager.Instance.ShowPopupUI<UI_Popup>("LoginUI");
    }

    public void SignUpButtonClicked()
    {
        UIManager.Instance.ShowPopupUI<UI_Popup>("SignUpUI");
    }

    public override void Clear()
    {

    }
}
