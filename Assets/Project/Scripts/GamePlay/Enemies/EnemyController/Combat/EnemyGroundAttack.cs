using System;
using System.Collections.Generic;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.DataDriven.Effects;
using Assets.Code.GamePlay.Physic.ColliderLogic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Code.GamePlay.Enemies.EnemyController.Combat
{
    [Serializable]
    public class EnemyGroundAttack:IEnemyAttack
    {
        [SerializeField] private string _attackId;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private Transform _particleSpawnPoint;
        [SerializeReference] private List<Effect> _effects;
        [SerializeField] private SimpleAttackTrigger _simpleAttackTrigger;


        public void Init(ActorEntity casterEntity)
        {
            _simpleAttackTrigger.Init(casterEntity);
            _simpleAttackTrigger.SetEffects(_effects);
            
        }
        

        public string AttackId => _attackId;

     

        public void Attack(Vector3 target, Vector3 direction)
        {
            ParticleSystem particleSystem = Object.Instantiate(_particleSystem, _particleSpawnPoint.position, Quaternion.identity);
            particleSystem.Play();
            Object.Destroy(particleSystem.gameObject, particleSystem.main.duration);
            
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