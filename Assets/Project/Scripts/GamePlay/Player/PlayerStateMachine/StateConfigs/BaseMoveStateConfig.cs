using System.Collections.Generic;
using Assets.Code.GamePlay.Abilities.General;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.GameplayStateMachine;
using UnityEngine;

namespace Assets.Code.GamePlay.Player.PlayerStateMachine.StateConfigs
{
    public abstract class BaseMoveStateConfig:ScriptableObject
    {
        public abstract List<StateConfiguration> GetStateConfiguration(ActorEntity playerEntity,
            AbilityInstance abilitiesInstance);
        
    }
}