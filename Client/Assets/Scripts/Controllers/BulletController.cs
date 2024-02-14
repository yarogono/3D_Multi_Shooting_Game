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
        if (other.CompareTag("Floor"))
        {
            Destroy(gameObject, 3f);
        }
        else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}