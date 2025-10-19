using System;
using Project.Scripts.GamePlay.Enemies.EnemyController.StateConfigs.Interfaces;
using Project.Scripts.GamePlay.Player.Abilities.Configs;
using UnityEngine;

namespace Project.Scripts.GamePlay.Enemies.EnemyController.StateConfigs
{
    [Serializable]

    public class ArmamentSpawnStateConfig:IStateConfig
    {
        [field: SerializeField] public float AttackDuration { get; private set; } = 1f;
        [field: SerializeField] public float AttackRange { get; private set; } = 2.4f;
        [field: SerializeField] public bool NeedToRotate { get; private set; } = true;
        [field: SerializeField] public float RotationSpeed { get; private set; } = 1.3f;
        [field: SerializeField] public float RotationDuration { get; private set; } = 0.4f;
        [field: SerializeField] public float ReloadDuration { get; private set; } = 2.4f;
        [field: SerializeField] public ArmamentSpawnAbilityConfig ArmamentSpawnAbilityConfig{ get; private set; }

    }
}