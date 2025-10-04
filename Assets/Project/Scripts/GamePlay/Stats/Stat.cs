using System;
using UnityEngine;

namespace Assets.Code.GamePlay.Stats
{
    [Serializable]
    public class Stat
    {
        [field:SerializeField] public StatType Type { get;private set; } = StatType.Attack;
        [field:SerializeField] public float Value { get;private set; } =0;
    }
}