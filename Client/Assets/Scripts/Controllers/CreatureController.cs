using Google.Protobuf.Protocol;
using UnityEngine;

public class CreatureController : MonoBehaviour
{
    public int Id { get; set; }

    public string Name { get; set; }

    PositionInfo _positionInfo = new PositionInfo();
    public PositionInfo PosInfo
    {
        get { return _positionInfo; } 
        set
        {
            if (_positionInfo.Equals(value))
                return;

            CellPos = new Vector3(value.PosX, value.PosY, value.PosZ);
            State = value.State;
        }
    }

    public Vector3 CellPos
    {
        get
        {
            return new Vector3(PosInfo.PosX, PosInfo.PosY, PosInfo.PosZ);
        }

        set
        {
            if (PosInfo.PosX == value.x && PosInfo.PosY == value.y)
                return;

            PosInfo.PosX = value.x;
            PosInfo.PosY = value.y;
            PosInfo.PosZ = value.z;
        }
    }

    public virtual CreatureState State
    {
        get { return PosInfo.State; }
        set
        {
            if (PosInfo.State == value)
                return;

            PosInfo.State = value;
        }
    }
}
