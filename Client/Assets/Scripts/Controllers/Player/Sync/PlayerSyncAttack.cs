using Assets.Scripts.Controllers.Player;
using Google.Protobuf;
using Google.Protobuf.Protocol;
using System.Collections;
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

    private bool _isHit;

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
            case S_DamageMelee:
                OnSyncDamageMelee(packet);
                break;
        }
    }

    private void OnSyncDamageMelee(IMessage packet)
    {
        S_DamageMelee damageMeleePacket = (S_DamageMelee)packet;

        if (damageMeleePacket.TargetPlayerId != playerController.Id)
            return;

        int getDamagedHp = playerController.Hp - damageMeleePacket.Damage;
        playerController.Hp = getDamagedHp;
    }

    private void OnSyncMeleeAttack()
    {
        _meleeWeaponController.WeaponAttack();
        _playerSyncAnimation.WeaponAttackSwingAnimation();
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Melee"))
        {
            if (_isHit)
                return;

            _isHit = true;
            MeleeWeaponController melee = other.GetComponent<MeleeWeaponController>();
            StartCoroutine(SendDamagePacket(playerController.Id, melee.Damage));
        }
    }

    private IEnumerator SendDamagePacket(int targetPlayerId, int damage)
    {
        C_DamageMelee damageMeleePacket = new C_DamageMelee()
        {
            TargetPlayerId = targetPlayerId,
            Damage = damage,
            TargetPosInfo = _playerSyncTransform.PosInfo,
        };
        NetworkManager.Instance.Send(damageMeleePacket);

        yield return new WaitForSeconds(1.5f);
        _isHit = false;
    }
}
