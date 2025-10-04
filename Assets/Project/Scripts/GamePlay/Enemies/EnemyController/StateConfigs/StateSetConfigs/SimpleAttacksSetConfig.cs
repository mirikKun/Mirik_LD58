using System;
using Assets.Code.GamePlay.Enemies.EnemyController.Enum;
using Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs.Interfaces;
using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs.StateSetConfigs
{
    [Serializable]
    public class SimpleAttacksSetConfig:IStatesSetConfig<SimpleAttackStateConfig>
    {
        [field:SerializeField] public StateSetOrderType StateSetOrderType { get; private set; }
        
        [field:SerializeField] public  SimpleAttackStateConfig[] StateConfigs { get; private set; }

    }
}