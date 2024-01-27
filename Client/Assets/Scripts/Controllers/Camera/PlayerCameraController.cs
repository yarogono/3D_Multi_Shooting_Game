using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset;

    private Transform target;

    private void Start()
    {
        target = ObjectManager.Instance.MyPlayer.gameObject.transform;
    }

    void LateUpdate()
    {
        if (target != null)
            transform.position = target.position + offset;
    }
}
