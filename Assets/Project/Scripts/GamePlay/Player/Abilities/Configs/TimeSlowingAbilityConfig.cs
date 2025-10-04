using Assets.Code.GamePlay.Abilities.General;
using Assets.Code.GamePlay.Player.Abilities.Factory;
using UnityEngine;

namespace Assets.Code.GamePlay.Player.Abilities.Configs
{
    [CreateAssetMenu(fileName = "TimeSlowingAbility", menuName = "Configs/Abilities/TimeSlowingAbility")]

    public class TimeSlowingAbilityConfig:ActionAbilityConfig
    {
        [field:SerializeField] public float Duration { get; private set; } = 2f;
        [field:SerializeField] public AnimationCurve TimeSlowCurve { get; private set; } = AnimationCurve.EaseInOut(0, 1, 1, 0.5f);
        
   
        public override IAbility CreateAbility(IAbilitiesFactory abilitiesFactory)
        {
            TimeSlowAbility ability=abilitiesFactory.CreateAbility<TimeSlowAbility>();
            ability.SetConfig(this);
            return ability;
        }
    }
}