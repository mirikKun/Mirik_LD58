using System;
using Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs.Interfaces;
using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs
{
    [Serializable]
    public class SimpleAttackStateConfig:IStateConfig
    {
        [field:SerializeField] public string AttackId { get;  set; }
        [field:SerializeField] public bool ShowPreAttackMark { get;  set; }
        [field:SerializeField] public string AnimationName { get;  set; }


        [field: SerializeField] public float AttackRange { get; private set; } = 2.4f;
        [field: SerializeField] public float AttackAngle { get; private set; } = 20;
        [field: SerializeField] public float AttackDuration { get; private set; } = 3;
        [field: SerializeField] public float RotationDuration { get; private set; } = 0.4f;
        [field: SerializeField] public float AttackCooldown { get; private set; } = 1;
        [field: SerializeField] public float AttackDamage { get; private set; } = 5;
        
        public int AnimationHash=>Animator.StringToHash(AnimationName);
    }
}