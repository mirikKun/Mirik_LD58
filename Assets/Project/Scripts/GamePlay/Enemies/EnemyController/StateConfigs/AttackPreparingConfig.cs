using System;
using Assets.Code.Common.Utils;
using Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs.Interfaces;
using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs
{
    [Serializable]
    public class AttackPreparingConfig:IStateConfig
    {
        [field:SerializeField] public string AnimationName { get; private set; }
        [field:SerializeField,FloatRangeSlider(0,10)] public FloatRange Speed { get; private set; } = 1.5f;
        [field:SerializeField,FloatRangeSlider(0,10)] public FloatRange Range { get; private set; } = 4f;
        [field:SerializeField,FloatRangeSlider(0,20)] public FloatRange MaxRange { get; private set; } = 8f;
        [field:SerializeField] public float ReturnDuration { get; private set; } = 1f;
        public int AnimationHash=>Animator.StringToHash(AnimationName);

    }
}   