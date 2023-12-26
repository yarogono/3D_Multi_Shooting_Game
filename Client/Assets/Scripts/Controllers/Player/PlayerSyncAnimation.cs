using Google.Protobuf;
using UnityEngine;

namespace Assets.Scripts.Controllers.Player
{

    [AddComponentMenu("Player/PlayerSyncAnimation")]
    public class PlayerSyncAnimation : BasePlayerSyncController, ISyncObservable
    {
        private Animator _anim;

        private void Awake()
        {
            _anim = GetComponentInChildren<Animator>();
        }


        public void OnSync(IMessage message)
        {
            Debug.Log(message);
        }
    }
}
