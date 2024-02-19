using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private int _damage;

    private int _shooterId;

    public int ShooterId
    {
        get => _shooterId;
        set => _shooterId = value;
    }

    public int Damage 
    { 
        get => _damage; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}