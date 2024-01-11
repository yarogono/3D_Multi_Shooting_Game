using System;
using UnityEngine;
using UnityEngine.InputSystem;

[AddComponentMenu("Player/PlayerEventController")]
public class PlayerEventController : MonoBehaviour
{
    public event Action<Vector3> OnMoveEvent;
    public event Action<Vector3> OnLookEvent;
    public event Action<InputValue> OnWeaponSwapEvent;
    public event Action OnLootItemEvent;

    public void CallMoveEvent(Vector3 direction)
    {
        OnMoveEvent?.Invoke(direction);
    }

    public void CallLookEvent(Vector3 direction)
    {
        OnLookEvent?.Invoke(direction);
    }

    public void CallWeaponSwapEvent(InputValue value)
    {
        OnWeaponSwapEvent?.Invoke(value);
    }

    public void CallLootItemEvent()
    {
        OnLootItemEvent?.Invoke();
    }
}
