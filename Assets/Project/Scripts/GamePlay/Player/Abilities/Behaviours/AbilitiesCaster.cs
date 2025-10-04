using System;
using System.Collections.Generic;
using Assets.Code.GamePlay.Abilities.Systems;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.Player.Abilities.Configs;
using Assets.Code.GamePlay.Player.Abilities.Factory;
using Assets.Code.GamePlay.Player.Controller;
using Assets.Code.GamePlay.Player.PlayerStateMachine;
using Zenject;

namespace Assets.Code.GamePlay.Abilities.General
{
    public class AbilitiesCaster:EntityComponent
    {
        private List<IAbility> _abilities = new List<IAbility>();
        private readonly PlayerController _playerController;
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