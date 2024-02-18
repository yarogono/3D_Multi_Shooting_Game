using UnityEngine;
using static Define;

public class WeaponController : MonoBehaviour
{
    private int _weaponId;
    private WeaponType _weaponType;

    public int WeaponId
    {
        get => _weaponId;
        set => _weaponId = value;
    }

    public WeaponType WeaponType
    {
        get => _weaponType;
        set => _weaponType = value;
    }
}
