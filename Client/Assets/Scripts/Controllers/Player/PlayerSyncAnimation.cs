using Google.Protobuf;
using Google.Protobuf.Protocol;
using UnityEngine;

namespace Assets.Scripts.Controllers.Player
{

    [AddComponentMenu("Player/PlayerSyncAnimation")]
    public class PlayerSyncAnimation : BasePlayerSyncController, ISyncObservable
    {
        private Animator _anim;

        private PlayerSyncTransform _syncTransform;

        private void Awake()
        {
            _anim = GetComponentInChildren<Animator>();
            _syncTransform = GetComponentInChildren<PlayerSyncTransform>();
        }


        public void OnSync(IMessage packet)
        {
            Debug.Log(packet);
        }

        private void Update()
        {
            if (this.playerController.IsMine == true)
            {
                UpdateMyPlayerAnimation();
            }
            else
            {
                UpdateEnemyPlayerAnimation();
            }

        }

        #region MyPlayer
        private void UpdateMyPlayerAnimation()
        {
            switch (_syncTransform.State)
            {
                case CreatureState.Moving:
                    UpdateMyPlayerMoving();
                    break;
                case CreatureState.Idle:
                    UpdateMyPlayerIdle();
                    break;
            }
        }

        private void UpdateMyPlayerIdle()
        {
            _anim.SetBool("isRun", false);
        }

        private void UpdateMyPlayerMoving()
        {
            Debug.Log(_syncTransform.IsPlayerWalk);
            if (_syncTransform.IsPlayerWalk == true)
            {
                _anim.SetBool("isWalk", true);
            }
            else
            {
                _anim.SetBool("isWalk", false);
                _anim.SetBool("isRun", true);
            }
        }
        #endregion


        #region Enemy Player
        private void UpdateEnemyPlayerAnimation()
        {
            
        }
        #endregion
    }
}
