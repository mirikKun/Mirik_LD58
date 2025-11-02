using Project.Scripts.GamePlay.Armaments.Enums;
using Project.Scripts.GamePlay.Player.Abilities.Factory;
using Project.Scripts.GamePlay.Player.Abilities.General;
using UnityEngine;

namespace Project.Scripts.GamePlay.Player.Abilities.Configs
{
    
    [CreateAssetMenu(fileName = "LandscapeArmamentSpawnAbilityConfig", menuName = "Configs/Abilities/LandscapeArmamentSpawnAbilityConfig")]
    public class LandscapeArmamentSpawnAbilityConfig : ActionAbilityConfig
    {
        [field: SerializeField] public ArmamentType ArmamentType { get; private set; }
        [field: SerializeField] public IndicatorType IndicatorType { get; private set; }
        [field: SerializeField] public float MaxStraightRange { get; private set; }
        [field: SerializeField] public float Offset { get; private set; }
        [field: SerializeField] public LayerMask RaycastLayerMask { get; private set; }
        public override IAbility CreateAbility(IAbilitiesFactory abilitiesFactory)
        {
            LandscapeArmamentSpawnAbility ability = abilitiesFactory.CreateAbility<LandscapeArmamentSpawnAbility>();
            ability.SetConfig(this);
            return ability;
        }
    }
}