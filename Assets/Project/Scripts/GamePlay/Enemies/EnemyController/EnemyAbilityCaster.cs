using Assets.Code.GamePlay.Abilities.General;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.Player.Abilities.Configs;
using Assets.Code.GamePlay.Player.Abilities.Factory;
using Zenject;

namespace Assets.Code.GamePlay.Enemies.EnemyController
{
    public class EnemyAbilityCaster:EntityComponent
    {
        private IAbilitiesFactory _abilitiesFactory;
        [Inject]
        public void Construct(IAbilitiesFactory abilitiesFactory)
        {
            _abilitiesFactory = abilitiesFactory;
        }
        public IAbility CreateAbility(BaseAbilityConfig config)
        {
            IAbility ability=null;
            if (config is ActionAbilityConfig actionAbilityConfig)
            {
                ability = actionAbilityConfig.CreateAbility(_abilitiesFactory);
                ability.Init(Entity);
     
            }
            if (config is CombatMoveAbilityConfig combatMoveAbilityConfig)
            {
                ability = combatMoveAbilityConfig.CreateAbility(_abilitiesFactory);
                ability.Init(Entity);
  
            }
            return ability;
        }
    }
}