using Project.Scripts.GamePlay.Armaments.Enums;
using Project.Scripts.GamePlay.Player.Abilities.Factory;
using Project.Scripts.GamePlay.Player.Abilities.General;
using UnityEngine;

namespace Project.Scripts.GamePlay.Player.Abilities.Configs
{    [CreateAssetMenu(fileName = "ActiveArmamentSpawnAbility", menuName = "Configs/Abilities/ActiveArmamentSpawnAbility")]

    public class ActiveArmamentSpawnAbilityConfig : ActionAbilityConfig
    {
        [field: SerializeField] public ArmamentType ArmamentType { get; private set; }
        public override IAbility CreateAbility(IAbilitiesFactory abilitiesFactory)
        {
            ActiveArmamentSpawnAbility ability = abilitiesFactory.CreateAbility<ActiveArmamentSpawnAbility>();
            ability.SetConfig(this);
            return ability;
        }
    }
}