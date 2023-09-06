using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _speed = 5.0f;

    private Define.State _state = Define.State.Moving;

    private bool _moveKeyPressed = false;

    private Define.MoveDir _moveDir = Define.MoveDir.Down;

    public Vector3Int _cellPos = new Vector3Int();

    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        Init();
    }

    void Update()
    {
        UpdateController();
    }

    protected virtual void Init()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
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

    protected virtual void UpdateMoving()
    {
        Vector3 destPos = _cellPos + new Vector3(0.5f, 0.5f);
        Vector3 moveDir = destPos - transform.position;

        float dist = moveDir.magnitude;
        if (dist < _speed * Time.deltaTime)
        {
            transform.position = destPos;
            MoveToNextPos();
        }
        else
        {
            transform.position += moveDir.normalized * _speed * Time.deltaTime;
            _state = Define.State.Moving;
        }
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
        }
    }


    protected virtual void MoveToNextPos()
    {
        if (_moveKeyPressed == false)
        {
            _state = Define.State.Idle;
            return;
        }

        switch (_moveDir)
        {
            case Define.MoveDir.Up:
                _cellPos += Vector3Int.up;
                break;
            case Define.MoveDir.Down:
                _cellPos += Vector3Int.down;
                break;
            case Define.MoveDir.Left:
                _cellPos += Vector3Int.left;
                break;
            case Define.MoveDir.Right:
                _cellPos += Vector3Int.right;
                break;
        }
    }
}
