using System;
using Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs.Interfaces;
using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs
{
    [Serializable]
    public class AttackPreparingIdleConfig:IStateConfig
    {
        [field:SerializeField] public string AnimationName { get; set; }
        [field:SerializeField] public float IdleDuration { get; private set; } = 1f;
        public int AnimationHash=>Animator.StringToHash(AnimationName);

    }
}