using System;
using Assets.Code.GamePlay.Common.Entity;
using Project.Scripts.GamePlay.Armaments;
using Project.Scripts.GamePlay.Armaments.ArmamentBehaviour;
using Project.Scripts.GamePlay.Armaments.Configs;
using Project.Scripts.GamePlay.Armaments.Enums;
using Project.Scripts.GamePlay.Armaments.Factories;
using Project.Scripts.GamePlay.Common.Enums;
using Project.Scripts.GamePlay.Common.Physic.Raycast;
using Project.Scripts.GamePlay.Common.Time;
using Project.Scripts.GamePlay.Player.Abilities.Configs;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Project.Scripts.GamePlay.Player.Abilities.General
{
    public class LandscapeArmamentSpawnAbility : IAbility,ITickableAbility
    {
        private LandscapeArmamentSpawnAbilityConfig _config;

        private IArmamentsFactory _armamentsFactory;
        private ActorEntity _casterEntity;
        private ITimeService _timeService;
        private ArmamentIndicator _currentIndicator;
        private bool _isPressed;
        private bool _canCast;
        private RaycastSensor _raycastSensor;
        private Transform _placement;

        [Inject]
        private void Construct(IArmamentsFactory armamentsFactory, ITimeService timeService)
        {
            _timeService = timeService;
            _armamentsFactory = armamentsFactory;
        }

        public void SetConfig(LandscapeArmamentSpawnAbilityConfig config)
        {
            _config = config;
        }

        public void Init(ActorEntity caster)
        {
            _casterEntity = caster;
            _placement = _casterEntity.Get<ArmamentsHolder>().GetArmamentPlacement(ArmamentPlacementType.CharacterHead);
            _raycastSensor = new RaycastSensor(_placement);
            _raycastSensor.Layermask = _config.RaycastLayerMask;

        }

        public void OnInput(bool pressed)
        {

            if (pressed)
            {
                ShowIndicator();
            }
            else if (_isPressed)
            {
                HideIndicator();
                Execute();
            }
            _isPressed = pressed;
        }

        private void ShowIndicator()
        {
            UpdateRaycast();
            if(_raycastSensor.HasDetectedHit())
            {
                Quaternion rotation = _raycastSensor.GetNormalRotation();
                _currentIndicator = _casterEntity.Get<ArmamentsHolder>().CreateIndicator(_config.IndicatorType,_raycastSensor.GetPosition(),rotation);
            }
            else
            {
                _currentIndicator = _casterEntity.Get<ArmamentsHolder>().CreateIndicator(_config.IndicatorType,Vector3.zero, Quaternion.identity);
                _currentIndicator.Hide();
            }
        }

     
        public void HideIndicator()
        {
            if (_currentIndicator != null)
            {
                _currentIndicator.Hide();
                Object.Destroy(_currentIndicator.gameObject);
                _currentIndicator = null;
            }
        }
        public void UpdateIndicatorPosition()
        {
            if (_currentIndicator == null)
                return;
            
            // Raycast straight forward
            UpdateRaycast();

            if (_raycastSensor.HasDetectedHit())
            {
                // Hit surface - place indicator at hit point with normal rotation
                Vector3 hitPosition = _raycastSensor.GetPosition();
                Quaternion rotation = _raycastSensor.GetNormalRotation();
                
                _currentIndicator.SetPosition(hitPosition);
                _currentIndicator.SetRotation(rotation);
                _currentIndicator.Show();
                _canCast = true;
            }
            else
            {
                // No hit - hide indicator
                _currentIndicator.Hide();
                _canCast = false;
            }
        }

        private void UpdateRaycast()
        {
            _raycastSensor.CastLength = _config.MaxStraightRange;
            _raycastSensor.SetCastOrigin(_placement.position);
            _raycastSensor.SetCastDirection(CastDirection.Forward);
            _raycastSensor.Cast();
        }

        public void Tick(float deltaTime)
        {

            if (_isPressed)
            {
                UpdateIndicatorPosition();
            }
        }

        public void Execute()
        {
            if (!_canCast)
                return;

            ArmamentConfig armamentConfig = _armamentsFactory.GetArmamentConfig(_config.ArmamentType);
            Vector3 direction = _raycastSensor.GetNormal();
            Quaternion rotation = Quaternion.LookRotation(direction);
            Vector3 position = _raycastSensor.GetPosition()+direction*_config.Offset;
            Armament armament = _casterEntity.Get<ArmamentsHolder>().CreateArmament(armamentConfig, position, rotation,null,armamentConfig.HitCaster);


            armament
                .With(new LifetimeArmamentBehaviour(armamentConfig.Duration))
                .With(new MovingArmamentBehaviour(armamentConfig.Speed, direction, _timeService))
                .StartBehaviours();
        }


    }
}