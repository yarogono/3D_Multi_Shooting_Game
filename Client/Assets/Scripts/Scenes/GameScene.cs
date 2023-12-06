using Google.Protobuf.Protocol;
using Unity.VisualScripting;
using UnityEngine;

public class GameScene : BaseScene
{
    
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Game;

        PositionInfo posInfo = new PositionInfo() { PosX = 0, PosY = 1, PosZ = 0, State = CreatureState.Idle };
        ObjectInfo myPlayer = new ObjectInfo() { Name = "MyPlayer", PosInfo = posInfo };
        C_EnterGame enterGamePacket = new C_EnterGame() { Player = myPlayer };
        NetworkManager.Instance.Send(enterGamePacket);
    }

    public override void Clear()
    {

    }
}
