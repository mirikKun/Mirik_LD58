using System.Collections.Generic;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.GameplayStateMachine;
using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController.EnemyConfigs
{
    public abstract class EnemyBehaviorConfig:ScriptableObject
    {
        public abstract List<StateConfiguration> GetConfigurations(ActorEntity enemyController);

    }
}