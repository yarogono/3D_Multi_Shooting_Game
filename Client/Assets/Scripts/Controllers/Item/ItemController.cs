using UnityEngine;

public class ItemController : MonoBehaviour
{
    public enum Type { Ammo, Coin, Grenade, Heart, Weapon };
    public Type type;
    public int value;

    private void Update()
    {
        transform.Rotate(Vector3.up * 10 * Time.deltaTime);
    }
}