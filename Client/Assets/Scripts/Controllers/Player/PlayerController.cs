using Assets.Scripts.Controllers.Player;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerController pcCache;

    public PlayerController playerController
    {
        get
        {
#if UNITY_EDITOR
            // In the editor we want to avoid caching this at design time, so changes in PV structure appear immediately.
            if (!Application.isPlaying || this.pcCache == null)
            {
                this.pcCache = Get(this);
            }
#else
                if (this.pvCache == null)
                {
                    this.pvCache = PhotonView.Get(this);
                }
#endif
            return this.pcCache;
        }
    }

    public int Id { get; set; }

    public string Name { get; set; }

    private void Awake()
    {
        this.FindObservables();
    }


    public bool IsMine { get; private set; }

    public List<Component> ObservedComponents;


    public enum ObservableSearch { Manual, AutoFindActive, AutoFindAll }

    /// Default to manual so existing PVs in projects default to same as before. Reset() changes this to AutoAll for new implementations.
    public ObservableSearch observableSearch = ObservableSearch.Manual;

    public void FindObservables(bool force = false)
    {
        if (!force && this.observableSearch == ObservableSearch.Manual)
        {
            return;
        }

        if (this.ObservedComponents == null)
        {
            this.ObservedComponents = new List<Component>();
        }
        else
        {
            this.ObservedComponents.Clear();
        }

        this.transform.GetNestedComponentsInChildren<Component, ISyncObservable, PlayerController>(force || this.observableSearch == ObservableSearch.AutoFindAll, this.ObservedComponents);
    }



    public static PlayerController Get(Component component)
    {
        return component.transform.GetParentComponent<PlayerController>();
    }

    public static PlayerController Get(GameObject gameObj)
    {
        return gameObj.transform.GetParentComponent<PlayerController>();
    }
}
