using System.Collections.Generic;
using Assets.Code.GamePlay.Abilities.General;
using Assets.Code.GamePlay.DataDriven.Effects;
using Assets.Code.GamePlay.DataDriven.Statuses;
using Assets.Code.GamePlay.Player.Abilities.Factory;
using UnityEngine;

namespace Assets.Code.GamePlay.Player.Abilities.Configs
{
    [CreateAssetMenu(fileName = "SelfStatusApplierAbility", menuName = "Configs/Abilities/SelfStatusApplierAbility")]
    public class SelfEffectApplierAbilityConfig:ActionAbilityConfig
    {
        [field:SerializeReference] public List<Effect> Effects { get; private set; } = new List<Effect>();

        public override IAbility CreateAbility(IAbilitiesFactory abilitiesFactory)
        {
            SelfEffectApplierAbility ability=abilitiesFactory.CreateAbility<SelfEffectApplierAbility>();
            ability.SetConfig(this);
            return ability;
        }
    }
}