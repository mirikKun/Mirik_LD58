using System;
using System.Collections.Generic;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.DataDriven.Effects;
using Assets.Code.GamePlay.Physic.ColliderLogic;
using UnityEngine;
using UnityEngine.VFX;

namespace Assets.Code.GamePlay.Enemies.EnemyController.Combat
{
    [Serializable]
    public class EnemyMeleeAttack:IEnemyAttack
    {
        [SerializeField] private string _attackId;
        [SerializeField] private VisualEffect _attackVfx;
        [SerializeField] private SimpleAttackTrigger _simpleAttackTrigger;
        [SerializeReference] private List<Effect> _effects;


        public string AttackId => _attackId;

        public void Init(ActorEntity casterEntity)
        {
            _simpleAttackTrigger.Init(casterEntity);
            _simpleAttackTrigger.SetEffects(_effects);
        }
        public void Attack(Vector3 target,Vector3 direction)
        {
            _attackVfx?.Play();
            
            _simpleAttackTrigger.Reset();
            _simpleAttackTrigger.gameObject.SetActive(true);
            StopAttack();
        }
        public async void StopAttack()
        {
            
            await Awaitable.WaitForSecondsAsync(0.2f);
            _simpleAttackTrigger.gameObject.SetActive(false);
        }
    }
}