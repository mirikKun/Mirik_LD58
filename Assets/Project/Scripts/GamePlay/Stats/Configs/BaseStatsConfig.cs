using System;
using System.Linq;
using Assets.Code.GamePlay.Stats;
using UnityEngine;

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