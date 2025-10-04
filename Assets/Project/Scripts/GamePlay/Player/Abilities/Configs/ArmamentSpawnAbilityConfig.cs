using Assets.Code.GamePlay.Abilities.General;
using Assets.Code.GamePlay.Armaments.Projectiles.Enums;
using Assets.Code.GamePlay.Player.Abilities.Configs;
using Assets.Code.GamePlay.Player.Abilities.Factory;
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