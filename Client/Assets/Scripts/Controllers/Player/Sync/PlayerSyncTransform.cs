using Assets.Scripts.Controllers.Player;
using Google.Protobuf;
using Google.Protobuf.Protocol;
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
        get
        {
            return new Vec3()
            {
                X = _positionInfo.X,
                Y = _positionInfo.Y,
                Z = _positionInfo.Z,
            };
        }
        set => _positionInfo = value;
    }

    private void Awake()
    {
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

    #region OnSync
    public void OnSync(IMessage packet)
    {
        if (packet == null)
            return;

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
        Vec3 movePacketPos = movePacket.PosInfo;

        Vector3 networkPos = new Vector3(movePacketPos.X, movePacketPos.Y, movePacketPos.Z);
        networkPos += this._direction * lag;

        this._movementDirection = networkPos;
        this._moveSpeed = movePacket.MoveSpeed;
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
            this.playerController.State = CreatureState.Idle;
        }
        else
        {
            this.playerController.State = CreatureState.Moving;
        }

        switch (this.playerController.State)
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

    public void StopPlayerMoving()
    {
        _movementDirection = Vector3.zero;
    }
    #endregion

    #region Enemy Player
    private void UpdateEnemyPlayer()
    {
        if (_movementDirection == Vector3.zero)
        {
            this.playerController.State = CreatureState.Idle;
        }
        else
        {
            this.playerController.State = CreatureState.Moving;
        }

        switch (playerController.State)
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
        float distance = Vector3.Distance(transform.position, _movementDirection);

        Vector3 nextPosition = Vector3.MoveTowards(transform.position, _movementDirection, distance * Time.deltaTime * 10);

        Vector3 moveDir = nextPosition - transform.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir), Time.deltaTime * _rotationSpeed);

        if (distance <= _syncMargin)
        {
            this.playerController.State = CreatureState.Idle;
            return;
        }

        // 다음 위치로 이동
        transform.position = nextPosition;
    }
    #endregion
}
