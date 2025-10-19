using System.Collections.Generic;
using Assets.Code.GamePlay.Common.Entity;
using Project.Scripts.GamePlay.Common.GameplayStateMachine;
using UnityEngine;

namespace Project.Scripts.GamePlay.Enemies.EnemyController.EnemyConfigs.BehaviourConfigs
{
    public abstract class EnemyBehaviorConfig:ScriptableObject
    {
        public abstract List<StateConfiguration> GetConfigurations(ActorEntity enemyController);

    }
}