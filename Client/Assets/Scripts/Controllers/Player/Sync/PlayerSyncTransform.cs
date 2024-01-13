using Assets.Scripts.Controllers.Player;
using Google.Protobuf;
using Google.Protobuf.Protocol;
using Google.Protobuf.WellKnownTypes;
using System;
using UnityEngine;

[AddComponentMenu("Player/PlayerSyncTransform")]
public class PlayerSyncTransform : BasePlayerSyncController, ISyncObservable
{

    [SerializeField] [Tooltip("회전 속도")] private float _rotationSpeed = 15.0f;
    [SerializeField] [Tooltip("달리기 속도")] private float _runSpeed = 15.0f;
    [SerializeField] [Tooltip("걷기 속도")]  private float _walkSpeed = 12.0f;
    [SerializeField] [Tooltip("동기화 오차범위")] private float _syncMargin = 0.1f;

    private PlayerInputController _inputController;

    private Vector3 _movementDirection;
    private Vector3 _direction;
    private Vector3 _storedPosition;

    private float _moveSpeed = 0f;
    public float MoveSpeed 
    { 
        get { return _moveSpeed; }
    }

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

        _inputController = GetComponent<PlayerInputController>();
    }

    private void Start()
    {
        if (playerController.IsMine)
            _inputController.OnMoveEvent += Move;
    }

    private void Update()
    {
        if (this.playerController.IsMine == true)
        {
            UpdateMyPlayer();
        }
        else
        {
            UpdateEnemyPlayer();
        }
    }

    #region SyncPacket
    public void OnSync(IMessage packet)
    {
        switch (packet)
        {
            case S_Move movePacket:
                OnSyncMovePacket(movePacket);
                break;
        }
    }

    private void OnSyncMovePacket(S_Move movePacket)
    {
        double sentServerTime = CalSentServerTime(movePacket.ServerTimestamp);
        float lag = Mathf.Abs((float)(ClientNetworkTime(sentServerTime) - sentServerTime));
        this.PosInfo = movePacket.PosInfo;

        Vector3 networkPos = new Vector3(PosInfo.X, PosInfo.Y, PosInfo.Z);
        networkPos += this._direction * lag;

        this.PosInfo = new Vec3() { X = networkPos.x, Y = networkPos.y, Z = networkPos.z };
        this._moveSpeed = movePacket.MoveSpeed;
        State = CreatureState.Moving;
    }

    public double CalSentServerTime(Google.Protobuf.WellKnownTypes.Timestamp serverTimestamp)
    {
        long seconds = serverTimestamp.Seconds;
        int nanos = serverTimestamp.Nanos;

        // Convert to milliseconds (1 second = 1000 milliseconds)
        double milliseconds = seconds * 1000.0d;

        // Add nanoseconds converted to milliseconds
        milliseconds += nanos / 1_000_000.0d;
        return milliseconds;
    }

    private int frame;
    private double frametime;

    public double ClientNetworkTime(double sentServerTime)
    {
        if (UnityEngine.Time.frameCount == frame)
        {
            return frametime;
        }

        uint u = (uint)sentServerTime;
        double t = u;
        frametime = t / 1000.0d;
        frame = UnityEngine.Time.frameCount;
        return frametime;
    }
    #endregion

    #region MyPlayer

    private void UpdateMyPlayer()
    {
        if (_movementDirection == Vector3.zero)
        {
            State = CreatureState.Idle;
        }
        else
        {
            State = CreatureState.Moving;
        }

        switch (State)
        {
            case CreatureState.Idle:
                UpdateMyPlayerIdle();
                break;
            case CreatureState.Moving:
                UpdateMyPlayerMoving(_movementDirection);
                break;
        }
    }

    private void Move(Vector3 direction)
    {
        _movementDirection = direction;
    }

    private void UpdateMyPlayerIdle()
    {
        _moveSpeed = 0f;
    }

    private void UpdateMyPlayerMoving(Vector3 direction)
    {
        direction = MultiplyMyPlayerMoveSpeed(direction);

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * _rotationSpeed);

        if (IsWallCheck() == true)
            return;

        transform.position += direction * Time.deltaTime;

        this._direction = transform.localPosition - _storedPosition;
        this._storedPosition = transform.localPosition;

        SendMovePacket();
    }


    private Vector3 MultiplyMyPlayerMoveSpeed(Vector3 direction)
    {
        bool isPlayerWalk = Input.GetButton("Walk");
        if (isPlayerWalk)
        {
            _moveSpeed = _walkSpeed;
            direction *= _moveSpeed;
        }
        else
        {
            _moveSpeed = _runSpeed;
            direction *= _moveSpeed;
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

            MoveSpeed = _moveSpeed
        };
        NetworkManager.Instance.Send(movePacket);
    }
    #endregion

    #region Enemy Player
    private void UpdateEnemyPlayer()
    {
        switch (State)
        {
            case CreatureState.Idle:
                UpdateEnemyIdle();
                break;
            case CreatureState.Moving:
                UpdateEnemyPlayerMoving();
                break;
        }
    }

    private void UpdateEnemyIdle()
    {
        _moveSpeed = 0f;
    }

    private void UpdateEnemyPlayerMoving()
    {
        Vector3 destPos = new Vector3(PosInfo.X, PosInfo.Y, PosInfo.Z);
        float distance = Vector3.Distance(transform.position, destPos);

        if (distance <= _syncMargin)
        {
            State = CreatureState.Idle;
            return;
        }

        Vector3 nextPosition = Vector3.MoveTowards(transform.position, destPos, distance * Time.deltaTime * 10);

        Vector3 moveDir = nextPosition - transform.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir), Time.deltaTime * _rotationSpeed);

        // 다음 위치로 이동
        transform.position = nextPosition;
    }
    #endregion
}
