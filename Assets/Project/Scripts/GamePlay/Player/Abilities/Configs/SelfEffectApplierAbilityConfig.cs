using System.Collections.Generic;
using Assets.Code.GamePlay.DataDriven.Effects;
using FMODUnity;
using Project.Scripts.GamePlay.Player.Abilities.Factory;
using Project.Scripts.GamePlay.Player.Abilities.General;
using UnityEngine;

namespace Project.Scripts.GamePlay.Player.Abilities.Configs
{
    [CreateAssetMenu(fileName = "SelfStatusApplierAbility", menuName = "Configs/Abilities/SelfStatusApplierAbility")]
    public class SelfEffectApplierAbilityConfig:ActionAbilityConfig
    {
        [field:SerializeReference] public List<Effect> Effects { get; private set; } = new List<Effect>();
        [field:Header("Sounds")]
        [field: SerializeField] public EventReference Sound { get; private set; }
        public override IAbility CreateAbility(IAbilitiesFactory abilitiesFactory)
        {
            SelfEffectApplierAbility ability=abilitiesFactory.CreateAbility<SelfEffectApplierAbility>();
            ability.SetConfig(this);
            return ability;
        }
    }
}