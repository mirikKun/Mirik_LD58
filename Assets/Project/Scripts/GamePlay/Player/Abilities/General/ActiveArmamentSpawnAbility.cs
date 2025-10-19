using Assets.Code.GamePlay.Common.Entity;
using Project.Scripts.GamePlay.Armaments;
using Project.Scripts.GamePlay.Armaments.Configs;
using Project.Scripts.GamePlay.Armaments.Factories;
using Project.Scripts.GamePlay.Player.Abilities.Configs;
using Project.Scripts.GamePlay.Player.PlayerResources;
using Zenject;

namespace Project.Scripts.GamePlay.Player.Abilities.General
{
    public class ActiveArmamentSpawnAbility : IAbility
    {
        private IArmamentsFactory _armamentsFactory;
        private ActorEntity _casterEntity;
        private bool _executing;

        private ActiveArmamentSpawnAbilityConfig _config;
        private Armament _armament;

        [Inject]
        private void Construct(IArmamentsFactory armamentsFactory)
        {
            _armamentsFactory = armamentsFactory;
        }

        public void Init(ActorEntity caster)
        {
            _casterEntity = caster;
        }

        public void SetConfig(ActiveArmamentSpawnAbilityConfig config)
        {
            _config = config;
        }

        public void OnInput(bool pressed)
        {
            if (pressed)
            {
                Execute();
            }

            if (!pressed && _executing)
            {
                OnExecutionEnded();
            }
        }

        public async void Execute()
        {
            _executing = true;
            ArmamentConfig armamentConfig = _armamentsFactory.GetArmamentConfig(_config.ArmamentType);
            _armament = _casterEntity.Get<ArmamentsHolder>().CreateArmament(armamentConfig);
            _casterEntity.Get<PlayerManaController>().ManaEnded += OnExecutionEnded;
            _armament.StartBehaviours();
        }

        private void OnExecutionEnded()
        {
            _executing = false;
            _casterEntity.Get<ArmamentsHolder>().RemoveArmament(_armament);
            _casterEntity.Get<PlayerManaController>().ManaEnded -= OnExecutionEnded;
        }
    }
}