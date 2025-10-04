using System;
using System.Collections.Generic;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.DataDriven.Effects;
using Assets.Code.GamePlay.Physic.ColliderLogic;
using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController.Combat
{
    [Serializable]
    public class EnemyOnslaughtAttack:IEnemyAttack
    {
        [SerializeField] private string _attackId;
        [SerializeField] private GameObject _vfxObject;
        [SerializeField] private Collider _attackCollider;
        [SerializeReference] private List<Effect> _effects;
        [SerializeField] private SimpleAttackTrigger _simpleAttackTrigger;


        public string AttackId => _attackId;

        public void Init(ActorEntity casterEntity)
        {
            _simpleAttackTrigger.Init(casterEntity);
            _simpleAttackTrigger.SetEffects(_effects);
        }

        public void Attack(Vector3 target, Vector3 direction)
        {
            _vfxObject.SetActive(true);
            
            _simpleAttackTrigger.Reset();
            _simpleAttackTrigger.gameObject.SetActive(true);
        }
        public void StopAttack()
        {
            _vfxObject.SetActive(false);
            
            _simpleAttackTrigger.gameObject.SetActive(false);

        }
    }
}