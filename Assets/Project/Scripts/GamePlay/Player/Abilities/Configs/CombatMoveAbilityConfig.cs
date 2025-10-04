using Assets.Code.GamePlay.Abilities.General;
using Assets.Code.GamePlay.Armaments;
using Assets.Code.GamePlay.Player.Abilities.Factory;
using Assets.Code.GamePlay.Player.PlayerStateMachine.StateConfigs;
using UnityEngine;

namespace Assets.Code.GamePlay.Player.Abilities.Configs
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