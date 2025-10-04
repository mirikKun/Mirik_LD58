using System;
using Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs.Interfaces;
using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs
{
    [Serializable]
    public class JumpAttackStateConfig:IStateConfig
    {
        [field: SerializeField] public string AttackId { get; private set; }
        [field: SerializeField] public string JumpStartAnimationName { get; private set; }
        [field:SerializeField] public float TimeToStartEndAnimation { get; private set; } = 3.5f;
        [field: SerializeField] public string JumpEndAnimationName { get; private set; }
        
        [field: SerializeField] public float JumpDuration { get; private set; } = 3f;
        [field: SerializeField] public float AttackDuration { get; private set; } = 3f;
        [field: SerializeField] public float JumpDelay { get; private set; } = 0.2f;
        [field: SerializeField] public float JumpHeight { get; private set; } = 2;
        [field: SerializeField] public AnimationCurve JumpSpeedCurve { get; private set; } 
        [field: SerializeField] public AnimationCurve JumpHeightCurve { get; private set; } 
        
        
        [field:SerializeField] public float TargetDistanceToPlayer { get; private set; } = 1.3f;
        [field:SerializeField] public float JumpAttackRange { get; private set; } = 8f;
        [field:SerializeField] public float JumpAttackMinRange { get; private set; } = 7.6f;
        public int JumpStartAnimationHash=>Animator.StringToHash(JumpStartAnimationName);
        public int JumpEndAnimationHash=>Animator.StringToHash(JumpEndAnimationName);

    }
}