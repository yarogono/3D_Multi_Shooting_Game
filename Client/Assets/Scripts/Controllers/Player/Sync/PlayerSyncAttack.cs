using Assets.Scripts.Controllers.Player;
using Google.Protobuf;
using Google.Protobuf.Protocol;
using UnityEngine;
using static Define;

[AddComponentMenu("Player/PlayerSyncAttack")]
public class PlayerSyncAttack : BasePlayerSyncController, ISyncObservable
{
    private PlayerInputController _inputController;
    private MeleeWeaponController _meleeWeaponController;

    private PlayerSyncItem _playerSyncItem;
    private PlayerSyncAnimation _playerSyncAnimation;
    private PlayerSyncTransform _playerSyncTransform;

    private GameObject _meleeGameObject;

    private float _attackDelayTimer;

    private void Awake()
    {
        _inputController = GetComponent<PlayerInputController>();
        _playerSyncItem = GetComponent<PlayerSyncItem>();
        _playerSyncAnimation = GetComponent<PlayerSyncAnimation>();
        _playerSyncTransform = GetComponent<PlayerSyncTransform>();

        _meleeGameObject = _playerSyncItem.MeleeWeaponGameObject;
        _meleeWeaponController = _meleeGameObject.GetComponent<MeleeWeaponController>();
    }

    private void Start()
    {
        if (playerController.IsMine)
        {
            _inputController.OnAttackEvent += PlayerAttack;
        }
    }

    private void Update()
    {
        _attackDelayTimer += Time.deltaTime;
    }

    public void PlayerAttack()
    {
        ItemNumber handHeldWeapon = _playerSyncItem.HandHeldWeapon;
        if (handHeldWeapon == ItemNumber.None)
            return;

        if (handHeldWeapon == ItemNumber.One)
        {
            MeleeAttack();
        }
        else
        {
            // ToDo : 총 아이템 => 총 아이템 공격 로직 실행
            Debug.Log("GunAttack");
        }
    }

    private void MeleeAttack()
    {
        if (_meleeWeaponController.Rate < _attackDelayTimer)
        {
            _meleeWeaponController.WeaponAttack();
            _playerSyncAnimation.WeaponAttackSwingAnimation();
            _attackDelayTimer = 0;
            _playerSyncTransform.StopPlayerMoving();

            SendMeleeAttackPacket();
        }
    }

    private void SendMeleeAttackPacket()
    {
        Vec3 meleeAttackPlayerPosInfo = new Vec3();
        meleeAttackPlayerPosInfo.MergeFrom(_playerSyncTransform.PosInfo);

        C_MeleeAttack meleeAttackPacket = new C_MeleeAttack()
        {
            AttackPlayerId = playerController.Id,
            PosInfo = meleeAttackPlayerPosInfo,
        };

        NetworkManager.Instance.Send(meleeAttackPacket);
    }

    #region OnSync
    public void OnSync(IMessage packet)
    {
        switch (packet)
        {
            case S_MeleeAttack:
                OnSyncMeleeAttack();
                break;
        }
    }

    private void OnSyncMeleeAttack()
    {
        _meleeWeaponController.WeaponAttack();
        _playerSyncAnimation.WeaponAttackSwingAnimation();
    }
    #endregion
}
