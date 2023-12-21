using Google.Protobuf.Protocol;
using UnityEngine;

public class MyPlayerController : MonoBehaviour
{
    [SerializeField]
    private float _runSpeed = 15.0f;

    [SerializeField]
    private float _rotationSpeed = 15.0f;

    [SerializeField]
    private float _walkSpeed = 5.0f;

    private PlayerInputController _controller;
    private Animator _anim;

    private bool _isWalk;
    private Vector3 _movementDirection = Vector3.zero;

    public int Id { get; set; }

    public string Name { get; set; }

    private Vec3 _positionInfo = new Vec3();
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

    public CreatureState State
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
        _controller = GetComponent<PlayerInputController>();
        _anim = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        _controller.OnMoveEvent += Move;
    }

    private void Update()
    {
        if (_movementDirection == Vector3.zero)
        {
            _anim.SetBool("isRun", false);
            State = CreatureState.Idle;
        }
        else
        {
            State = CreatureState.Moving;
        }

        switch (State)
        {
            case CreatureState.Idle:
                break;
            case CreatureState.Moving:
                UpdateMoving(_movementDirection);
                break;
        }
    }

    private void Move(Vector3 direction)
    {
        _movementDirection = direction;
    }

    private void UpdateMoving(Vector3 direction)
    {
        direction = MultiplyMyPlayerMoveSpeed(direction);

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * _rotationSpeed);
        
        if (IsWallCheck() == true)
            return;

        transform.position += direction * Time.deltaTime;

        SendMovePacket();
    }

    private Vector3 MultiplyMyPlayerMoveSpeed(Vector3 direction)
    {
        _isWalk = Input.GetButton("Walk");
        _anim.SetBool("isWalk", _isWalk);
        if (_isWalk)
        {
            direction = direction * _walkSpeed;
        }
        else
        {
            _anim.SetBool("isRun", true);
            direction = direction * _runSpeed;
        }
        return direction;
    }

    private bool IsWallCheck()
    {
        bool isWall = Physics.Raycast(transform.position, transform.forward + Vector3.up, 3, LayerMask.GetMask("Wall", "Cube"));
        return isWall;
    }

    private void SendMovePacket()
    {
        C_Move movePacket = new C_Move()
        {
            PosInfo = new()
            {
                X = transform.position.x,
                Y = transform.position.y,
                Z = transform.position.z,
            },
        };
        NetworkManager.Instance.Send(movePacket);
    }
}
