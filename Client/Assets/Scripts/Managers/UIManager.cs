using Assets.Scripts.UI.Scene;
using UnityEngine;

public class UIManager : CustomSingleton<UIManager>
{
    UI_Popup _popup = null;
    UI_Scene _sceneUI = null;

    UI_Loot _loot = null;

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };
            return root;
        }
    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = ResourceManager.Instance.Instantiate($"UI/Scene/{name}");
        T sceneUI = Util.GetOrAddComponent<T>(go);
        _sceneUI = sceneUI;

        go.transform.SetParent(Root.transform);

        return sceneUI;
    }

    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = ResourceManager.Instance.Instantiate($"UI/Popup/{name}");
        T popup = Util.GetOrAddComponent<T>(go);
        _popup = popup;

        go.transform.SetParent(Root.transform);

        return popup;
    }

    public void ClosePopupUI()
    {
        if (_popup == null)
            return;

        ResourceManager.Instance.Destroy(_popup.gameObject);
        _popup = null;
    }

    public UI_Loot ShowLootUI(string name = null)
    {
        if (_loot != null)
        {
            _loot.gameObject.SetActive(true);
            return _loot;
        }

        if (string.IsNullOrEmpty(name))
            name = typeof(UI_Loot).Name;

        GameObject go = ResourceManager.Instance.Instantiate($"UI/Popup/{name}");
        UI_Loot loot = Util.GetOrAddComponent<UI_Loot>(go);
        _loot = loot;

        go.transform.SetParent(Root.transform);

        return _loot;
    }

    public void CloseLootUI()
    {
        if (_loot == null)
            return;

        _loot.gameObject.SetActive(false);
    }
}