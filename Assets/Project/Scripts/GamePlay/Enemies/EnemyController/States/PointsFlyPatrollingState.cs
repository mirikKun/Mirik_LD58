using System.Collections.Generic;
using System.Linq;
using Assets.Code.GamePlay.Common.Entity;
using Project.Scripts.GamePlay.Common.GameplayStateMachine;
using Project.Scripts.GamePlay.Enemies.EnemyController.StateConfigs;
using Project.Scripts.Utils.Extensions;
using Unity.VisualScripting;
using UnityEngine;
using IState = Project.Scripts.GamePlay.Common.GameplayStateMachine.IState;

namespace Project.Scripts.GamePlay.Enemies.EnemyController.States
{
    public class PointsFlyPatrollingState : IState
    {
        private readonly ActorEntity _enemy;
        private readonly PointsFlyPatrollingStateConfig _patrollingConfig;
        private List<Vector3> _patrolPoints;
        private Vector3 _targetPosition;
        private float _curTargetSpeed;

        public PointsFlyPatrollingState(ActorEntity enemy, PointsFlyPatrollingStateConfig patrollingConfig)
        {
            _enemy = enemy;
            _patrollingConfig = patrollingConfig;
            _patrolPoints = _enemy.Get<EnemyPatrolPointsHolder>().PatrolPoints.Select(x => x.position).ToList();
        }

        public void OnEnter()
        {
            _targetPosition = _patrolPoints.PickRandom(_targetPosition);
            _curTargetSpeed=_patrollingConfig.Speed.RandomValueInRange;
        }

        public void Update(float deltaTime)
        {
            float rotationSpeed = _patrollingConfig.RotationSpeed;

            RigidbodyEnemyMover mover = _enemy.Get<RigidbodyEnemyMover>();
            Vector3 directionToTarget = _targetPosition - mover.Tr.position;
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget.normalized);
            mover.Tr.rotation = Quaternion.Slerp(mover.Tr.rotation, targetRotation, rotationSpeed * deltaTime);
            mover.Rigidbody.angularVelocity = Vector3.zero;
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
            float acceleration = _patrollingConfig.Acceleration;

            RigidbodyEnemyMover mover = _enemy.Get<RigidbodyEnemyMover>();
            Vector3 directionToTarget = _targetPosition - mover.Tr.position;
            mover.ChangeMomentum(directionToTarget.normalized * _curTargetSpeed,acceleration,fixedDeltaTime);
            
        }


        public void OnExit()
        {
            _enemy.Get<RigidbodyEnemyMover>().SetMomentum(Vector3.zero);
        }

        public bool HasReachedDestination()
        {
            return Vector3.Distance(_enemy.GetPosition(), _targetPosition) < _patrollingConfig.ReachThreshold;
        }
    }
}