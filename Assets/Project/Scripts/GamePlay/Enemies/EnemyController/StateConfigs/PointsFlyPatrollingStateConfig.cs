using System;
using Project.Scripts.GamePlay.Enemies.EnemyController.StateConfigs.Interfaces;
using Project.Scripts.Utils;
using UnityEngine;

namespace Project.Scripts.GamePlay.Enemies.EnemyController.StateConfigs
{
    [Serializable]

    public class PointsFlyPatrollingStateConfig:IStateConfig
    {
        [field:SerializeField] public string AnimationName { get; private set; }
        [field:SerializeField ,FloatRangeSlider(0f,10f)] public FloatRange Speed { get; private set; } = 2.5f;
        [field:SerializeField ,FloatRangeSlider(0f,10f)] public float Acceleration { get; private set; } = 3.5f;
        [field:SerializeField] public float RotationSpeed { get; private set; } = 21.5f;
        [field:SerializeField ] public float ReachThreshold { get; private set; } =0.2f;
    }
}