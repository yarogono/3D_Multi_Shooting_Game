using Assets.Scripts.Controllers.Player;
using Google.Protobuf;
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

        if (_handheldWeapon != null)
            _weapons[(int)_handheldWeapon].active = false;

        _weapons[(int)itemNumber].active = true;
        _handheldWeapon = itemNumber;
    }

    public void OnSync(IMessage packet)
    {
        throw new System.NotImplementedException();
    }

    public void LootItem()
    {
        if (_nearItemObject != null)
        {
            ItemController item = _nearItemObject.GetComponent<ItemController>();
            int weaponIndex = item.Value;
            _hasWeapon[weaponIndex] = true;

            Destroy(_nearItemObject);
        }
    }

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
