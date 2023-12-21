using Google.Protobuf.Protocol;
using UnityEngine;

public class CreatureController : MonoBehaviour
{
    public int Id { get; set; }

    public string Name { get; set; }

    Vec3 _positionInfo = new Vec3();
    public Vec3 PosInfo
    {
        get { return _positionInfo; } 
        set
        {
            if (_positionInfo.Equals(value))
                return;

            CellPos = new Vector3(value.X, value.Y, value.Z);
        }
    }

    public Vector3 CellPos
    {
        get
        {
            return new Vector3(PosInfo.X, PosInfo.Y, PosInfo.Z);
        }

        set
        {
            if (PosInfo.X == value.x && PosInfo.Y == value.y)
                return;

            PosInfo.X = value.x;
            PosInfo.Y = value.y;
            PosInfo.Z = value.z;
        }
    }

    private CreatureState _state = new CreatureState();

    public virtual CreatureState State
    {
        get { return _state; }
        set
        {
            if (_state == value)
                return;

            _state = value;
        }
    }

    private void Awake()
    {
        _state = CreatureState.Idle;
    }
}
