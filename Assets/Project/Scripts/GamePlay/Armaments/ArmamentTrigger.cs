using System;
using System.Collections.Generic;
using Assets.Code.GamePlay.Armaments;
using Assets.Code.GamePlay.Armaments.ArmamentBehaviour;
using Assets.Code.GamePlay.Armaments.Enums;
using Assets.Code.GamePlay.Armaments.Projectiles.Factories;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.DataDriven.Effects;
using UnityEngine;
using Zenject;

namespace Assets.Code.GamePlay.Physic.ColliderLogic
{
    public class ArmamentTrigger : MonoBehaviour, IAttackTrigger
    {
        [SerializeField] private ParticleSystem[] _particleSystems;
        [SerializeField] private GameObject _root;
        private List<ITriggerHittable> _hitObjects = new List<ITriggerHittable>();
        private List<ITriggerHittable> _hitProtectedObjects = new List<ITriggerHittable>();
        private ArmamentHitType _configArmamentHitType;
        private BaseEntity _casterEntity;
        private Coroutine _hitCoroutine;
        private IArmamentsFactory _armamentsFactory;
        private ArmamentConfig _armamentToSpawn;
        private ArmamentConfig _currentArmamentConfig;
        public BaseEntity CasterEntity => _casterEntity;
        public List<ITriggerHittable> HitObjects => _hitObjects;
        public List<Effect> Effects { get; private set; }
        public ArmamentConfig ArmamentConfig=> _currentArmamentConfig;
        public event Action Hitted;
        public event Action Dismissed;
        private bool _dismissed;

        [Inject]
        private void Construct(IArmamentsFactory armamentsFactory)
        {
            _armamentsFactory = armamentsFactory;
        }

        public void Init(BaseEntity casterEntity)
        {
            _casterEntity = casterEntity;
        }

        public void SetData(ArmamentConfig config)
        {
            _currentArmamentConfig= config;
            Effects = config.Effects;
            _configArmamentHitType = config.ArmamentHitType;
            _armamentToSpawn = config.ArmamentToSpawnOnDestroy;
        }


        public void Reset()
        {
            _hitObjects.Clear();
            _hitProtectedObjects.Clear();
            _hitCoroutine = null;
        }

        public void Dismiss()
        {
            Dismissed?.Invoke();
            _dismissed= true;
            Destroy(_root);

        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ITriggerHittable hittable) && !_hitObjects.Contains(hittable) &&
                !_hitProtectedObjects.Contains(hittable))
            {
                OnHit(hittable);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            OnHit(null);
        }

        private void OnHit(ITriggerHittable hittable)
        {
            if (hittable != null)
            {
                _hitObjects.Add(hittable);
                hittable.OnHit(this);
            }
            Hitted?.Invoke();
            if (_dismissed) return;
            foreach (var particleSystem in _particleSystems)
            {
                particleSystem.Play();
            }

        

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

            Destroy(_root);
        }

        public void AddHitProtected(ITriggerHittable hittable)
        {
            _hitProtectedObjects.Add(hittable);
        }
    }
}