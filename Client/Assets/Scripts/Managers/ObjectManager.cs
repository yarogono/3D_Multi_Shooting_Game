using Google.Protobuf.Protocol;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : CustomSingleton<ObjectManager>
{
    public MyPlayerController MyPlayer { get; set; }
    readonly Dictionary<int, GameObject> _objects = new();

    public static GameObjectType GetObjectTypeById(int id)
    {
        int type = (id >> 24) & 0x7F;
        return (GameObjectType)type;
    }

    public void Add(ObjectInfo info, bool isMyPlayer = false)
    {
        GameObjectType objectType = GetObjectTypeById(info.ObjectId);

        if (objectType == GameObjectType.Player)
        {
            if (isMyPlayer)
            {
                GameObject gameObject = ResourceManager.Instance.Instantiate("MyPlayer");

                gameObject.name = info.Name;
                _objects.Add(info.ObjectId, gameObject);

                MyPlayerController MyPlayer = gameObject.GetComponent<MyPlayerController>();
                MyPlayer.Id = info.ObjectId;
                MyPlayer.Name = info.Name;
                MyPlayer.PosInfo = info.PosInfo;
                MyPlayer.StatInfo = info.StatInfo;
            }
            else
            {
                GameObject gameObject = ResourceManager.Instance.Instantiate("EnemyPlayer");

                _objects.Add(info.ObjectId, gameObject);

                EnemyPlayerController enemyPlayer = gameObject.GetComponent<EnemyPlayerController>();
                enemyPlayer.Id = info.ObjectId;
                enemyPlayer.Name = info.Name;
                enemyPlayer.PosInfo = info.PosInfo;
            }
        }
    }

    public void Remove(int id)
    {
        GameObject gameObject = FindById(id);
        if (gameObject == null)
            return;

        _objects.Remove(id);
        Destroy(gameObject);
    }

    public GameObject FindById(int id)
    {
        _objects.TryGetValue(id, out GameObject gameObject);
        return gameObject;
    }

    public void Clear()
    {
        foreach (GameObject gameObject in _objects.Values)
        {
            Destroy(gameObject);
        }

        _objects.Clear();
        MyPlayer = null;
    }
}
