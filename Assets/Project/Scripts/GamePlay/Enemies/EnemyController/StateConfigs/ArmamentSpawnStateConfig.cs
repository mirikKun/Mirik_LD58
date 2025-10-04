using System;
using Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs.Interfaces;
using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs
{
    [Serializable]

    public class ArmamentSpawnStateConfig:IStateConfig
    {
        [field: SerializeField] public float PreAttackDelay { get; private set; } = 1f;
        [field: SerializeField] public float AttackRange { get; private set; } = 2.4f;
        [field: SerializeField] public bool NeedToRotate { get; private set; } = true;
        [field: SerializeField] public float AttackAngle { get; private set; } = 20;
        [field: SerializeField] public float RotationDuration { get; private set; } = 0.4f;
        [field: SerializeField] public float ReloadDuration { get; private set; } = 2.4f;

    }
}