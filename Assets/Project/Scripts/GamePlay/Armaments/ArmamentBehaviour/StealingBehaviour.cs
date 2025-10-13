using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.Physic.ColliderLogic;
using Assets.Code.GamePlay.Player.PlayerEffects;
using Project.Scripts.GamePlay.Armaments.ArmamentBehaviour.Abstract;
using Project.Scripts.GamePlay.Collection.Systems;
using Project.Scripts.GamePlay.Player.PlayerResources;
using Project.Scripts.GamePlay.Player.StealSystem;
using UnityEngine;
using Zenject;

namespace Project.Scripts.GamePlay.Armaments.ArmamentBehaviour
{
    public class StealingBehaviour:ComponentBehaviour,IUpdateableArmament,IStartableBehaviour,IOnDestroyableBehaviour
    {
        [SerializeField] private float _manaSpendRate = 10;
        [SerializeField] private ParryTrigger _parryTrigger;
        private BookEffects _bookEffects;
        private ICollectionSystem _collectionSystem;
        private BaseEntity _casterEntity;

        public BaseEntity CasterEntity => _casterEntity;

        [Inject]
        private void Construct(ICollectionSystem collectionSystem)
        {
            _collectionSystem = collectionSystem;
        }
        public void Start()
        {
            _bookEffects = CasterEntity.Get<PlayerEffects>().BookEffects;
            _parryTrigger.OnHitEvent += OnParryTriggered;
            _bookEffects.StartStealingEffect();
        }
        private void OnParryTriggered(IAttackTrigger attackTrigger)
        {
            if (attackTrigger is ArmamentTrigger armamentTrigger  && armamentTrigger.ArmamentConfig!=null)
            {
                _collectionSystem.TryAddStealArmamentAbility(armamentTrigger.ArmamentConfig);
                armamentTrigger.Dismiss();
            }
        }

        public void Tick(float deltaTime)
        {
            _casterEntity.Get<PlayerManaController>().SpendMana(_manaSpendRate*deltaTime);
        }

        public void OnDestroy()
        {
            _parryTrigger.OnHitEvent -= OnParryTriggered;

            _bookEffects.StopStealingEffect();
        }
    }
}