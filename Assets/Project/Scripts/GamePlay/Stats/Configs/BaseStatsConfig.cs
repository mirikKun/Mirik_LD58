using System.Linq;
using UnityEngine;

namespace Project.Scripts.GamePlay.Stats.Configs
{
    [CreateAssetMenu (fileName = "BaseStatsConfig", menuName = "Configs/Stats/BaseStatsConfig")]
    public class BaseStatsConfig : ScriptableObject
    {
        [field: SerializeField] public Stat[] BaseStats { get; private set; }
    
        public float this[StatType statType]
        {
            get 
            {
                return BaseStats.FirstOrDefault(x=> x.Type == statType)?.Value ?? -1f;
            }
        }
    }
}