using System;
using System.Collections;
using UnityEngine;

public class GunWeaponController : WeaponController, IAttackable
{
    [SerializeField] private Transform _bulletPos;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletCasePos;
    [SerializeField] private GameObject _bulletCase;

    public void Attack()
    {
        StartCoroutine(GunAttack());
    }

    private IEnumerator GunAttack()
    {
        yield return null;
    }

    public void OnSyncAttack()
    {
        throw new NotImplementedException();
    }
}
