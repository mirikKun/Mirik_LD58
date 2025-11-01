using System;
using Project.Scripts.GamePlay.Enemies.EnemyController.StateConfigs.Interfaces;
using UnityEngine;

namespace Project.Scripts.GamePlay.Enemies.EnemyController.StateConfigs
{
    [Serializable]
    public class GravityFallStunStateConfig:IStateConfig
    {
        [field:SerializeField ] public float StunDuration { get; private set; } =4.2f;
    }
}