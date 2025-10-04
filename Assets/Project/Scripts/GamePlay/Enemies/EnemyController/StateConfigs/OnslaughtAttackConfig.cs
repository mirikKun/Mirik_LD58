using System;
using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs
{
    [Serializable]
    public class OnslaughtAttackConfig
    {
        [field:SerializeField] public string AttackId { get; private set; }
        [field:SerializeField] public string AnimationName { get; private set; }
        [field: SerializeField] public float Speed { get; private set; } = 1.5f;
        [field: SerializeField] public float Range { get; private set; } = 4f;
        public int AnimationHash=>Animator.StringToHash(AnimationName);

    }
}