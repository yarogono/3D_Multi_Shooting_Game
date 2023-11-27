using System;
using UnityEngine;

public class CharacterEventController : MonoBehaviour
{
    public event Action<Vector3> OnMoveEvent;
    public event Action<Vector3> OnLookEvent;

    public void CallMoveEvent(Vector3 direction)
    {
        OnMoveEvent?.Invoke(direction);
    }

    public void CallLookEvent(Vector3 direction)
    {
        OnLookEvent?.Invoke(direction);
    }
}
