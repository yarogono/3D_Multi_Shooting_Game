using Google.Protobuf;
using Google.Protobuf.Protocol;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Controllers.Player
{

    [AddComponentMenu("Player/PlayerSyncAnimation")]
    public class PlayerSyncAnimation : BasePlayerSyncController, ISyncObservable
    {
        private Animator _anim;
        private PlayerSyncTransform _syncTransform;
        private MeshRenderer[] _meshs;


        private void Awake()
        {
            _syncTransform = GetComponent<PlayerSyncTransform>();
            _anim = GetComponentInChildren<Animator>();
            _meshs = GetComponentsInChildren<MeshRenderer>();
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

        public void GetDamageAnimation()
        {
            StartCoroutine(GetDamageAnimationCo());
        }

        private IEnumerator GetDamageAnimationCo()
        {
            if (playerController.Hp <= 0)
            {
                foreach (MeshRenderer mesh in _meshs)
                    mesh.material.color = Color.gray;

                yield break;
            }

            foreach (MeshRenderer mesh in _meshs)
                mesh.material.color = Color.red;

            yield return new WaitForSeconds(0.1f);

            foreach (MeshRenderer mesh in _meshs)
                mesh.material.color = Color.white;
        }

        public void WeaponSwapAnimation()
        {
            _anim.SetTrigger("doSwap");
        }

        public void WeaponAttackSwingAnimation()
        {
            _anim.SetTrigger("doSwing");
        }

        public void WeaponGunAttackAnimation()
        {
            _anim.SetTrigger("doShoot");
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
            switch (playerController.State)
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
            switch (playerController.State)
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
