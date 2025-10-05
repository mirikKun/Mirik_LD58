using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.Common.GameBehaviour.Services;
using Code.Gameplay.Common.Time;
using UnityEngine;
using Zenject;

namespace Assets.Code.GamePlay.Enemies.EnemyController
{
    public class EnemyRigidbodyMover : EntityComponent, IPausable
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private bool _canBeMoved = true;
        public Transform Tr;
        
        private ITimeService _timeService;
        private IUpdateService _updateService;
        private Vector3 _savedVelocity;

        private Vector3 _momentum;
        private Vector3 _savedMovementVelocity;


        [Inject]
        private void Construct(ITimeService timeService, IUpdateService updateService)
        {
            _timeService = timeService;
            _updateService = updateService;
        }
        private void Start()
        {
            _updateService.Pausable.Register(this);
        }        public override void InitEntity(ActorEntity entity)
        {
            base.InitEntity(entity);
            Tr = transform;
        }
        public void SetMomentum(Vector3 momentum)
        {
            _momentum = momentum;

        }
        public void FixedTick(float fixedDeltaTime)
        {
            SetRbVelocity(_momentum, _timeService.TimeScale);
        }
        public void SetRbVelocity(Vector3 velocity, float timeScale) => _rigidbody.linearVelocity =
            velocity* timeScale;
        
        public void SetRigidbodyKinematic(bool kinematic)
        {
            _rigidbody.isKinematic = kinematic;
        }

        public void EnableGravity(bool enable)
        {
            SetRigidbodyKinematic(!enable);
            _rigidbody.useGravity = enable;
        }
        public void Pause()
        {
            SetRigidbodyKinematic(true);
        }

        public void Resume()
        {
            SetRigidbodyKinematic(false);
        }
    }
}