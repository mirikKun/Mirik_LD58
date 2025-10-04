using System;
using Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs.Interfaces;
using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs
{
    [Serializable]
    public class ChasingStateConfig:IStateConfig
    {
        [field:SerializeField] public string AnimationName { get; private set; }
        [field:SerializeField] public float ChasingDuration { get; private set; } = 1f;
        [field:SerializeField] public float ChasingMaxDistance { get; private set; } = 10f;
        [field:SerializeField] public float MovingSpeed { get; private set; } = 4.5f;
        
        public int AnimationHash=>Animator.StringToHash(AnimationName);
    }
}