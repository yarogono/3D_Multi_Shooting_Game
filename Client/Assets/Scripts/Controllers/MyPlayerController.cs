using Google.Protobuf.Protocol;
using UnityEngine;

public class MyPlayerController : CreatureController
{
    [SerializeField]
    float _speed = 5.0f;

    public StatInfo StatInfo { get; set; }

    private float hAxis;
    private float vAxis;
    private bool wDown;

    private Vector3 moveVec;

    private Animator _anim;

    private Define.State _state;


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
        wDown = Input.GetButton("Walk");

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        if (transform.position != moveVec)
            _state = Define.State.Moving;
        else if (transform.position == moveVec)
            _state = Define.State.Idle;

        UpdateController();
    }

    protected virtual void Init()
    {

    }

    protected virtual void UpdateController()
    {
        switch (_state)
        {
            case Define.State.Idle:
                UpdateIdle();
                break;
            case Define.State.Moving:
                UpdateMoving();
                break;
        }
    }

    protected virtual void UpdateIdle()
    {

    }

    protected virtual void UpdateMoving()
    {
        if (wDown)
            transform.position += moveVec * _speed * 0.3f * Time.deltaTime;
        else
            transform.position += moveVec * _speed * Time.deltaTime;

        _anim.SetBool("isRun", moveVec != Vector3.zero);
        _anim.SetBool("isWalk", wDown);

        transform.LookAt(transform.position + moveVec);

        _state = Define.State.Idle;
    }
}
