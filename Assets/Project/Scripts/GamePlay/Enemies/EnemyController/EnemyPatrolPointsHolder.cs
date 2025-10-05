using System.Collections.Generic;
using Assets.Code.GamePlay.Common.Entity;
using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController
{
    public class EnemyPatrolPointsHolder:EntityComponent
    {
        [SerializeField] private List<Transform> _patrolPoints;
        public List<Transform>PatrolPoints => _patrolPoints;
    }
}