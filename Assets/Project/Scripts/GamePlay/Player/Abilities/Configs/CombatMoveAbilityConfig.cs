using Project.Scripts.GamePlay.Armaments.Configs;
using Project.Scripts.GamePlay.Player.Abilities.Factory;
using Project.Scripts.GamePlay.Player.Abilities.General;
using Project.Scripts.GamePlay.Player.PlayerStateMachine.StateConfigs;
using UnityEngine;

namespace Project.Scripts.GamePlay.Player.Abilities.Configs
{
    [CreateAssetMenu(fileName = "CombatMoveAbility", menuName = "Configs/Abilities/CombatMoveAbility")]
    public class CombatMoveAbilityConfig : BaseAbilityConfig
    {
        [field: SerializeField] public BaseMoveStateConfig MovementMoveStateConfig { get; private set; }
        [field: SerializeField] public ArmamentConfig ArmamentConfig { get; private set; }

        public IAbility CreateAbility(IAbilitiesFactory abilitiesFactory)
        {
            CombatMoveAbility ability=abilitiesFactory.CreateAbility<CombatMoveAbility>();
            ability.SetConfig(this);
            return ability;
        }
    }
}