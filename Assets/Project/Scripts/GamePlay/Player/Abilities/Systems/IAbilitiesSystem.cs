using System;
using System.Collections.Generic;
using Assets.Code.GamePlay.Abilities.Configs;
using Assets.Code.GamePlay.Abilities.General;

namespace Assets.Code.GamePlay.Abilities.Systems
{
    public interface IAbilitiesSystem
    {
        void Setup(PlayerStartAbilities playerStartAbilities);
        List<AbilityInstance> Abilities { get; }
        public event Action AbilitiesListChanged;
    }
}