using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SignUp : UI_Popup
{

    enum Buttons
    {
        ExitButton,
        SignUpButton,
    }

    enum InputFields
    {
        IDinput,
        PasswordInput,
        PasswordInputTwo,
    }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));
        Bind<TMP_InputField>(typeof(InputFields));

        GetButton((int)Buttons.ExitButton).gameObject.AddUIEvent(OnExitButtonClicked);
        GetButton((int)Buttons.SignUpButton).gameObject.AddUIEvent(OnSignUpButtonClicked);
    }

    public void OnSignUpButtonClicked(PointerEventData data)
    {
        string id = GetTMPInputField((int)InputFields.IDinput).text;
        string password = GetTMPInputField((int)InputFields.PasswordInput).text;
        string passwordTwo = GetTMPInputField((int)InputFields.PasswordInputTwo).text;

        if (id == null || password == null || passwordTwo == null)
            return;

        if (password.Equals(passwordTwo) == false)
        {
            // ToDo : 비밀번호 불일치 팝업 창 띄우기
            Debug.Log("비밀번호를 다시 입력해주세요.");
            return;
        }

        C_SavePlayer savePlayerPacket = new C_SavePlayer() { username = id, password = password };
        NetworkManager.Instance.Send(savePlayerPacket.Write());
        Debug.Log("아이디가 생성되었습니다.");

        ClosePopupUI();
    }


    public void OnExitButtonClicked(PointerEventData data)
    {
        ClosePopupUI();
    }
}
