using Google.Protobuf.Protocol;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Game;

        PositionInfo posInfo = new PositionInfo() { PosX = 0, PosY = 1, PosZ = 0, State = CreatureState.Idle };
        StatInfo statInfo = new StatInfo() { Attack = 1, Hp = 100, Level = 1, MaxHp = 100, Speed = 15 };
        ObjectInfo myPlayer = new ObjectInfo() { Name = "MyPlayer", PosInfo = posInfo, StatInfo = statInfo };
        C_EnterGame enterGamePacket = new C_EnterGame() { Player = myPlayer };
        NetworkManager.Instance.Send(enterGamePacket);
    }

    public override void Clear()
    {

    }
}
