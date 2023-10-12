using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Login : UI_Popup
{
    private void Start()
    {
        Init();
    }

    enum Buttons
    {
        ExitButton,
        LoginButton,
    }

    enum InputFields
    {
        IDinput,
        PasswordInput,
    }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));
        Bind<TMP_InputField>(typeof(InputFields));


        GetButton((int)Buttons.ExitButton).gameObject.AddUIEvent(OnExitButtonClicked);
        GetButton((int)Buttons.LoginButton).gameObject.AddUIEvent(OnLoginButtonClicked);
    }

    public void OnLoginButtonClicked(PointerEventData data)
    {
        string id = GetTMPInputField((int)InputFields.IDinput).text;
        string password = GetTMPInputField((int)InputFields.PasswordInput).text;

        if (id == null || password == null)
            return;

        if (id.Length == 0 || password.Length == 0)
            return;

        C_PlayerLogin playerLoginPacket = new C_PlayerLogin() { username = id, password = password };
        NetworkManager.Instance.Send(playerLoginPacket.Write());
    }

    public void OnExitButtonClicked(PointerEventData data)
    {
        ClosePopupUI();
    }
}
