using System.Collections;
using UnityEngine;
using static Define;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private BoxCollider _meleeArea;
    [SerializeField] private TrailRenderer _trailEffect;

    public void WeaponAttack(WeaponType weaponType)
    {
        switch (weaponType)
        {
            case WeaponType.Melee:
                StartCoroutine(MeleeWeaponSwing());
                break;
            case WeaponType.HandGun:
                break;
            case WeaponType.SubMachineGun:
                break;
        }
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

    public void OnSyncWeaponAttack()
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
