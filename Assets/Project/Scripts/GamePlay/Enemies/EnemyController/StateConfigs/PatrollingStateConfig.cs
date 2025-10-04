using System;
using Assets.Code.Common.Utils;
using Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs.Interfaces;
using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs
{
    [Serializable]
    public class PatrollingStateConfig:IStateConfig
    {
        [field:SerializeField] public string AnimationName { get; private set; }
        [field:SerializeField ,FloatRangeSlider(0f,10f)] public FloatRange Speed { get; private set; } = 2.5f;
        [field:SerializeField] public float Range { get; private set; } = 9f;
      
        
        public int AnimationHash=>Animator.StringToHash(AnimationName);

    }
}