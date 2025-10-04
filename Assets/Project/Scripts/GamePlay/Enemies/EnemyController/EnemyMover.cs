using System;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.Physic.Raycast;
using Code.Gameplay.Common.Time;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Assets.Code.GamePlay.Enemies.EnemyController
{
    public class EnemyMover : EntityComponent
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _stoppingDistance = 1f;
        [SerializeField] private float _groundCheckDistance = 0.2f;
        private float _timeScale = 1f;
        private float _defaultSpeed ;
        private Transform _transform;


        private RaycastSensor _sensor;
        private ITimeService _timeService;
        public Transform Tr => _transform;
        [Inject]
        private void Construct(ITimeService timeService)
        {
            _timeService = timeService;
        }


        private void OnDestroy()
        {
            _timeService.OnTimeScaleChanged -= AdjustMoverTimeScale;
        }

        private void Awake()
        {
            _sensor ??= new RaycastSensor(transform);

            _sensor.SetCastOrigin(transform.transform.position);
            _sensor.SetCastDirection(RaycastSensor.CastDirection.Down);
            _sensor.CastLength = _groundCheckDistance;
        }

        private void Start()
        {
            _timeService.OnTimeScaleChanged += AdjustMoverTimeScale;

            _transform=transform;
        }

        public float RotationSpeed => _agent.angularSpeed;

        public void SetDestination(Vector3 destination, float speed)
        {
            _defaultSpeed = speed;
            _agent.speed = _defaultSpeed * _timeScale;
            _agent.SetDestination(destination);
            _agent.isStopped = false;
        }

        public Vector3[] GetPathPoints()
        {
            return _agent.path.corners;
        }

        private void AdjustMoverTimeScale(float scale)
        {
            _timeScale = scale;
            _agent.speed = _defaultSpeed * _timeScale;
        }

        public void RotateInDirection(Vector3 targetDirection,float deltaTime)
        {
            targetDirection.y = 0;
            transform.rotation=Quaternion.RotateTowards(transform.rotation,Quaternion.LookRotation(targetDirection),
                deltaTime*RotationSpeed);
        }
        public void SetRotationByDirection(Vector3 targetDirection)
        {
            targetDirection.y = 0;
            transform.rotation = Quaternion.LookRotation(targetDirection);
        }

        public void StopAgent()
        {
            _agent.isStopped = true;
        }

        public bool HasReachedDestination()
        {
            return !_agent.pathPending && _agent.remainingDistance <= _stoppingDistance
                                       && (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f);
        }
        public Vector3 GetClosestPointOnNavMesh(Vector3 position)
        {
            NavMeshHit hit;
            NavMesh.SamplePosition(position, out hit, 4, NavMesh.AllAreas);
            return hit.position;
        }


        public void DisableAgent()
        {
            _agent.enabled = false;

        }

        public void EnableAgent()
        {
            _agent.enabled = true;
        }
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public bool DetectGround(float distance)
        {
            _sensor.CastLength = distance;
            _sensor.Cast();
            return _sensor.HasDetectedHit();
        }
        public bool IsGrounded()
        {
            _sensor.Cast();
            bool notRising=!_rigidbody.isKinematic||_rigidbody.linearVelocity.y<=0.01f;
            return _sensor.HasDetectedHit()&&notRising;
        }
        public bool Falling() => _rigidbody.linearVelocity.y<=0.0f;
    }
}