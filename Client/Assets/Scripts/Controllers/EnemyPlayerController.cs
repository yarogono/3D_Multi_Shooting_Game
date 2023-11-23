using Google.Protobuf.Protocol;
using UnityEngine;

public class EnemyPlayerController : CreatureController
{
    [SerializeField]
    float _speed = 15.0f;

    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        UpdateController();
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

    protected virtual void UpdateIdle()
    {

    }

    protected virtual void UpdateMoving()
    {
        Vector3 destPos = new Vector3(PosInfo.PosX, PosInfo.PosY, PosInfo.PosZ);
        Vector3 moveDir = destPos - transform.position;

        _anim.SetBool("isRun", transform.position != destPos);

        // 도착 여부 체크
        float dist = moveDir.magnitude;
        if (dist < _speed * Time.deltaTime)
        {
            transform.position = destPos;
        }
        else
        {
            transform.LookAt(transform.position + moveDir.normalized);
            transform.position += moveDir.normalized * _speed * Time.deltaTime;
            State = CreatureState.Moving;
        }
;
    }
}
