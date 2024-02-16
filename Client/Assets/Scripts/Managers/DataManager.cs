using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager : CustomSingleton<DataManager>
{
    public Dictionary<int, Data.Weapon> WeaponDict { get; private set; } = new Dictionary<int, Data.Weapon>();

    public void Init()
    {
        WeaponDict = LoadJson<Data.WeaponData, int, Data.Weapon>("WeaponData").MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = ResourceManager.Instance.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
