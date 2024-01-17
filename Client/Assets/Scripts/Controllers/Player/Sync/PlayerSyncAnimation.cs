using Google.Protobuf;
using Google.Protobuf.Protocol;
using System;
using UnityEngine;
using static Define;

namespace Assets.Scripts.Controllers.Player
{

    [AddComponentMenu("Player/PlayerSyncAnimation")]
    public class PlayerSyncAnimation : BasePlayerSyncController, ISyncObservable
    {
        private Animator _anim;

        private PlayerSyncTransform _syncTransform;

        private PlayerInputController _inputController;
        private PlayerSyncItem _playerSyncItem;

        private void Awake()
        {
            _anim = GetComponentInChildren<Animator>();
            _syncTransform = GetComponentInChildren<PlayerSyncTransform>();
            _inputController = GetComponent<PlayerInputController>();
            _playerSyncItem = GetComponentInChildren<PlayerSyncItem>();
        }

        private void Start()
        {
            if (playerController.IsMine)
            {
                _inputController.OnWeaponSwapEvent += WeaponSwapAnimation;
            }
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

        private void WeaponSwapAnimation(ItemNumber itemNumber)
        {
            if (itemNumber == _playerSyncItem.HandHeldWeapon)
                return;

            if (_playerSyncItem.HasWeapon[(int)itemNumber] == false)
                return;

            _anim.SetTrigger("doSwap");
        }

        #region OnSync
        public void OnSync(IMessage packet)
        {
            switch (packet)
            {
                case S_SwapWeaponItem:
                    OnSyncWeaponSwapAnimation();
                    break;
            }
        }

        private void OnSyncWeaponSwapAnimation()
        {
            _anim.SetTrigger("doSwap");
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
