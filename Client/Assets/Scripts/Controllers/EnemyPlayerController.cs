using Google.Protobuf.Protocol;
using UnityEngine;

public class EnemyPlayerController : CreatureController
{
    [SerializeField]
    float _speed = 15.0f;

    private Animator _anim;

    private Vector3 currentPosition;
    private Vector3 lastPosition;
    private Vector3 velocity = Vector3.zero; // 데드 레커닝에 사용될 속도 벡터
    private float smoothTime = 0.1f; // 부드러운 데드 레커닝을 위한 시간 매개 변수

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
    }


    void Start()
    {
        // 초기 위치 설정
        currentPosition = transform.position;
        lastPosition = currentPosition;
    }


    void Update()
    {
        UpdateController();
    }

    protected virtual void UpdateController()
    {
        if (transform.position != new Vector3(PosInfo.PosX, PosInfo.PosY, PosInfo.PosZ))
            State = CreatureState.Moving;
        else
            State = CreatureState.Idle;

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
        _anim.SetBool("isRun", false);
    }

    protected virtual void UpdateMoving()
    {
        Vector3 destPos = new Vector3(PosInfo.PosX, PosInfo.PosY, PosInfo.PosZ);

        // 부드러운 데드 레커닝을 위해 SmoothDamp 사용
        Vector3 nextPosition = Vector3.SmoothDamp(transform.position, destPos, ref velocity, smoothTime, _speed);

        _anim.SetBool("isRun", transform.position != nextPosition);

        Vector3 moveDir = nextPosition - transform.position;
        transform.LookAt(transform.position + moveDir.normalized);

        // 다음 위치로 이동
        transform.position = nextPosition;

        State = CreatureState.Moving;
    }
}
