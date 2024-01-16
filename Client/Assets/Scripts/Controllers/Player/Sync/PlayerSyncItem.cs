using Assets.Scripts.Controllers.Player;
using Google.Protobuf;
using Google.Protobuf.Protocol;
using System;
using UnityEngine;
using static Define;

[AddComponentMenu("Player/PlayerSyncItem")]
public class PlayerSyncItem : BasePlayerSyncController, ISyncObservable
{
    [SerializeField] private GameObject[] _weapons;
    [SerializeField] private bool[] _hasWeapon;

    private ItemNumber _handheldWeapon;

    private GameObject _nearItemObject;

    private PlayerInputController _inputController;
    private bool _isLootPopUpOpen = false;

    private void Awake()
    {
        _inputController = GetComponent<PlayerInputController>();
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
            _weapons[(int)_handheldWeapon].active = false;

        GameObject weaponItem = _weapons[(int)itemNumber];
        weaponItem.active = true;
        _handheldWeapon = itemNumber;

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
            _weapons[(int)_handheldWeapon].active = false;

        _weapons[(int)itemNumber].active = true;
        _handheldWeapon = (ItemNumber)itemNumber;
    }

    public void LootItem()
    {
        if (_nearItemObject != null)
        {
            ItemController item = _nearItemObject.GetComponent<ItemController>();
            int weaponIndex = item.Value;
            _hasWeapon[weaponIndex] = true;

            SendLootItemPacket(item.Id, weaponIndex);

            Destroy(_nearItemObject);
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
        if (_isLootPopUpOpen == false)
        {
            UI_Loot lootUI = UIManager.Instance.ShowPopupUI<UI_Loot>("LootUI");
            _isLootPopUpOpen = true;

            lootUI.ShowLootText(other.gameObject.name);
        }

        if (_nearItemObject == null)
        {
            _nearItemObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        UIManager.Instance.ClosePopupUI();
        _isLootPopUpOpen = false;
        _nearItemObject = null;
    }
}
