using UnityEngine;

public class BasePlayerSyncController : MonoBehaviour
{
    private PlayerController _playerControllerCache;

    public PlayerController playerController
    {
        get
        {
#if UNITY_EDITOR
            // In the editor we want to avoid caching this at design time, so changes in PV structure appear immediately.
            if (!Application.isPlaying || this._playerControllerCache == null)
            {
                this._playerControllerCache = PlayerController.Get(this);
            }
#else
            if (this._playerControllerCache == null)
            {
                this._playerControllerCache = PlayerController.Get(this);
            }
#endif
            return this._playerControllerCache;
        }
    }
}
