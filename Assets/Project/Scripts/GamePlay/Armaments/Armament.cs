using System;
using System.Collections.Generic;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.Common.GameBehaviour.Services;
using Project.Scripts.GamePlay.Armaments.ArmamentBehaviour.Abstract;
using Project.Scripts.GamePlay.Armaments.Configs;
using Project.Scripts.Sounds;
using UnityEngine;
using Zenject;

namespace Project.Scripts.GamePlay.Armaments
{
    public class Armament : MonoBehaviour, IGameUpdateable,IGameFixedUpdateable
    {
        [SerializeField] private ArmamentTrigger _armamentTrigger;
        [SerializeField] private List<ComponentBehaviour> _componentBehaviours;
        [SerializeField] private Rigidbody _rigidbody;
        private List<IArmamentBehaviour> _armamentBehaviours = new List<IArmamentBehaviour>();
        private IUpdateService _updateService;
        public event Action<Armament> Destroyed;
        private bool _dissmissed;
        private ActorEntity _casterEntity;
        private ISoundsSystem _soundsSystem;
        private ArmamentConfig _config;
        public Rigidbody Rigidbody=>_rigidbody;
        public ActorEntity CasterEntity => _casterEntity;
        public ArmamentConfig Config => _config;

        [Inject]
        private void Construct( IUpdateService updateService,ISoundsSystem soundsSystem)
        {
            _soundsSystem = soundsSystem;
            _updateService = updateService;
        }

        private void Start()
        {
            _updateService.ProjectilesUpdate.Register(this);
            _updateService.ProjectilesFixedUpdate.Register(this);
            _armamentTrigger.Dismissed += OnDismissed;
            _armamentTrigger.Hitted += OnHit;
        }

        private void OnDestroy()
        {
            _updateService.ProjectilesUpdate.Unregister(this);
            _updateService.ProjectilesFixedUpdate.Unregister(this);
            _armamentTrigger.Dismissed -= OnDismissed;
            _armamentTrigger.Hitted -= OnHit;

        }

        public Armament Init(ActorEntity caster, ArmamentConfig config)
        {
            _config = config;
            _casterEntity = caster;
            _armamentTrigger.Init(caster);
            _armamentTrigger.SetData(config,this);

            foreach (var componentBehaviour in _componentBehaviours)
            {
                With(componentBehaviour);
            }
            _soundsSystem.PlayOneShot(_config.SpawnSound,transform.position);

            return this;
        }

        public Armament With(IArmamentBehaviour armamentBehaviour)
        {
            armamentBehaviour.InitArmament(this);
            _armamentBehaviours.Add(armamentBehaviour);
            return this;
        }

        public void GameUpdate(float deltaTime)
        {
            foreach (var armamentBehaviour in _armamentBehaviours)
            {
                if (armamentBehaviour is IUpdateableArmament armament)
                {
                    armament.Tick(deltaTime);
                }
            }
        }

        public void GameFixedUpdate(float fixedDeltaTime)
        {
            foreach (var armamentBehaviour in _armamentBehaviours)
            {
                if (armamentBehaviour is IFixedUpdateableArmament armament)
                {
                    armament.FixedTick(fixedDeltaTime);
                }
            }
            
        }

        public void StartBehaviours()
        {
            foreach (var armamentBehaviour in _armamentBehaviours)
            {
                if (armamentBehaviour is IStartableBehaviour armament)
                {
                    armament.StartBehaviour();
                }
            }
        }

        private void OnDismissed()
        {
            //throw new NotImplementedException();
        }

        private void OnHit()
        {
            _soundsSystem.PlayOneShot(_config.HitSound,transform.position);
        }


        public void Destroy()
        {
            foreach (var armamentBehaviour in _armamentBehaviours)
            {
                if (armamentBehaviour is IOnDestroyableBehaviour armament)
                {
                    armament.OnDestroy();
                }
            }
            _soundsSystem.PlayOneShot(_config.DestroySound,transform.position);

            Destroyed?.Invoke(this);
            Destroy(gameObject);
        }
    }
}