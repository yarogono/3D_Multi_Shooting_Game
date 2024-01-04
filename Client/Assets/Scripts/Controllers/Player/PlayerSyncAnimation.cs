using Google.Protobuf;
using Google.Protobuf.Protocol;
using System;
using UnityEngine;

namespace Assets.Scripts.Controllers.Player
{

    [AddComponentMenu("Player/PlayerSyncAnimation")]
    public class PlayerSyncAnimation : BasePlayerSyncController, ISyncObservable
    {
        private Animator _anim;

        private PlayerSyncTransform _syncTransform;

        private bool _isWalk;

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
            _anim.SetBool("isWalk", false);
        }

        private void UpdateMyPlayerMoving()
        {
            if (_syncTransform.IsPlayerWalk == true)
            {
                _anim.SetBool("isWalk", true);
                _anim.SetBool("isRun", false);
            }
            else
            {
                _anim.SetBool("isRun", true);
                _anim.SetBool("isWalk", false);
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
