using Assets.Code.GamePlay.Armaments;
using Assets.Code.GamePlay.Armaments.ArmamentBehaviour;
using Assets.Code.GamePlay.Armaments.Projectiles;
using Assets.Code.GamePlay.Armaments.Projectiles.Factories;
using Assets.Code.GamePlay.Common.Entity;
using Project.Scripts.GamePlay.Player.Abilities.Configs;
using UnityEngine;
using Zenject;
namespace Assets.Code.GamePlay.Abilities.General
{
    public class ArmamentSpawnAbility:IAbility
    {
        private ArmamentSpawnAbilityConfig _config;

        private IArmamentsFactory _armamentsFactory;
        private ActorEntity _casterEntity;

        [Inject]
        private void Construct(IArmamentsFactory armamentsFactory)
        {
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

        public async void Execute()
        {
            ArmamentConfig armamentConfig = _armamentsFactory.GetArmamentConfig(_config.ArmamentType);
            Armament armament = _casterEntity.Get<ArmamentsHolder>().CreateArmament(armamentConfig);


            Vector3 directionToTarget = _casterEntity.Get<ArmamentsHolder>().GetArmamentPlacement(armamentConfig).forward;
            armament
                .With(new LifetimeArmamentBehaviour(armamentConfig.Duration))
                .With(new MovingArmamentBehaviour(armamentConfig.Speed,directionToTarget))
                .StartBehaviours();
        }
    }
}