using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private int _damage;

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