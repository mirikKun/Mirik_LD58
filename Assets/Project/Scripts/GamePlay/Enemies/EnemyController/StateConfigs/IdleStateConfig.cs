using System;
using Project.Scripts.GamePlay.Enemies.EnemyController.StateConfigs.Interfaces;
using UnityEngine;

namespace Project.Scripts.GamePlay.Enemies.EnemyController.StateConfigs
{
    [Serializable]
    public class IdleStateConfig:IStateConfig
    {
        [field:SerializeField] public string AnimationName { get; set; }
        [field:SerializeField] public float IdleDuration { get; private set; } = 1f;
        public int AnimationHash=>Animator.StringToHash(AnimationName);
    }
}