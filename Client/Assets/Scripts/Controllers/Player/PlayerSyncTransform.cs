using Assets.Scripts.Controllers.Player;
using Google.Protobuf;
using Google.Protobuf.Protocol;
using UnityEngine;

[AddComponentMenu("Player/PlayerSyncTransform")]
public class PlayerSyncTransform : BasePlayerSyncController, ISyncObservable
{
    [SerializeField]
    private float _runSpeed = 15.0f;

    [SerializeField]
    private float _rotationSpeed = 15.0f;

    [SerializeField]
    private float _walkSpeed = 5.0f;

    private PlayerInputController _controller;
    //private Animator _anim;

    private bool _isWalk;
    private Vector3 _movementDirection = Vector3.zero;

    private Vector3 _Direction;
    private Vector3 _StoredPosition;


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
        //_anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        if (playerController.IsMine)
            _controller.OnMoveEvent += Move;
    }

    private void Update()
    {
        if (this.playerController.IsMine == true)
        {
            if (_movementDirection == Vector3.zero)
            {
                //_anim.SetBool("isRun", false);
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
                    UpdateMyPlayerMoving(_movementDirection);
                    break;
            }
        }
        else if (this.playerController.IsMine == false)
        {
            UpdateEnemyPlayerController();
        }
    }

    #region Sync
    public void OnSync(IMessage packet)
    {
        S_Move movePacket = (S_Move)packet;

        double sentServerTime = CalSentServerTime(movePacket.ServerTimestamp);
        float lag = Mathf.Abs((float)(ClientNetworkTime(sentServerTime) - sentServerTime));
        this.PosInfo = movePacket.PosInfo;

        Vector3 networkPos = new Vector3(PosInfo.X, PosInfo.Y, PosInfo.Z);
        networkPos += this._Direction * lag;

        this.PosInfo = new Vec3() { X = networkPos.x, Y = networkPos.y, Z = networkPos.z };
        this.State = CreatureState.Moving;
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
    private void Move(Vector3 direction)
    {
        _movementDirection = direction;
    }

    private void UpdateMyPlayerMoving(Vector3 direction)
    {
        direction = MultiplyMyPlayerMoveSpeed(direction);

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * _rotationSpeed);

        if (IsWallCheck() == true)
            return;

        transform.position += direction * Time.deltaTime;

        this._Direction = transform.localPosition - _StoredPosition;
        this._StoredPosition = transform.localPosition;

        SendMovePacket();
    }


    private Vector3 MultiplyMyPlayerMoveSpeed(Vector3 direction)
    {
        //_isWalk = Input.GetButton("Walk");
        //_anim.SetBool("isWalk", _isWalk);
        if (_isWalk)
        {
            direction = direction * _walkSpeed;
        }
        else
        {
            //_anim.SetBool("isRun", true);
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
    #endregion

    #region Enemy Player
    private Vector3 velocity = Vector3.zero; // 데드 레커닝에 사용될 속도 벡터
    private float smoothTime = 0.1f; // 부드러운 데드 레커닝을 위한 시간 매개 변수

    private void UpdateEnemyPlayerController()
    {
        if (transform.position != new Vector3(PosInfo.X, PosInfo.Y, PosInfo.Z))
            State = CreatureState.Moving;
        else
            State = CreatureState.Idle;

        switch (State)
        {
            case CreatureState.Idle:
                UpdateIdle();
                break;
            case CreatureState.Moving:
                UpdateEnemyPlayerMoving();
                break;
        }
    }

    private void UpdateIdle()
    {
        //_anim.SetBool("isRun", false);
    }

    private void UpdateEnemyPlayerMoving()
    {
        Vector3 destPos = new Vector3(PosInfo.X, PosInfo.Y, PosInfo.Z);


        // TODO : 동기화 관련 코드 수정
        //float distance = Vector3.Distance(transform.position, destPos);
        //Vector3 nextPosition = Vector3.MoveTowards(transform.position, destPos, distance * Time.deltaTime * 10);

        // 부드러운 데드 레커닝을 위해 SmoothDamp 사용
        Vector3 nextPosition = Vector3.SmoothDamp(transform.position, destPos, ref velocity, smoothTime, _runSpeed);

        //_anim.SetBool("isRun", transform.position != nextPosition);

        Vector3 moveDir = nextPosition - transform.position;
        transform.LookAt(transform.position + moveDir.normalized);

        // 다음 위치로 이동
        transform.position = nextPosition;

        State = CreatureState.Moving;
    }
    #endregion
}
