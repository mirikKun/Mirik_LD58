using Assets.Code.GamePlay.Player.PlayerStateMachine.StateConfigs;
using UnityEngine;

namespace Assets.Code.GamePlay.Player.Abilities.Configs
{
    [CreateAssetMenu(fileName = "MovingAbility", menuName = "Configs/Abilities/MovingAbility")]
    public class MovingAbilityConfig : BaseAbilityConfig
    {
        [field: SerializeField] public BaseMoveStateConfig MovementMoveStateConfig { get; private set; }
        
    }
}