using System;
using Project.Scripts.GamePlay.Armaments.Enums;
using UnityEngine;

namespace Project.Scripts.GamePlay.Armaments.Configs
{
    [CreateAssetMenu(fileName = "IndicatorsConfig", menuName = "Configs/Player/Abilities/IndicatorsConfig")]
    public class IndicatorsConfig : ScriptableObject
    {
        [SerializeField] private IndicatorData[] _indicators;

        public ArmamentIndicator GetIndicatorPrefab(IndicatorType type)
        {
            foreach (var indicator in _indicators)
            {
                if (indicator.Type == type)
                {
                    return indicator.Prefab;
                }
            }

            return null;
        }
    }

    [Serializable]
    public class IndicatorData
    {
        [field: SerializeField] public IndicatorType Type { get; private set; }
        [field: SerializeField] public ArmamentIndicator Prefab { get; private set; }
    }
}

