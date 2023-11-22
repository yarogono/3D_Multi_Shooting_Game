using Assets.Scripts.UI.Scene;
using Google.Protobuf.Protocol;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Start : UI_Scene

{
    enum Buttons
    {
        ExitButton,
        LoginButton,
    }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.LoginButton).gameObject.AddUIEvent(OnLoginButtonClicked);
    }


    public void OnLoginButtonClicked(PointerEventData data)
    {
        Debug.Log("Click!!");

        PositionInfo posInfo =new PositionInfo() { PosX = 0, PosY = 1, PosZ = 0 };
        ObjectInfo myPlayer = new ObjectInfo() { Name = "MyPlayer", State = 1, PosInfo = posInfo };
        C_EnterGame enterGamePacket = new C_EnterGame() { Player = myPlayer };
        NetworkManager.Instance.Send(enterGamePacket);
    }
}
