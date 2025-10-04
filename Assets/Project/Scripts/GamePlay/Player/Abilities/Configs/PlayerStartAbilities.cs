using Assets.Code.GamePlay.Player.Abilities.Configs;
using Assets.Code.GamePlay.Player.PlayerStateMachine.StateConfigs;
using UnityEngine;

namespace Assets.Code.GamePlay.Abilities.Configs
{
    [CreateAssetMenu(fileName = "PlayerStartAbilities", menuName = "Configs/Player/Abilities/PlayerStartAbilities")]
    public class PlayerStartAbilities:ScriptableObject
    {
        [field: SerializeField] public BaseAbilityConfig[] BaseAbilityConfigs { get; private set; }
    }
}