using UnityEngine;
using UnityEngine.InputSystem;
using static Define;

[AddComponentMenu("Player/PlayerInputController")]
public class PlayerInputController : PlayerEventController
{
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        Keyboard keyboard = InputSystem.GetDevice<Keyboard>();
        if (keyboard.digit1Key.wasPressedThisFrame == true)
        {
            OnWeaponSwap(ItemNumber.One);
        }

        if (keyboard.digit2Key.wasPressedThisFrame == true)
        {
            OnWeaponSwap(ItemNumber.Two);
        }

        if (keyboard.digit3Key.wasPressedThisFrame == true)
        {
            OnWeaponSwap(ItemNumber.Three);
        }
    }


    public void OnMove(InputValue value)
    {
        Vector3 moveInput = value.Get<Vector3>().normalized;
        CallMoveEvent(moveInput);
    }

    public void OnLook(InputValue value)
    {
        Vector2 newAim = value.Get<Vector2>();
        Vector2 worldPos = _camera.ScreenToWorldPoint(newAim);
        newAim = (worldPos - (Vector2)transform.position).normalized;

        if (newAim.magnitude >= .9f)
        {
            CallLookEvent(newAim);
        }
    }

    public void OnWeaponSwap(ItemNumber itemNumber)
    {
        CallWeaponSwapEvent(itemNumber);
    }

    public void OnLootItem()
    {
        CallLootItemEvent();
    }

    public void OnAttack()
    {
        CallAttackEvent();
    }
}
