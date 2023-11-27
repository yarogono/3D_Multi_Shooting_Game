using Google.Protobuf.Protocol;
using System;
using UnityEngine;

public class MyPlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 15.0f;

    private PlayerInputController _controller;

    private Vector3 _movementDirection = Vector3.zero;
    private Rigidbody _rigidbody;
    private bool wDown;
    private Animator _anim;

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
        _rigidbody = GetComponent<Rigidbody>();
        _anim = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        _controller.OnMoveEvent += Move;
    }


    private void FixedUpdate()
    {
        switch (State)
        {
            case CreatureState.Idle:
                break;
            case CreatureState.Moving:
                ApplyMovement(_movementDirection);
                break;
        }

        if (State == CreatureState.Moving)
            State = CreatureState.Idle;
    }


    private void Move(Vector3 direction)
    {
        State = CreatureState.Moving;
        _movementDirection = direction;
    }

    private void ApplyMovement(Vector3 direction)
    {
        _anim.SetBool("isRun", direction != Vector3.zero);
        _anim.SetBool("isWalk", wDown);

        wDown = Input.GetButton("Walk");
        if (wDown)
            direction = direction * _speed * 0.3f;
        else
            direction = direction * _speed;

        _rigidbody.velocity = direction;

        transform.LookAt(transform.position + direction);

        SendMovePacket();
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
