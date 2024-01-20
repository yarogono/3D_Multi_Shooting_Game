using System;
using UnityEngine;
using static Define;

[AddComponentMenu("Player/PlayerEventController")]
public class PlayerEventController : MonoBehaviour
{
    public event Action<Vector3> OnMoveEvent;
    public event Action<Vector3> OnLookEvent;
    public event Action<ItemNumber> OnWeaponSwapEvent;
    public event Action OnLootItemEvent;
    public event Action OnAttackEvent;

    public void CallMoveEvent(Vector3 direction)
    {
        OnMoveEvent?.Invoke(direction);
    }

    public void CallLookEvent(Vector3 direction)
    {
        OnLookEvent?.Invoke(direction);
    }

    public void CallWeaponSwapEvent(ItemNumber itemNumber)
    {
        OnWeaponSwapEvent?.Invoke(itemNumber);
    }

    public void CallLootItemEvent()
    {
        OnLootItemEvent?.Invoke();
    }
    
    public void CallAttackEvent()
    {
        OnAttackEvent?.Invoke();
    }
}
