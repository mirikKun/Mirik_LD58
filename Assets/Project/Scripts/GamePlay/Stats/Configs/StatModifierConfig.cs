using System;
using UnityEngine;

namespace Assets.Code.GamePlay.Stats
{
    [Serializable]
    public class StatModifierConfig
    {
        [field: SerializeField] public Stat Stat { get; private set; }
        
        [field: SerializeField] public StatOperatorType OperatorType { get; private set; }= StatOperatorType.Add;
        [field: SerializeField] public float  Duration { get; private set; }= 5f;

    }
}