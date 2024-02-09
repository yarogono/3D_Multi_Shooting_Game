using Assets.Scripts.UI.Scene;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Start : UI_Scene

{
    enum Buttons
    {
        LoginButton,
    }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.LoginButton).gameObject.AddUIEvent(OnLoginButtonClicked);
    }


    public void OnLoginButtonClicked(PointerEventData data)
    {
        //SceneManagerEx.Instance.LoadScene(Define.Scene.Game);

        WebManager.Instance.SendGetRequest<GoogleLoginRes>("api/oauth/google-login", null, res =>
        {
            Debug.Log(res);
        });
    }
}
