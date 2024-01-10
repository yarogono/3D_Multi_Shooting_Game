using Assets.Scripts.Controllers.Player;
using Google.Protobuf;
using UnityEngine;

[AddComponentMenu("Player/PlayerSyncItem")]
public class PlayerSyncItem : BasePlayerSyncController, ISyncObservable
{
    private ItemController _weapon;
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
        }
    }


    private void WeaponSwap()
    {
        Debug.Log("WeaponSwap");
    }

    public void OnSync(IMessage packet)
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerStay(Collider other)
    {
        if (_isLootPopUpOpen == false)
        {
            UIManager.Instance.ShowPopupUI<UI_Popup>("LootUI");
            _isLootPopUpOpen = true;
        }

        if (other.tag == "Weapon")
            _weapon = other.GetComponent<ItemController>();

    }

    private void OnTriggerExit(Collider other)
    {
        UIManager.Instance.ClosePopupUI();
        _isLootPopUpOpen = false;
    }
}
