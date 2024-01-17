using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset;

    private Transform target;

    public void TargetSetting(Transform target)
    {
        this.target = target;
    }

    void Update()
    {
        if (target != null)
            transform.position = target.position + offset;
    }
}
