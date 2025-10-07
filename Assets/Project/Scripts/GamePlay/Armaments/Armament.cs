using System;
using System.Collections.Generic;
using Assets.Code.GamePlay.Armaments.ArmamentBehaviour;
using Assets.Code.GamePlay.Armaments.ArmamentBehaviour.Abstract;
using Assets.Code.GamePlay.Armaments.Enums;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.Common.GameBehaviour.Services;
using Assets.Code.GamePlay.DataDriven.Effects;
using Assets.Code.GamePlay.Health;
using Assets.Code.GamePlay.Physic.ColliderLogic;
using Code.Gameplay.Common.Time;
using UnityEngine;
using Zenject;

namespace Assets.Code.GamePlay.Armaments
{
    public class Armament : MonoBehaviour, IGameUpdateable
    {
        [SerializeField] private ArmamentTrigger _armamentTrigger;

        private List<IArmamentBehaviour> _armamentBehaviours = new List<IArmamentBehaviour>();
        private IUpdateService _updateService;
        public event Action<Armament> Destroyed;
        private bool _dissmissed;

        [Inject]
        private void Construct( IUpdateService updateService)
        {
            _updateService = updateService;
        }

        private void Start()
        {
            _updateService.ProjectilesUpdate.Register(this);
            _armamentTrigger.Dismissed += OnDismissed;
        }

        private void OnDestroy()
        {
            _updateService.ProjectilesUpdate.Unregister(this);
            _armamentTrigger.Dismissed -= OnDismissed;

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

        public Armament Init(ActorEntity caster, ArmamentConfig config)
        {
            _armamentTrigger.Init(caster);
            _armamentTrigger.SetData(config,this);
            
            
            return this;
        }

        public Armament With(IArmamentBehaviour armamentBehaviour)
        {
            armamentBehaviour.InitArmament(this);
            _armamentBehaviours.Add(armamentBehaviour);
            return this;
        }

        public void StartBehaviours()
        {
            foreach (var armamentBehaviour in _armamentBehaviours)
            {
                if (armamentBehaviour is IStartableBehaviour armament)
                {
                    armament.Start();
                }
            }
        }

        private void OnDismissed()
        {
            //throw new NotImplementedException();
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
            Destroyed?.Invoke(this);
            Destroy(gameObject);
        }
    }
}