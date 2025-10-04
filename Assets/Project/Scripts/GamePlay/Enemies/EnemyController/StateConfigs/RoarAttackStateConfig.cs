using System;
using Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs.Interfaces;
using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs
{
    [Serializable]
    public class RoarAttackStateConfig:IStateConfig
    {
        [field:SerializeField] public string AttackId { get;  set; }
        [field:SerializeField] public string AnimationName { get;  set; }


        [field: SerializeField] public float AttackRadius { get; private set; } = 2.4f;
        [field: SerializeField] public float AttackDamage { get; private set; } = 5;
        [field: SerializeField] public float AttackDuration { get; private set; } = 5;
        [field: SerializeField] public float StateDuration { get; private set; } = 6;
        [field: SerializeField] public bool Rotate { get; private set; } =true;
        
        public int AnimationHash=>Animator.StringToHash(AnimationName);
    }
}