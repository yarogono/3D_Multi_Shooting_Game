using System.Collections;
using UnityEngine;

public class MeleeWeaponController : WeaponController, IAttackable
{
    [SerializeField] private BoxCollider _meleeArea;
    [SerializeField] private TrailRenderer _trailEffect;

    public void Attack()
    {
         StartCoroutine(MeleeWeaponSwing());
    }

    IEnumerator MeleeWeaponSwing()
    {
        yield return new WaitForSeconds(0.2f);
        _meleeArea.enabled = true;
        _trailEffect.enabled = true;

        yield return new WaitForSeconds(0.2f);
        _meleeArea.enabled = false;

        yield return new WaitForSeconds(0.3f);
        _trailEffect.enabled = false;
    }

    public void OnSyncAttack()
    {
        StartCoroutine(OnSyncMeleeWeaponSwing());
    }

    IEnumerator OnSyncMeleeWeaponSwing()
    {
        yield return new WaitForSeconds(0.2f);
        _trailEffect.enabled = true;

        yield return new WaitForSeconds(0.5f);
        _trailEffect.enabled = false;
    }
}
