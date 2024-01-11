using UnityEngine;
using UnityEngine.InputSystem;

[AddComponentMenu("Player/PlayerInputController")]
public class PlayerInputController : PlayerEventController
{
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
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

    public void OnFire(InputValue value)
    {

    }

    public void OnSwap(InputValue value)
    {
        CallWeaponSwapEvent(value);
    }

    public void OnLootItem()
    {
        CallLootItemEvent();
    }
}
