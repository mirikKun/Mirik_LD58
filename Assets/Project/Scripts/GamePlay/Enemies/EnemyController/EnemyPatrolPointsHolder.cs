using System.Collections.Generic;
using Assets.Code.GamePlay.Common.Entity;
using UnityEngine;

namespace Project.Scripts.GamePlay.Enemies.EnemyController
{
    public class EnemyPatrolPointsHolder:EntityComponent
    {
        [SerializeField] private List<Transform> _patrolPoints;
        public List<Transform>PatrolPoints => _patrolPoints;
    }
}