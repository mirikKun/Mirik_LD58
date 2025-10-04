using Assets.Code.GamePlay.Armaments.Projectiles.Enums;
using UnityEngine;

namespace Assets.Code.GamePlay.Armaments.Projectiles.Configs
{
    [CreateAssetMenu(fileName = "ProjectilesConfig", menuName = "Configs/Armament/ProjectilesConfig", order = 1)]
    public class ArmamentsConfig:ScriptableObject
    {
        [SerializeField] private ArmamentConfig[] _projectilesData;

        public ArmamentConfig GetProjectileData(ArmamentType type)
        {
            foreach (var projectileData in _projectilesData)
            {
                if (projectileData.Type == type)
                {
                    return projectileData;
                }
            }
            Debug.LogError($"Projectile data for type {type} not found.");
            return null;
         
        }
    }
}