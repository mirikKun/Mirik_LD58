using System.Collections.Generic;
using Project.Scripts.GamePlay.Enemies.EnemyController;
using UnityEngine;

namespace Project.Scripts.Utils.Extensions
{
    public static class EnemyControllerExtensions
    {
        public static List<Vector3> GetPositions(this List<EnemyEntity> enemyControllers)
        {
            List<Vector3> positions = new List<Vector3>();
            foreach (var enemy in enemyControllers)
            {
                positions.Add(enemy.transform.position);
            }

            return positions;
        }
    }
}