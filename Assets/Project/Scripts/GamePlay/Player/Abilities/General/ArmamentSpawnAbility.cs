using Assets.Code.GamePlay.Common.Entity;
using Project.Scripts.GamePlay.Armaments;
using Project.Scripts.GamePlay.Armaments.ArmamentBehaviour;
using Project.Scripts.GamePlay.Armaments.Configs;
using Project.Scripts.GamePlay.Armaments.Factories;
using Project.Scripts.GamePlay.Common.Time;
using Project.Scripts.GamePlay.Player.Abilities.Configs;
using UnityEngine;
using Zenject;

namespace Project.Scripts.GamePlay.Player.Abilities.General
{
    public class ArmamentSpawnAbility:IAbility
    {
        private ArmamentSpawnAbilityConfig _config;

        private IArmamentsFactory _armamentsFactory;
        private ActorEntity _casterEntity;
        private ITimeService _timeService;

        [Inject]
        private void Construct(IArmamentsFactory armamentsFactory,ITimeService timeService)
        {
            _timeService = timeService;
            _armamentsFactory = armamentsFactory;
        }
        public void SetConfig(ArmamentSpawnAbilityConfig config)
        {
            _config = config;
        }

        public void Init(ActorEntity caster)
        {
            _casterEntity = caster;
        }

        public void OnInput(bool pressed)
        {
            if (pressed)
            {
                Execute();
            }
        }

        public void Execute()
        {
            ArmamentConfig armamentConfig = _armamentsFactory.GetArmamentConfig(_config.ArmamentType);
            Armament armament = _casterEntity.Get<ArmamentsHolder>().CreateArmament(armamentConfig,armamentConfig.HitCaster);


            Vector3 directionToTarget = _casterEntity.Get<ArmamentsHolder>().GetArmamentPlacement(armamentConfig).forward;
            armament
                .With(new LifetimeArmamentBehaviour(armamentConfig.Duration))
                .With(new MovingArmamentBehaviour(armamentConfig.Speed,directionToTarget,_timeService))
                .StartBehaviours();
        }
    }
}