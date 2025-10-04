using Assets.Code.GamePlay.Abilities.General;
using Assets.Code.GamePlay.Abilities.Systems;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.Common.GameBehaviour.Services;
using Assets.Code.GamePlay.Player.Abilities.Factory;
using Assets.Code.GamePlay.Player.PlayerStateMachine;
using Assets.Code.GamePlay.Stats;
using UnityEngine;
using Zenject;

namespace Assets.Code.GamePlay.Player.Controller
{
    [RequireComponent(typeof(PlayerMover))]
    public class PlayerController : EntityComponent, IGameUpdateable, IGameFixedUpdateable, IGameLateUpdateable
    {
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private Transform _cameraViewTransform;
        [SerializeField] private Transform _targetTransform;


        private Transform _transform;
        private IInputReader _input;
        private IAbilitiesSystem _abilitiesSystem;
        
        private AbilitiesCaster _abilitiesCaster;
        private IUpdateService _updateService;
        private IAbilitiesFactory _abilitiesFactory;


        public Transform Tr => _transform;
        public Transform CameraTrX => _cameraController.CameraTrX;
        public Transform CameraTrY => _cameraController.CameraTrY;
        public Transform TargetTr => _targetTransform;
        public Transform CameraViewTr => _cameraViewTransform;
        public IInputReader Input => _input;
        public CameraController CameraController => _cameraController;
     

        [Inject]
        private void Construct(IInputReader inputReader, IUpdateService updateService, IAbilitiesSystem abilitiesSystem,IAbilitiesFactory abilitiesFactory)
        {
            _abilitiesFactory = abilitiesFactory;
            _abilitiesSystem = abilitiesSystem;
            _updateService = updateService;
            _input = inputReader;
        }

        private void Awake()
        {
            _transform = transform;
        }

        private void Start()
        {

            _input.EnablePlayerActions();
            Entity.Get<AbilitiesCaster>().Init();

            _updateService.PlayerUpdate.Register(this);
            _updateService.PlayerFixedUpdate.Register(this);
            _updateService.LateUpdate.Register(this);
        }

        private void OnDestroy()
        {
            _updateService.PlayerUpdate.Unregister(this);
            _updateService.PlayerFixedUpdate.Unregister(this);
            _updateService.LateUpdate.Unregister(this);
        }
        
        public void GameUpdate(float deltaTime)
        {
            Entity.Get<PlayerMover>().Tick(deltaTime);

            Entity.Get<PlayerStateMachineContainer>().Tick(deltaTime);
            Entity.Get<PlayerPickUpper>().Tick();
            Entity.Get<PlayerEffects.PlayerEffects>().CameraMovingEffects.Tick(deltaTime);
            //Entity.Get<EnemyDetector>().Tick(deltaTime);
            Entity.Get<StatsController>().Mediator.Tick(deltaTime);
        }

        public void GameLateUpdate(float deltaTime)
        {
            _cameraController.TickLateUpdate(deltaTime);
            Entity.Get<PlayerMover>().LateTick(deltaTime);
        }

        public void GameFixedUpdate(float fixedDeltaTime)
        {
            Entity.Get<PlayerMover>().SetVelocity(Vector3.zero);
            Entity.Get<PlayerMover>().CheckForGround(fixedDeltaTime);
            Entity.Get<PlayerStateMachineContainer>().FixedTick(fixedDeltaTime);

            Entity.Get<PlayerMover>().FixedTick(fixedDeltaTime);
            Entity.Get<CeilingDetector>().Reset();
            Entity.Get<WallDetector>().Reset();
        }

  
        public Vector3 GetInputMovementDirection()
        {
            Vector3 direction = CameraTrX == null
                ? _transform.right * _input.Direction.x + _transform.forward * _input.Direction.y
                : Vector3.ProjectOnPlane(CameraTrX.right, _transform.up).normalized * _input.Direction.x +
                  Vector3.ProjectOnPlane(CameraTrX.forward, _transform.up).normalized * _input.Direction.y;

            return direction.magnitude > 1f ? direction.normalized : direction;
        }

    }
}