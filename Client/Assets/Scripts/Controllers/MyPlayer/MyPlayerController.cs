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

    private PositionInfo _positionInfo = new PositionInfo();
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

    public CreatureState State
    {
        get { return PosInfo.State; }
        set
        {
            if (PosInfo.State == value)
                return;

            PosInfo.State = value;
        }
    }

    private void Awake()
    {
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
                PosX = transform.position.x,
                PosY = transform.position.y,
                PosZ = transform.position.z,
                State = CreatureState.Moving
            }
        };
        NetworkManager.Instance.Send(movePacket);
    }
}
