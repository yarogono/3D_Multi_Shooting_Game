﻿using Google.Protobuf;
using Google.Protobuf.Protocol;
using UnityEngine;
using static Define;

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

        public void WeaponSwapAnimation()
        {
            _anim.SetTrigger("doSwap");
        }

        public void WeaponAttackSwingAnimation()
        {
            _anim.SetTrigger("doSwing");
        }

        #region OnSync
        public void OnSync(IMessage packet)
        {
            switch (packet)
            {
                case S_SwapWeaponItem:
                    WeaponSwapAnimation();
                    break;
            }
        }
        #endregion

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
