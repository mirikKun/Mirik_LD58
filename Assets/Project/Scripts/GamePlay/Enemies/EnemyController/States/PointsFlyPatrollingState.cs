using System.Collections.Generic;
using System.Linq;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs;
using Assets.Code.GamePlay.GameplayStateMachine;
using ImprovedTimers;
using Project.Scripts.Utils.Extensions;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Code.GamePlay.Enemies.EnemyController.States
{
    public class PointsFlyPatrollingState:IState
    {
        private readonly ActorEntity _enemy;
        private readonly PointsFlyPatrollingStateConfig _patrollingConfig;
        private EnemyMover Mover => _enemy.Get<EnemyMover>();
        private EnemyCombat Combat => _enemy.Get<EnemyCombat>();
        private List<Vector3> _patrolPoints;
        private Vector3 _targetPosition;
        public PointsFlyPatrollingState(ActorEntity enemy, PointsFlyPatrollingStateConfig patrollingConfig)
        {
            _enemy = enemy;
            _patrollingConfig = patrollingConfig;
            _patrolPoints=_patrollingConfig.PatrolPoints.Select(x=>x.position).ToList();
        }

        public void OnEnter()
        {
            _targetPosition = _patrolPoints.PickRandom(_targetPosition);

        }

        public void Update(float deltaTime)
        {
            float speed= _patrollingConfig.Speed.RandomValueInRange;
            float rotationSpeed= _patrollingConfig.RotationSpeed.RandomValueInRange;
            
            Vector3 directionToTarget = _targetPosition - Mover.Tr.position;
            _enemy.Get<>()
        }

        public void OnExit()
        {
         
        }
    }
}