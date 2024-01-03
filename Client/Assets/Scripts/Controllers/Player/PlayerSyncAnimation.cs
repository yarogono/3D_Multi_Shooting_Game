using Google.Protobuf;
using UnityEngine;

namespace Assets.Scripts.Controllers.Player
{

    [AddComponentMenu("Player/PlayerSyncAnimation")]
    public class PlayerSyncAnimation : BasePlayerSyncController, ISyncObservable
    {
        private Animator _anim;

        private bool _isWalk;

        private void Awake()
        {
            _anim = GetComponentInChildren<Animator>();
        }


        public void OnSync(IMessage packet)
        {
            Debug.Log(packet);
        }

        private void Update()
        {
        }
    }
}
