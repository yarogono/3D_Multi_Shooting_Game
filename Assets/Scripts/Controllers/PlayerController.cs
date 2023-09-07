using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 5.0f;

    private Define.State _state = Define.State.Moving;

    private bool _moveKeyPressed = false;

    private Define.MoveDir _moveDir = Define.MoveDir.Down;

    private SpriteRenderer _spriteRenderer;

    private Rigidbody2D _rigidbody2D;


    private void Start()
    {
        Init();
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {   
        UpdateController();
    }

    private void LateUpdate()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    protected virtual void Init()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    protected virtual void UpdateController()
    {
        UpdatePlayerSight();

        switch (_state)
        {
            case Define.State.Idle:
                GetDirInput();
                break;
            case Define.State.Moving:
                GetDirInput();
                break;
        }


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

    private void UpdatePlayerSight()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 point = Camera.main.ScreenToWorldPoint(mousePosition + new Vector3(0, 0, 0));

        if (point.x > transform.position.x)
            _spriteRenderer.flipX = false;
        else
            _spriteRenderer.flipX = true;
    }

    protected virtual void UpdateIdle()
    {

    }

    void GetDirInput()
    {
        _moveKeyPressed = true;

        if (Input.GetKey(KeyCode.W))
        {
            _moveDir = Define.MoveDir.Up;
            _state = Define.State.Moving;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _moveDir = Define.MoveDir.Down;
            _state = Define.State.Moving;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _moveDir = Define.MoveDir.Left;
            _state = Define.State.Moving;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _moveDir = Define.MoveDir.Right;
            _state = Define.State.Moving;
        }
        else
        {
            _moveKeyPressed = false;
            _rigidbody2D.velocity = Vector2.zero;
        }
    }


    protected virtual void UpdateMoving()
    {
        if (_moveKeyPressed == false)
        {
            _state = Define.State.Idle;
            return;
        }

        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");

        switch (_moveDir)
        {
            case Define.MoveDir.Up:
                _rigidbody2D.velocity = Vector2.up * _speed;
                break;
            case Define.MoveDir.Down:
                _rigidbody2D.velocity = Vector2.down * _speed;
                break;
            case Define.MoveDir.Left:
                _rigidbody2D.velocity = Vector2.left * _speed;
                break;
            case Define.MoveDir.Right:
                _rigidbody2D.velocity = Vector2.right * _speed;
                break;
        }
    }
}
