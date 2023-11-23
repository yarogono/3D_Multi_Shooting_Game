using Google.Protobuf.Protocol;
using UnityEngine;

public class MyPlayerController : CreatureController
{
    [SerializeField]
    float _speed = 15.0f;

    public StatInfo StatInfo { get; set; }

    private float hAxis;
    private float vAxis;
    private bool wDown;

    private Animator _anim;

    private Vector3 _moveVec;

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
    }


    void Start()
    {
        Init();
    }

    void Update()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        _moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        _anim.SetBool("isRun", _moveVec != Vector3.zero);
        _anim.SetBool("isWalk", wDown);

        if (_moveVec == Vector3.zero)
        {
            State = CreatureState.Idle;
        }
        else
        {
            State = CreatureState.Moving;
        }

        UpdateController();
    }

    protected virtual void Init()
    {

    }

    protected virtual void UpdateController()
    {
        switch (State)
        {
            case CreatureState.Idle:
                UpdateIdle();
                break;
            case CreatureState.Moving:
                UpdateMoving();
                break;
        }
    }

    private void UpdateIdle()
    {

    }

    private void UpdateMoving()
    {
        wDown = Input.GetButton("Walk");
        if (wDown)
            transform.position += _moveVec * _speed * 0.3f * Time.deltaTime;
        else
            transform.position += _moveVec * _speed * Time.deltaTime;

        transform.LookAt(transform.position + _moveVec);

        CellPos = transform.position;

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
