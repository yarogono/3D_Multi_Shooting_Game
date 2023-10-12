using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SignUp : UI_Popup
{
    void Start()
    {
        
    }

    enum Buttons
    {
        ExitButton,
    }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));


        GetButton((int)Buttons.ExitButton).gameObject.AddUIEvent(OnExitButtonClicked);
    }

    public void OnExitButtonClicked(PointerEventData data)
    {
        ClosePopupUI();
    }
}
