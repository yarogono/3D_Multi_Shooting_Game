using System;
using UnityEngine;

[AddComponentMenu("Player/PlayerEventController")]
public class PlayerEventController : MonoBehaviour
{
    public event Action<Vector3> OnMoveEvent;
    public event Action<Vector3> OnLookEvent;
    public event Action OnWeaponSwapEvent;

    public void CallMoveEvent(Vector3 direction)
    {
        OnMoveEvent?.Invoke(direction);
    }

    public void CallLookEvent(Vector3 direction)
    {
        OnLookEvent?.Invoke(direction);
    }

    public void CallWeaponSwapEvent()
    {
        OnWeaponSwapEvent?.Invoke();
    }
}
