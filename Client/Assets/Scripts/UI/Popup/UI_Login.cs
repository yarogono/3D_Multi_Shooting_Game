using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
