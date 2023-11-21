using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 5.0f;

    float hAxis;
    float vAxis;
    bool wDown;

    Vector3 moveVec;

    Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
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

        if (wDown)
            transform.position += moveVec * _speed * 0.3f * Time.deltaTime;
        else
            transform.position += moveVec * _speed * Time.deltaTime;

        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);

        transform.LookAt(transform.position + moveVec);
    }

    private void FixedUpdate()
    {   
        //UpdateController();
    }

    protected virtual void Init()
    {

    }

    protected virtual void UpdateController()
    {
        //switch (_state)
        //{
        //    case Define.State.Idle:
        //        GetDirInput();
        //        break;
        //    case Define.State.Moving:
        //        GetDirInput();
        //        break;
        //}


        //switch (_state)
        //{
        //    case Define.State.Idle:
        //        UpdateIdle();
        //        break;
        //    case Define.State.Moving:
        //        UpdateMoving();
        //        break;
        //}
    }

    protected virtual void UpdateIdle()
    {

    }

    void GetDirInput()
    {
        //_moveKeyPressed = true;

        //if (Input.GetKey(KeyCode.W))
        //{
        //    _moveDir = Define.MoveDir.Up;
        //    _state = Define.State.Moving;
        //}
        //else if (Input.GetKey(KeyCode.S))
        //{
        //    _moveDir = Define.MoveDir.Down;
        //    _state = Define.State.Moving;
        //}
        //else if (Input.GetKey(KeyCode.A))
        //{
        //    _moveDir = Define.MoveDir.Left;
        //    _state = Define.State.Moving;
        //}
        //else if (Input.GetKey(KeyCode.D))
        //{
        //    _moveDir = Define.MoveDir.Right;
        //    _state = Define.State.Moving;
        //}
        //else
        //{
        //    _moveKeyPressed = false;
        //    _rigidbody2D.velocity = Vector2.zero;
        //}
    }


    protected virtual void UpdateMoving()
    {
        //if (_moveKeyPressed == false)
        //{
        //    _state = Define.State.Idle;
        //    return;
        //}

        //float xMove = Input.GetAxis("Horizontal");
        //float zMove = Input.GetAxis("Vertical");

        //switch (_moveDir)
        //{
        //    case Define.MoveDir.Up:
        //        _rigidbody2D.velocity = Vector2.up * _speed;
        //        break;
        //    case Define.MoveDir.Down:
        //        _rigidbody2D.velocity = Vector2.down * _speed;
        //        break;
        //    case Define.MoveDir.Left:
        //        _rigidbody2D.velocity = Vector2.left * _speed;
        //        break;
        //    case Define.MoveDir.Right:
        //        _rigidbody2D.velocity = Vector2.right * _speed;
        //        break;
        //}
    }
}
