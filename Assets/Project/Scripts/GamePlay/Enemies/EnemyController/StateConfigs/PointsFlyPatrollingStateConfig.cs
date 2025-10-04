using System;
using System.Collections.Generic;
using Assets.Code.Common.Utils;
using Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs.Interfaces;
using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs
{
    [Serializable]

    public class PointsFlyPatrollingStateConfig:IStateConfig
    {
        [field:SerializeField] public string AnimationName { get; private set; }
        [field:SerializeField ,FloatRangeSlider(0f,10f)] public FloatRange Speed { get; private set; } = 2.5f;
        [field:SerializeField ,FloatRangeSlider(0f,10f)] public FloatRange RotationSpeed { get; private set; } = 21.5f;
        [field:SerializeField] public List<Transform> PatrolPoints{ get; private set; } 
    }
}