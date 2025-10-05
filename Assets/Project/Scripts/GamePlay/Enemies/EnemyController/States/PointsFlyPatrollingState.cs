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
        private List<Vector3> _patrolPoints;
        private Vector3 _targetPosition;
        public PointsFlyPatrollingState(ActorEntity enemy, PointsFlyPatrollingStateConfig patrollingConfig)
        {
            _enemy = enemy;
            _patrollingConfig = patrollingConfig;
            _patrolPoints=_enemy.Get<EnemyPatrolPointsHolder>().PatrolPoints.Select(x=>x.position).ToList();
        }

        public void OnEnter()
        {
            _targetPosition = _patrolPoints.PickRandom(_targetPosition);

        }

        public void Update(float deltaTime)
        {
            float speed= _patrollingConfig.Speed.RandomValueInRange;
            float rotationSpeed= _patrollingConfig.RotationSpeed;

            EnemyRigidbodyMover mover = _enemy.Get<EnemyRigidbodyMover>();
            Vector3 directionToTarget = _targetPosition - mover.Tr.position;
            mover.SetMomentum( directionToTarget.normalized * speed);
            mover.Tr.forward= Vector3.Slerp(mover.Tr.forward, directionToTarget.normalized, rotationSpeed * deltaTime);
        }
        
        

        public void OnExit()
        {
            _enemy.Get<EnemyRigidbodyMover>().SetMomentum( Vector3.zero);

        }

        public bool HasReachedDestination()
        {
            return Vector3.Distance(_enemy.GetPosition(), _targetPosition) < _patrollingConfig.ReachThreshold;
        }
    }
}