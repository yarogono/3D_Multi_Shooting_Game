using Assets.Scripts.Controllers.Player;
using Google.Protobuf.Protocol;
using UnityEngine;
using static Define;

public class PlayerWeaponController : BasePlayerSyncController
{
    [SerializeField] private GameObject[] _weapons;
    [SerializeField] private bool[] _hasWeapon;

    private PlayerInputController _inputController;
    private PlayerSyncAnimation _playerSyncAnimation;

    private WeaponController[] _weaponsController;

    private IAttackable _currentWeaponAttack;
    private ItemNumber _handheldWeapon;

    public IAttackable CurrentWeaponAttack
    {
        get => _currentWeaponAttack;
    }

    public ItemNumber HandHeldWeapon
    {
        get => _handheldWeapon;
        set => _handheldWeapon = value;
    }

    public bool[] HasWeapon
    {
        get => _hasWeapon;
    }

    public GameObject[] Weapons
    {
        get => _weapons;
    }

    private void Awake()
    {
        _inputController = GetComponent<PlayerInputController>();
        _playerSyncAnimation = GetComponent<PlayerSyncAnimation>();

        _handheldWeapon = ItemNumber.None;

        _weaponsController = new WeaponController[_weapons.Length];
        for (int i = 0; i < _weapons.Length; i++)
        {
            _weaponsController[i] = _weapons[i].GetComponent<WeaponController>();
        }
    }

    private void Start()
    {
        if (playerController.IsMine)
        {
            _inputController.OnWeaponSwapEvent += WeaponSwap;
        }
    }

    private void WeaponSwap(ItemNumber itemNumber)
    {
        if (_hasWeapon[(int)itemNumber] == false)
            return;

        if ((int)itemNumber > _weapons.Length)
            return;

        if (itemNumber == _handheldWeapon)
            return;

        if (_handheldWeapon != ItemNumber.None)
            HandHeldWeaponActive(false);

        WeaponActive((int)itemNumber);
        _handheldWeapon = itemNumber;

        SwapWeaponAttack();

        _playerSyncAnimation.WeaponSwapAnimation();

        SendSwapWeaponItemPacket(itemNumber);
    }

    public void SwapWeaponAttack()
    {
        _currentWeaponAttack = (IAttackable)_weaponsController[(int)_handheldWeapon];
    }

    private void SendSwapWeaponItemPacket(ItemNumber itemNumber)
    {
        C_SwapWeaponItem swapWeaponItemPacket = new C_SwapWeaponItem()
        {
            WeaponItemNumber = (int)itemNumber,
        };

        NetworkManager.Instance.Send(swapWeaponItemPacket);
    }


    public void PlayerLootItem(int weaponItemNumber)
    {
        if (weaponItemNumber > _hasWeapon.Length)
            return;

        _hasWeapon[weaponItemNumber] = true;
    }

    public void HandHeldWeaponActive(bool weaponActive)
    {
        _weapons[(int)_handheldWeapon].SetActive(weaponActive);
    }

    public void WeaponActive(int itemNumber)
    {
        if (itemNumber > _weapons.Length)
            return;

        _weapons[(int)itemNumber].SetActive(true);
    }

}
