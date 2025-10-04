using System;
using System.Collections.Generic;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.DataDriven.Effects;
using UnityEngine;
using UnityEngine.VFX;

namespace Assets.Code.GamePlay.Enemies.EnemyController.Combat
{
    [Serializable]
    public class EnemyRoarAttack:IEnemyAttack
    {
        [SerializeField] private string _attackId;
        [SerializeField] private List<VisualEffect> _vfxEffects;
        [SerializeField] private Collider _attackCollider;
        [SerializeReference] private List<Effect> _effects;


        public string AttackId => _attackId;

        public void Init(ActorEntity actorEntity)
        {
            //_attackTrigger.SetDamage(_damage);
        }

        public void Attack(Vector3 target, Vector3 direction)
        {
            foreach (var vfxEffect in _vfxEffects)
            {
                vfxEffect.Play();
            }
        }
        public void StopAttack()
        {
            foreach (var vfxEffect in _vfxEffects)
            {
                vfxEffect.Stop();
            }
        }
    }
}