
using Project.Scripts.GamePlay.Common.Physic.Raycast;
using Project.Scripts.GamePlay.Common.Time;
using Project.Scripts.GamePlay.Enemies.EnemyController.Enum;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Project.Scripts.GamePlay.Enemies.EnemyController
{
    public class NavMeshEnemyMover : RigidbodyEnemyMover
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _stoppingDistance = 1f;
        [SerializeField] private float _groundCheckDistance = 0.2f;
        private float _timeScale = 1f;
        private float _defaultSpeed ;


        private RaycastSensor _sensor;
        private ITimeService _timeService;
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

        protected override void Start()
        {
            base.Start();
            _timeService.OnTimeScaleChanged += AdjustMoverTimeScale;

        }

        public float RotationSpeed => _agent.angularSpeed;

        public void SetDestination(Vector3 destination, float speed)
        {
            _defaultSpeed = speed;
            _agent.speed = _defaultSpeed * _timeScale;
            _agent.SetDestination(destination);
            _agent.isStopped = false;
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

        public override void ApplyForce(Vector3 force)
        {
            base.ApplyForce(force);
            SetMovementType(EnemyMovementType.PhysicsWithGravity);
        }

        public override void SetMovementType(EnemyMovementType enemyMovementType)
        {
            base.SetMovementType(enemyMovementType);
            if (enemyMovementType == EnemyMovementType.NavMesh)
            {
                EnableAgent();
            }
            else
            {
                DisableAgent();
            }
        }

        public override void FixedTick(float fixedDeltaTime)
        {
            base.FixedTick(fixedDeltaTime);
            if (EnemyMovementType != EnemyMovementType.NavMesh)
            {
                if (IsGrounded())
                {
                    SetMovementType(EnemyMovementType.NavMesh);
                }
            }
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
            bool notRising=!Rigidbody.isKinematic||Rigidbody.linearVelocity.y<=0.01f;
            return _sensor.HasDetectedHit()&&notRising;
        }
        public bool Falling() => Rigidbody.linearVelocity.y<=0.0f;
    }
}