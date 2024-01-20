using System.Collections;
using UnityEngine;

public class MeleeWeaponController : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _rate;
    [SerializeField] private BoxCollider _meleeArea;
    [SerializeField] private TrailRenderer _trailEffect;

    public float Rate
    {
        get => _rate;
    }

    public void WeaponAttack()
    {
        StartCoroutine(MeleeWeaponSwing());
    }

    IEnumerator MeleeWeaponSwing()
    {
        yield return new WaitForSeconds(0.1f);
        _meleeArea.enabled = true;
        _trailEffect.enabled = true;

        yield return new WaitForSeconds(0.2f);
        _meleeArea.enabled = false;

        yield return new WaitForSeconds(0.3f);
        _trailEffect.enabled = false;
    }
}
