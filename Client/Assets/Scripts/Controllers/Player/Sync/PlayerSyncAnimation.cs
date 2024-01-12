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
                case CreatureState.Idle:
                    UpdateMyPlayerIdle();
                    break;
                case CreatureState.Moving:
                    UpdateMyPlayerMoving();
                    break;
            }
        }

        private void UpdateMyPlayerIdle()
        {
            _anim.SetFloat("MoveSpeed", _syncTransform.MoveSpeed);
        }

        private void UpdateMyPlayerMoving()
        {
            _anim.SetFloat("MoveSpeed", _syncTransform.MoveSpeed);
        }
        #endregion


        #region Enemy Player
        private void UpdateEnemyPlayerAnimation()
        {
            switch (_syncTransform.State)
            {
                case CreatureState.Idle:
                    UpdateEnemyPlayerIdle();
                    break;
                case CreatureState.Moving:
                    UpdateEnemyPlayerMoving();
                    break;
            }
        }

        private void UpdateEnemyPlayerIdle()
        {
            _anim.SetFloat("MoveSpeed", _syncTransform.MoveSpeed);
        }

        private void UpdateEnemyPlayerMoving()
        {
            _anim.SetFloat("MoveSpeed", _syncTransform.MoveSpeed);
        }
        #endregion
    }
}
