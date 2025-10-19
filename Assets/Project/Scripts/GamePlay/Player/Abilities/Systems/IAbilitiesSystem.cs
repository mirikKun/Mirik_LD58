using System;
using System.Collections.Generic;
using Project.Scripts.GamePlay.Player.Abilities.Configs;
using Project.Scripts.GamePlay.Player.Abilities.General;

namespace Project.Scripts.GamePlay.Player.Abilities.Systems
{
    public interface IAbilitiesSystem
    {
        void Setup(PlayerStartAbilities playerStartAbilities);
        List<AbilityInstance> Abilities { get; }
        public event Action AbilitiesListChanged;
    }
}