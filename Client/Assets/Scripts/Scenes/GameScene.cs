using Google.Protobuf.Protocol;
using Unity.VisualScripting;
using UnityEngine;

public class GameScene : BaseScene
{
    
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Game;

        Vec3 posInfo = new Vec3() { X = 0, Y = 1, Z = 0 };
        CreatureState state = CreatureState.Idle;
        ObjectInfo myPlayer = new ObjectInfo() { Name = "MyPlayer", PosInfo = posInfo, State = state };
        C_EnterGame enterGamePacket = new C_EnterGame() { Player = myPlayer };
        NetworkManager.Instance.Send(enterGamePacket);
    }

    public override void Clear()
    {

    }
}
