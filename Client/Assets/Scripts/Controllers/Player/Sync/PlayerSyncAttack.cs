using Assets.Scripts.Controllers.Player;
using Google.Protobuf;
using Google.Protobuf.Protocol;
using System;
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
            case S_DamageBullet:
                OnSyncDamageBullet(packet);
                break;
        }
    }

    private void OnSyncDamageBullet(IMessage packet)
    {
        S_DamageBullet damageBulletPacket = (S_DamageBullet)packet;

        if (damageBulletPacket.TargetPlayerId != playerController.Id)
            return;

        playerController.GetDamage(damageBulletPacket.Damage);
        _playerSyncAnimation.GetDamageAnimation();
    }

    private void OnSyncDamageMelee(IMessage packet)
    {
        S_DamageMelee damageMeleePacket = (S_DamageMelee)packet;

        if (damageMeleePacket.TargetPlayerId != playerController.Id)
            return;

        playerController.GetDamage(damageMeleePacket.Damage);
        _playerSyncAnimation.GetDamageAnimation();
    }

    private void OnSyncMeleeAttack()
    {
        _meleeWeaponController.OnSyncWeaponAttack();
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
            StartCoroutine(SendMeleeDamagePacket(playerController.Id, melee));
        }
        else if (other.CompareTag("Bullet"))
        {
            BulletController bullet = other.GetComponent<BulletController>();
            StartCoroutine(SendBulletDamagePacket(playerController.Id, bullet));
        }
    }

    private IEnumerator SendMeleeDamagePacket(int targetPlayerId, MeleeWeaponController melee)
    {
        C_DamageMelee damageMeleePacket = new C_DamageMelee()
        {
            TargetPlayerId = targetPlayerId,
            Damage = melee.Damage,
            TargetPosInfo = _playerSyncTransform.PosInfo,
            MeleeItemNumber = melee.AttackRange,
        };
        NetworkManager.Instance.Send(damageMeleePacket);

        yield return new WaitForSeconds(0.1f);
        _isHit = false;
    }

    private IEnumerator SendBulletDamagePacket(int targetPlayerId, BulletController bullet)
    {
        C_DamageBullet damageBulletPacket = new C_DamageBullet()
        {
            TargetPlayerId = targetPlayerId,
            Damage = bullet.Damage,
            TargetPosInfo = _playerSyncTransform.PosInfo,
        };
        NetworkManager.Instance.Send(damageBulletPacket);

        yield return null;
    }
}
