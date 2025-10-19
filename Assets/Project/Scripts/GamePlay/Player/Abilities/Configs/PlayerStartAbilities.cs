using UnityEngine;

namespace Project.Scripts.GamePlay.Player.Abilities.Configs
{
    [CreateAssetMenu(fileName = "PlayerStartAbilities", menuName = "Configs/Player/Abilities/PlayerStartAbilities")]
    public class PlayerStartAbilities:ScriptableObject
    {
        [field: SerializeField] public BaseAbilityConfig[] BaseAbilityConfigs { get; private set; }
    }
}