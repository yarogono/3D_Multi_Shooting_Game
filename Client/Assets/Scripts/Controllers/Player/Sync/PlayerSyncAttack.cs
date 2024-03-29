using Assets.Scripts.Controllers.Player;
using Data;
using Google.Protobuf;
using Google.Protobuf.Protocol;
using System.Collections;
using UnityEngine;
using static Define;

[AddComponentMenu("Player/PlayerSyncAttack")]
public class PlayerSyncAttack : BasePlayerSyncController, ISyncObservable
{
    private PlayerInputController _inputController;
    private PlayerWeaponController _playerWeaponController;

    private PlayerSyncAnimation _playerSyncAnimation;
    private PlayerSyncTransform _playerSyncTransform;

    private float _attackDelayTimer;

    private bool _isHit;

    private void Awake()
    {
        _inputController = GetComponent<PlayerInputController>();
        _playerSyncAnimation = GetComponent<PlayerSyncAnimation>();
        _playerSyncTransform = GetComponent<PlayerSyncTransform>();
        _playerWeaponController = GetComponent<PlayerWeaponController>();
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
        ItemNumber handHeldWeapon = _playerWeaponController.HandHeldWeapon;
        if (handHeldWeapon == ItemNumber.None)
            return;

        switch (handHeldWeapon)
        {
            case ItemNumber.One:
                MeleeAttack();
                break;
            case ItemNumber.Two:
                GunAttack();
                break;
            case ItemNumber.Three:
                GunAttack();
                break;
        }
    }

    private void MeleeAttack()
    {
        Weapon weapon = DataManager.Instance.WeaponDict[(int)_playerWeaponController.HandHeldWeapon];
        if (weapon.rate < _attackDelayTimer)
        {
            _playerWeaponController.CurrentWeaponAttack.Attack();
            _playerSyncAnimation.WeaponAttackSwingAnimation();
            _attackDelayTimer = 0;
            _playerSyncTransform.StopPlayerMoving();

            SendMeleeAttackPacket();
        }
    }

    private void SendMeleeAttackPacket()
    {
        C_MeleeAttack meleeAttackPacket = new C_MeleeAttack()
        {
            AttackPlayerId = playerController.Id,
        };

        NetworkManager.Instance.Send(meleeAttackPacket);
    }

    private void GunAttack()
    {
        Weapon weapon = DataManager.Instance.WeaponDict[(int)_playerWeaponController.HandHeldWeapon];
        if (weapon.rate < _attackDelayTimer)
        {
            _playerWeaponController.CurrentWeaponAttack.Attack();
            _playerSyncAnimation.WeaponGunAttackAnimation();
            _playerSyncTransform.StopPlayerMoving();

            SendGunAttackPacket();
        }
    }

    private void SendGunAttackPacket()
    {
        C_GunAttack gunAttackPacket = new C_GunAttack()
        {
            AttackPlayerId = playerController.Id,
        };

        NetworkManager.Instance.Send(gunAttackPacket);
    }

    #region OnSync
    public void OnSync(IMessage packet)
    {
        switch (packet)
        {
            case S_MeleeAttack:
                OnSyncMeleeAttack();
                break;
            case S_GunAttack:
                OnSyncGunAttack();
                break;
            case S_DamageMelee:
                OnSyncDamageMelee(packet);
                break;
            case S_DamageBullet:
                OnSyncDamageBullet(packet);
                break;
        }
    }

    private void OnSyncGunAttack()
    {
        _playerWeaponController.CurrentWeaponAttack.OnSyncAttack();
        _playerSyncAnimation.WeaponGunAttackAnimation();
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
        _playerWeaponController.CurrentWeaponAttack.OnSyncAttack();
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
            Weapon meleeWeapon = DataManager.Instance.WeaponDict[(int)ItemNumber.One];
            StartCoroutine(SendMeleeDamagePacket(playerController.Id, meleeWeapon));
        }
        else if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            if (playerController.IsMine == false)
                return;

            BulletController bullet = other.GetComponent<BulletController>();

            if (bullet.ShooterId == playerController.Id)
                return;

            SendDamageBulletPacket(playerController.Id, bullet.Damage);
        }
    }

    private IEnumerator SendMeleeDamagePacket(int targetPlayerId, Weapon melee)
    {
        C_DamageMelee damageMeleePacket = new C_DamageMelee()
        {
            TargetPlayerId = targetPlayerId,
            Damage = melee.damage,
            TargetPosInfo = _playerSyncTransform.PosInfo,
            MeleeItemNumber = melee.id,
        };
        NetworkManager.Instance.Send(damageMeleePacket);

        yield return new WaitForSeconds(0.1f);
        _isHit = false;
    }

    private void SendDamageBulletPacket(int targetPlayerId, int bulletDamage)
    {
        C_DamageBullet damageBulletPacket = new C_DamageBullet()
        {
            TargetPlayerId = targetPlayerId,
            Damage = bulletDamage,
            TargetPosInfo = _playerSyncTransform.PosInfo,
        };
        NetworkManager.Instance.Send(damageBulletPacket);
    }
}
