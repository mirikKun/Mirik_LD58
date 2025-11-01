using System;
using Assets.Code.GamePlay.Common.Entity;
using Project.Scripts.GamePlay.Common.GameBehaviour.Services;
using Project.Scripts.GamePlay.Common.Movement;
using Project.Scripts.GamePlay.Common.Time;
using Project.Scripts.GamePlay.Enemies.EnemyController.Enum;
using UnityEngine;
using Zenject;

namespace Project.Scripts.GamePlay.Enemies.EnemyController
{
    public class RigidbodyEnemyMover : EntityComponent, IPausable,IMovementForceApplier
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform _transform;
        [SerializeField]private EnemyMovementType _enemyMovementType;
        [SerializeField]private bool _isFlying;
        public Transform Tr => _transform;
        public Rigidbody Rigidbody => _rigidbody;
        public Vector3 Momentum => _momentum;
        public EnemyMovementType EnemyMovementType => _enemyMovementType;
        public event Action Forced;
        public bool IsFlying => _isFlying;
        private ITimeService _timeService;
        private IUpdateService _updateService;

        private Vector3 _momentum;
        private EnemyMovementType _prevMovementType;


        [Inject]
        private void Construct(ITimeService timeService, IUpdateService updateService)
        {
            _timeService = timeService;
            _updateService = updateService;
        }

        protected virtual void Start()
        {
            _updateService.Pausable.Register(this);
            SetMovementType(_enemyMovementType);
            
        }

        private void OnDestroy()
        {
            _updateService.Pausable.Unregister(this);
        }

        public void SetMomentum(Vector3 momentum)
        {
            _momentum = momentum;
        }

        public void ChangeMomentum(Vector3 curTargetSpeed, float acceleration, float fixedDeltaTime)
        {
            _momentum=Vector3.Lerp(_momentum, curTargetSpeed, acceleration * fixedDeltaTime);
        }


        public virtual void FixedTick(float fixedDeltaTime)
        {
            if(_enemyMovementType==EnemyMovementType.Physics)
                SetRbVelocity(_momentum, _timeService.TimeScale);
          
        }

        public void SetRbVelocity(Vector3 velocity, float timeScale) => _rigidbody.linearVelocity =
            velocity * timeScale;


        public virtual void SetMovementType(EnemyMovementType enemyMovementType)
        {
            _enemyMovementType = enemyMovementType;
            switch (enemyMovementType)
            {
                case EnemyMovementType.None:
                    _rigidbody.isKinematic = true;
                    break;
                case EnemyMovementType.Kinematic:
                    _rigidbody.isKinematic = true;

                    break;
                case EnemyMovementType.Physics:
                    _rigidbody.isKinematic = false;
                    _rigidbody.useGravity = false;

                    break;
                case EnemyMovementType.PhysicsWithGravity:
                    _rigidbody.isKinematic = false;
                    _rigidbody.useGravity = true;
                    break;
       
            }
        }

        public void Pause()
        {
            _prevMovementType = _enemyMovementType;
            SetMovementType(EnemyMovementType.None);
        }

        public void Resume()
        {
            SetMovementType(_prevMovementType);
        }

        public virtual void ApplyForce(Vector3 force)
        {
            Forced?.Invoke();
            SetMomentum(force);
            if(_enemyMovementType==EnemyMovementType.Physics||_enemyMovementType == EnemyMovementType.PhysicsWithGravity)
            {
                
                SetRbVelocity(_momentum, _timeService.TimeScale);
            }
        }
    }
}