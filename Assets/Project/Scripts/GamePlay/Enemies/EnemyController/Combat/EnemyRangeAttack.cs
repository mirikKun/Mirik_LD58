using System;
using System.Collections.Generic;
using Assets.Code.GamePlay.Armaments;
using Assets.Code.GamePlay.Armaments.Projectiles.Enums;
using Assets.Code.GamePlay.Armaments.Projectiles.Factories;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.DataDriven.Effects;
using UnityEngine;
using Zenject;

namespace Assets.Code.GamePlay.Enemies.EnemyController.Combat
{
    [Serializable]
    public class EnemyRangeAttack : IEnemyAttack
    {
        [SerializeField] private string _attackId;
        [SerializeField] private float _projectileSpeed;
        [SerializeField] private Transform _attackSpawnPoint;
        [SerializeField] private ArmamentType _armamentType;
        [SerializeReference] private List<Effect> _effects;
        private IArmamentsFactory _armamentsFactory;
        private ActorEntity _casterEntity;


        public string AttackId => _attackId;

        [Inject]
        private void Construct(IArmamentsFactory armamentsFactory)
        {
            _armamentsFactory = armamentsFactory;
        }

        public void Init(ActorEntity casterEntity)
        {
            _casterEntity = casterEntity;
        }

        public void Attack(Vector3 target, Vector3 direction)
        {
            Vector3 directionToTarget = target - _attackSpawnPoint.position;
            Armament armament = _armamentsFactory.CreateArmament(_armamentType, _attackSpawnPoint.position, Quaternion.LookRotation(directionToTarget),null);
            
            // armament.Init(_casterEntity, _effects,)
            //     .With(new LifetimeArmamentBehaviour())
            //     .With(new MovingArmamentBehaviour(_projectileSpeed,directionToTarget))
            //     .StartBehaviours();
        }
    }
}