using System.Collections.Generic;
using Assets.Code.GamePlay.Common.Entity;
using Project.Scripts.GamePlay.Player.Abilities.Configs;
using Project.Scripts.GamePlay.Player.Abilities.Factory;
using Project.Scripts.GamePlay.Player.Abilities.General;
using Project.Scripts.GamePlay.Player.Abilities.Systems;
using Project.Scripts.GamePlay.Player.PlayerStateMachine;
using Zenject;

namespace Project.Scripts.GamePlay.Player.Abilities.Behaviours
{
    public class AbilitiesCaster:EntityComponent
    {
        private List<IAbility> _abilities = new List<IAbility>();
        private IAbilitiesFactory _abilitiesFactory;
        private IAbilitiesSystem _abilitiesSystem;

        [Inject]
        public void Construct(IAbilitiesFactory abilitiesFactory,IAbilitiesSystem abilitiesSystem)
        {
            _abilitiesFactory = abilitiesFactory;
            _abilitiesSystem = abilitiesSystem;
        }
        private void Awake()
        {
            _abilitiesSystem.AbilitiesListChanged += Init;
        }

        private void Start()
        {
            Init();
        }

        private void OnDestroy()
        {
            _abilitiesSystem.AbilitiesListChanged -= Init;

        }

        public void Tick(float deltaTime)
        {
            foreach (var ability in _abilities)
            {
                if (ability is ITickableAbility tickableAbility)
                {
                    tickableAbility.Tick(deltaTime);
                }
            }
        }

        public void Init()
        {
            foreach (AbilityInstance abilityInstance in _abilitiesSystem.Abilities)
            {
                abilityInstance.Clear();
                if (abilityInstance.AbilityConfig is ActionAbilityConfig actionAbilityConfig)
                {
                    IAbility ability = actionAbilityConfig.CreateAbility(_abilitiesFactory);
                    ability.Init(Entity);
                    abilityInstance.OnAbilityInput += ability.OnInput;
                    _abilities.Add(ability);
                }
                if (abilityInstance.AbilityConfig is CombatMoveAbilityConfig combatMoveAbilityConfig)
                {
                    IAbility ability = combatMoveAbilityConfig.CreateAbility(_abilitiesFactory);
                    ability.Init(Entity);
                    abilityInstance.OnAbilityInput += ability.OnInput;
                    _abilities.Add(ability);
                }
            }
            
            Entity.Get<PlayerStateMachineContainer>().SetupStateMachine();

        }
        
        
    }
}