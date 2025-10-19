using Project.Scripts.GamePlay.Armaments.Enums;
using Project.Scripts.GamePlay.Player.Abilities.Factory;
using Project.Scripts.GamePlay.Player.Abilities.General;
using UnityEngine;

namespace Project.Scripts.GamePlay.Player.Abilities.Configs
{
    [CreateAssetMenu(fileName = "ArmamentSpawnAbility", menuName = "Configs/Abilities/ArmamentSpawnAbility")]
    public class ArmamentSpawnAbilityConfig : ActionAbilityConfig
    {
        [field: SerializeField] public ArmamentType ArmamentType { get; private set; }

        public override IAbility CreateAbility(IAbilitiesFactory abilitiesFactory)
        {
            ArmamentSpawnAbility ability = abilitiesFactory.CreateAbility<ArmamentSpawnAbility>();
            ability.SetConfig(this);
            return ability;
        }
    }
}