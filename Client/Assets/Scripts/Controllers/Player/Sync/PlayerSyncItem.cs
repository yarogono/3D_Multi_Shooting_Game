using Assets.Scripts.Controllers.Player;
using Google.Protobuf;
using Google.Protobuf.Protocol;
using UnityEngine;
using static Define;

[AddComponentMenu("Player/PlayerSyncItem")]
public class PlayerSyncItem : BasePlayerSyncController, ISyncObservable
{
    [SerializeField] private GameObject[] _weapons;
    [SerializeField] private bool[] _hasWeapon;

    private ItemNumber _handheldWeapon;

    private DropItemController _nearDropItem;

    private PlayerInputController _inputController;
    private PlayerSyncAnimation _playerSyncAnimation;

    private bool _isLootPopUpOpen = false;

    public ItemNumber HandHeldWeapon 
    { 
        get => _handheldWeapon; 
    }

    public bool[] HasWeapon
    {
        get => _hasWeapon;
    }
    
    public GameObject MeleeWeaponGameObject
    {
        get => _weapons[(int)ItemNumber.One];
    }

    private void Awake()
    {
        _inputController = GetComponent<PlayerInputController>();
        _playerSyncAnimation = GetComponent<PlayerSyncAnimation>();
        _handheldWeapon = ItemNumber.None;
    }

    private void Start()
    {
        if (playerController.IsMine)
        {
            _inputController.OnWeaponSwapEvent += WeaponSwap;
            _inputController.OnLootItemEvent += LootItem;
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
            _weapons[(int)_handheldWeapon].SetActive(false);

        GameObject weaponItem = _weapons[(int)itemNumber];
        weaponItem.SetActive(true);
        _handheldWeapon = itemNumber;
        _playerSyncAnimation.WeaponSwapAnimation();

        SendSwapWeaponItemPacket(itemNumber);
    }

    private void SendSwapWeaponItemPacket(ItemNumber itemNumber)
    {
        C_SwapWeaponItem swapWeaponItemPacket = new C_SwapWeaponItem()
        {
            WeaponItemNumber = (int)itemNumber,
        };

        NetworkManager.Instance.Send(swapWeaponItemPacket);
    }


    #region OnSync
    public void OnSync(IMessage packet)
    {

        switch (packet)
        {
            case S_SwapWeaponItem:
                OnSyncSwapWeaponItem(packet);
                break;
            case S_LootItem:
                OnSyncLootItem(packet);
                break;
        }
    }

    private void OnSyncLootItem(IMessage packet)
    {
        if (packet == null)
            return;

        S_LootItem lootItem = (S_LootItem)packet;

        int itemId = lootItem.ItemId;
        int weaponItemNumber = lootItem.WeaponItemNumber;

        GameObject itemGameObject = ObjectManager.Instance.FindById(itemId);

        _hasWeapon[weaponItemNumber] = true;
        Destroy(itemGameObject);
    }

    private void OnSyncSwapWeaponItem(IMessage packet)
    {
        if (packet == null)
            return;

        S_SwapWeaponItem swapWeaponItem = (S_SwapWeaponItem)packet;
        int itemNumber = swapWeaponItem.WeaponItemNumber;

        if (_hasWeapon[itemNumber] == false)
            return;

        if (itemNumber > _weapons.Length)
            return;

        if (_handheldWeapon != ItemNumber.None)
            _weapons[(int)_handheldWeapon].SetActive(false);

        _weapons[(int)itemNumber].SetActive(true);
        _handheldWeapon = (ItemNumber)itemNumber;
    }

    public void LootItem()
    {
        if (_nearDropItem != null)
        {
            int weaponIndex = _nearDropItem.Value;
            _hasWeapon[weaponIndex] = true;

            SendLootItemPacket(_nearDropItem.Id, weaponIndex);

            UIManager.Instance.ClosePopupUI();

            Destroy(_nearDropItem.gameObject);
        }
    }

    private void SendLootItemPacket(int itemId, int weaponIndex)
    {
        C_LootItem lootItemPacket = new C_LootItem()
        {
            ItemId = itemId,
            WeaponItemNumber = weaponIndex
        };

        NetworkManager.Instance.Send(lootItemPacket);
    }
    #endregion


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Structure"))
            return;

        if (_isLootPopUpOpen == false)
        {
            UI_Loot lootUI = UIManager.Instance.ShowPopupUI<UI_Loot>("LootUI");
            _isLootPopUpOpen = true;

            lootUI.ShowLootText(other.gameObject.name);
        }

        if (_nearDropItem == null)
        {
            _nearDropItem = other.gameObject.GetComponent<DropItemController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        UIManager.Instance.ClosePopupUI();
        _isLootPopUpOpen = false;
        _nearDropItem = null;
    }
}
