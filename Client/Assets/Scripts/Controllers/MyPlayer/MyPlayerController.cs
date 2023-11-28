using Google.Protobuf.Protocol;
using System;
using UnityEngine;

public class MyPlayerController : MonoBehaviour
{
    [SerializeField]
    float _moveSpeed = 15.0f;

    [SerializeField]
    float _rotationSpeed = 15.0f;

    private PlayerInputController _controller;

    private Vector3 _movementDirection = Vector3.zero;
    private Animator _anim;
    private bool wDown;

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
                ApplyMovement(_movementDirection);
                break;
        }
    }

    private void Move(Vector3 direction)
    {
        _movementDirection = direction;
    }

    private void ApplyMovement(Vector3 direction)
    {
        _anim.SetBool("isWalk", wDown);
        wDown = Input.GetButton("Walk");
        if (wDown)
            direction = direction * 5f;
        else
            direction = direction * _moveSpeed;

        if (IsWallCheck() == false)
        {
            transform.position += direction * Time.deltaTime;
            _anim.SetBool("isRun", true);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * _rotationSpeed);

        SendMovePacket();
    }

    private bool IsWallCheck()
    {
        bool isWall = Physics.Raycast(transform.position, transform.forward + Vector3.up, 3, LayerMask.GetMask("Wall"));
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
