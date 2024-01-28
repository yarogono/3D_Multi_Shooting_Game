using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] private Transform _healthBar;
    
    private Transform _camTransform;

    public Transform CamTransform
    {
        set
        {
            if (_camTransform != null)
                return;

            _camTransform = value;
        }
    }

    private void Start()
    {
        CameraSetting();
    }

    void LateUpdate()
    {
        if (_camTransform != null)
        {
            _healthBar.forward = -_camTransform.forward;
        }
    }

    public void CameraSetting()
    {
        PlayerCameraController cameraController = ObjectManager.Instance.MainCamera;
        this.CamTransform = cameraController.transform;
    }
} 
