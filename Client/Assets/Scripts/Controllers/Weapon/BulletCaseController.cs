using UnityEngine;

public class BulletCaseController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            Destroy(gameObject, 2f);
        }
    }
}
