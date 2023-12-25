using Google.Protobuf;
using UnityEngine;

namespace Assets.Scripts.Controllers.Player
{
    public class PlayerSyncAnimation : MonoBehaviour, ISyncObservable
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
