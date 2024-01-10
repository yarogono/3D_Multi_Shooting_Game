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
            UI_Loot lootUI = UIManager.Instance.ShowPopupUI<UI_Loot>("LootUI");
            _isLootPopUpOpen = true;

            lootUI.ShowLootText(other.gameObject.name);
        }

        if (other.tag == "Weapon")
            _weapon = other.GetComponent<ItemController>();


        // ToDo : 키보드 E 입력 시 아이템 먹기 => 캐릭터 UI에 반영
        if (Input.GetKeyDown(KeyCode.E))
            Debug.Log("EEEE");
    }

    private void OnTriggerExit(Collider other)
    {
        UIManager.Instance.ClosePopupUI();
        _isLootPopUpOpen = false;
    }
}
