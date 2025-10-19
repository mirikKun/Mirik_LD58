using System;
using Project.Scripts.GamePlay.Enemies.EnemyController.Enum;
using Project.Scripts.GamePlay.Enemies.EnemyController.StateConfigs.Interfaces;
using UnityEngine;

namespace Project.Scripts.GamePlay.Enemies.EnemyController.StateConfigs.StateSetConfigs
{
    [Serializable]
    public class SimpleAttacksSetConfig:IStatesSetConfig<SimpleAttackStateConfig>
    {
        [field:SerializeField] public StateSetOrderType StateSetOrderType { get; private set; }
        
        [field:SerializeField] public  SimpleAttackStateConfig[] StateConfigs { get; private set; }

    }
}