using UnityEngine;
using static Define;

public class WeaponItemController : MonoBehaviour
{
    [SerializeField] private WeaponItemType _type;
    [SerializeField] private int damage;
    [SerializeField] private float rate;
    [SerializeField] private BoxCollider meleeArea;
    [SerializeField] private TrailRenderer trailEffect;
}
