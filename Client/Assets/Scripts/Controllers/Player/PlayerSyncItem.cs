using Assets.Scripts.Controllers.Player;
using Google.Protobuf;
using UnityEngine;

[AddComponentMenu("Player/PlayerSyncItem")]
public class PlayerSyncItem : BasePlayerSyncController, ISyncObservable
{
    private ItemController _weapon;

    public void OnSync(IMessage packet)
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Weapon")
            _weapon = other.GetComponent<ItemController>();

        Debug.Log(other.name);
    }

    private void OnTriggerExit(Collider other)
    {

    }
}
