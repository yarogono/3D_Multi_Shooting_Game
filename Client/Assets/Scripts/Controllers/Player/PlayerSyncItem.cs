using Assets.Scripts.Controllers.Player;
using Google.Protobuf;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

[AddComponentMenu("Player/PlayerSyncItem")]
public class PlayerSyncItem : BasePlayerSyncController, ISyncObservable
{
    [SerializeField] private GameObject[] _weapons;
    [SerializeField] private bool[] _hasWeapon;

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


    private void WeaponSwap(InputValue value)
    {
        // ToDo : 입력 바인딩 시스템 구축해서 아이템 번호에 맞춰서 Swap
        Debug.Log($"WeaponSwap {value}");
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
