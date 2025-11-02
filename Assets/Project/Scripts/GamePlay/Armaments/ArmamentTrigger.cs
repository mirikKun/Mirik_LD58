using System;
using System.Collections.Generic;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.DataDriven.Effects;
using Project.Scripts.GamePlay.Armaments.ArmamentBehaviour;
using Project.Scripts.GamePlay.Armaments.Configs;
using Project.Scripts.GamePlay.Armaments.Enums;
using Project.Scripts.GamePlay.Armaments.Factories;
using Project.Scripts.GamePlay.Common.Physic.ColliderLogic;
using Project.Scripts.Sounds;
using UnityEngine;
using Zenject;

namespace Project.Scripts.GamePlay.Armaments
{
    public class ArmamentTrigger : MonoBehaviour, IAttackTrigger
    {
        [SerializeField] private ParticleSystem[] _particleSystems;
        [SerializeField] private GameObject _root;
        private List<ITriggerHittable> _hitObjects = new List<ITriggerHittable>();
        private List<ITriggerHittable> _hitProtectedObjects = new List<ITriggerHittable>();
        private Dictionary<ITriggerHittable, float> _hitCooldowns = new Dictionary<ITriggerHittable, float>();
        private List<ITriggerHittable> _targetsInTrigger = new List<ITriggerHittable>();
        private ArmamentHitType _configArmamentHitType;
        private float _hitPeriod;
        private BaseEntity _casterEntity;
        private IArmamentsFactory _armamentsFactory;
        private ArmamentConfig _armamentToSpawn;
        private ArmamentConfig _currentArmamentConfig;
        public BaseEntity CasterEntity => _casterEntity;
        public List<ITriggerHittable> HitObjects => _hitObjects;
        public List<Effect> Effects { get; private set; }
        public ArmamentConfig ArmamentConfig=> _currentArmamentConfig;
        public event Action Hitted;
        public Transform Transform=>transform;
        private bool _dismissed;
        private Armament _armament;

        [Inject]
        private void Construct(IArmamentsFactory armamentsFactory)
        {
            _armamentsFactory = armamentsFactory;
        }

        public void Init(BaseEntity casterEntity)
        {
            _casterEntity = casterEntity;
        }

        public void SetData(ArmamentConfig config,Armament armament)
        {
            _currentArmamentConfig= config;
            Effects = config.Effects;
            _configArmamentHitType = config.ArmamentHitType;
            _hitPeriod = config.HitPeriod;
            _armamentToSpawn = config.ArmamentToSpawnOnDestroy;
            _armament = armament;
        }


        public void Reset()
        {
            _hitObjects.Clear();
            _hitProtectedObjects.Clear();
            _hitCooldowns.Clear();
            _targetsInTrigger.Clear();
        }

        public void Dismiss()
        {
            _dismissed= true;
            _armament.Dismiss();

        }

       

        public void GameUpdate(float deltaTime)
        {
            if (_configArmamentHitType != ArmamentHitType.EveryoneWithPeriod)
                return;

            // Update cooldowns
            var keysToUpdate = new List<ITriggerHittable>(_hitCooldowns.Keys);
            foreach (var target in keysToUpdate)
            {
                _hitCooldowns[target] -= deltaTime;
            }

            // Check targets in trigger for periodic hits
            foreach (var target in _targetsInTrigger)
            {
                if (CanHitTarget(target))
                {
                    OnHit(target);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ITriggerHittable hittable) && !_hitProtectedObjects.Contains(hittable))
            {
                if (_configArmamentHitType == ArmamentHitType.EveryoneWithPeriod)
                {
                    // Add to targets in trigger
                    if (!_targetsInTrigger.Contains(hittable))
                    {
                        _targetsInTrigger.Add(hittable);
                    }
                    
                    // For EveryoneWithPeriod, allow hitting even if already hit        
                    if (CanHitTarget(hittable))
                    {
                        OnHit(hittable);
                    }
                }               
                else if (!_hitObjects.Contains(hittable))
                {
                    // For other types, only hit if not already hit
                    OnHit(hittable);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (_configArmamentHitType != ArmamentHitType.EveryoneWithPeriod)
                return;

            if (other.TryGetComponent(out ITriggerHittable hittable))
            {
                _targetsInTrigger.Remove(hittable);
            }
        }

        private bool CanHitTarget(ITriggerHittable hittable)
        {
            if (!_hitCooldowns.TryGetValue(hittable, out float cooldown))
            {
                // Never hit this target before
                return true;
            }

            // Check if cooldown has expired
            return cooldown <= 0;
        }

        private void OnCollisionEnter(Collision other)
        {
            OnHit(null);
        }

        private void OnHit(ITriggerHittable hittable)
        {
            if (hittable != null)
            {
                // For EveryoneWithPeriod, set cooldown timer
                if (_configArmamentHitType == ArmamentHitType.EveryoneWithPeriod)
                {
                    _hitCooldowns[hittable] = _hitPeriod;
                    
                    // Still add to _hitObjects for tracking, but it won't prevent re-hits
                    if (!_hitObjects.Contains(hittable))
                    {
                        _hitObjects.Add(hittable);
                    }
                }
                else
                {
                    // For other types, just add to _hitObjects
                    _hitObjects.Add(hittable);
                }
                
                hittable.OnHit(this);
            }
            if (_dismissed) return;
            foreach (var particleSystem in _particleSystems)
            {
                particleSystem.Play();
            }
            Hitted?.Invoke();


        

            switch (_configArmamentHitType)
            {
                case ArmamentHitType.FirstHitSolo:
                case ArmamentHitType.FirstHitAll:
                    OnLifeTimeEnded();
                    break;
                case ArmamentHitType.EveryoneOneHit:
                    break;
                case ArmamentHitType.EveryoneWithPeriod:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnLifeTimeEnded()
        {
            if (_armamentToSpawn != null)
            {
                var armament =
                    _armamentsFactory.CreateArmament(_armamentToSpawn, transform.position, transform.rotation);
                armament.Init(_casterEntity as ActorEntity, _armamentToSpawn);
                armament.With(new LifetimeArmamentBehaviour(_armamentToSpawn.Duration))
                    .StartBehaviours();
            }

            _armament.Destroy();
        }

        public void AddHitProtected(ITriggerHittable hittable)
        {
            _hitProtectedObjects.Add(hittable);
        }
    }
}