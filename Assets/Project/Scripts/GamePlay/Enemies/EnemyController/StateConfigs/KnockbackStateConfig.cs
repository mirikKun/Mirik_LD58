using System;
using UnityEngine;

namespace Project.Scripts.GamePlay.Enemies.EnemyController.StateConfigs
{
    [Serializable]
    public class KnockbackStateConfig
    {
        [field:SerializeField] public float Duration { get; private set; } = 2.2f;
    }
}