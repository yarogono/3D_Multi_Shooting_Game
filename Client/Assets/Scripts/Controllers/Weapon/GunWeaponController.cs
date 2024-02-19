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
        GameObject instantBullet = Instantiate(_bullet, _bulletPos.position, _bulletPos.rotation);
        Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = _bulletPos.forward * 50;

        yield return null;

        GameObject instantCase = Instantiate(_bulletCase, _bulletCasePos.position, _bulletCasePos.rotation);
        Rigidbody caseRigid = instantCase.GetComponent<Rigidbody>();
        Vector3 caseVec = _bulletCasePos.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(2, 3);
        caseRigid.AddForce(caseVec, ForceMode.Impulse);
        caseRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);
    }

    public void OnSyncAttack()
    {
        
    }
}
