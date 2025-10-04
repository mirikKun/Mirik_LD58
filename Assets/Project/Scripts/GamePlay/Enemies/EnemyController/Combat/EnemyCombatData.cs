using System;
using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController.Combat
{
    [Serializable]
    public class EnemyCombatData
    {
        [field:SerializeField]public bool CanAttack { get; set; }
        [field:SerializeField]public bool  HasDetectedCharacter { get; set; }
    }
}