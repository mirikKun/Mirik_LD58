using System;
using UnityEngine;

namespace Assets.Code.GamePlay.DataDriven.Statuses
{
    [Serializable]
    public class StatusConfig
    {
        [field: SerializeField] public StatusType StatusType { get; private set; }
        [field: SerializeField] public float Value { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
    }
}