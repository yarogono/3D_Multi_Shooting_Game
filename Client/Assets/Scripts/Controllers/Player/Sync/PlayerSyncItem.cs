using Assets.Scripts.Controllers.Player;
using Google.Protobuf;
using Google.Protobuf.Protocol;
using UnityEngine;
using static Define;

[AddComponentMenu("Player/PlayerSyncItem")]
public class PlayerSyncItem : BasePlayerSyncController, ISyncObservable
{
    private DropItemController _nearDropItem;

    private PlayerInputController _inputController;
    private PlayerWeaponController _playerWeaponController;

    private bool _isLootPopUpOpen = false;

    private void Awake()
    {
        _inputController = GetComponent<PlayerInputController>();
        _playerWeaponController = GetComponent<PlayerWeaponController>();
    }

    private void Start()
    {
        if (playerController.IsMine)
        {
            _inputController.OnLootItemEvent += LootItem;
        }
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

        _playerWeaponController.PlayerLootItem(weaponItemNumber);
        Destroy(itemGameObject);
    }

    private void OnSyncSwapWeaponItem(IMessage packet)
    {
        if (packet == null)
            return;

        S_SwapWeaponItem swapWeaponItem = (S_SwapWeaponItem)packet;
        int itemNumber = swapWeaponItem.WeaponItemNumber;

        if (_playerWeaponController.HasWeapon[itemNumber] == false)
            return;

        if (itemNumber > _playerWeaponController.Weapons.Length)
            return;

        if (_playerWeaponController.HandHeldWeapon != ItemNumber.None)
            _playerWeaponController.HandHeldWeaponActive(false);

        _playerWeaponController.HandHeldWeapon = (ItemNumber)itemNumber;
        _playerWeaponController.WeaponActive(itemNumber);
        _playerWeaponController.SwapWeaponAttack();

    }
    #endregion

    public void LootItem()
    {
        if (_nearDropItem != null)
        {
            int weaponIndex = _nearDropItem.Value;
            _playerWeaponController.PlayerLootItem(weaponIndex);

            SendLootItemPacket(_nearDropItem.Id, weaponIndex);

            UIManager.Instance.CloseLootUI();

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


    private void OnTriggerStay(Collider other)
    {
        if (_isLootPopUpOpen == false && other.CompareTag("Weapon"))
        {
            UI_Loot lootUI = UIManager.Instance.ShowLootUI("LootUI");
            _isLootPopUpOpen = true;

            lootUI.ShowLootText(other.gameObject.name);
        }

        if (_nearDropItem == null && other.CompareTag("Weapon"))
        {
            _nearDropItem = other.gameObject.GetComponent<DropItemController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            UIManager.Instance.CloseLootUI();
            _isLootPopUpOpen = false;
            _nearDropItem = null;
        }
    }
}
