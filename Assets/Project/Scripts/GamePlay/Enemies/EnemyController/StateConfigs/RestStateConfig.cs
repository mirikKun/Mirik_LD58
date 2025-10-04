using System;
using Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs.Interfaces;
using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs
{
    [Serializable]

    public class RestStateConfig:IStateConfig
    {
        [field:SerializeField] public string AnimationName { get; set; }
        [field:SerializeField] public float RestDuration { get; private set; } = 1f;
        public int AnimationHash=>Animator.StringToHash(AnimationName);
    }
}