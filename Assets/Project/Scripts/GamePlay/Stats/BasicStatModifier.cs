using System;
using Assets.Code.GamePlay.Stats;

namespace Project.Scripts.GamePlay.Stats
{
    public class BasicStatModifier : StatModifier
    {
        private readonly Func<float, float> _operation;

        public BasicStatModifier(StatType statType, float duration, Func<float, float> operation) : base(statType,duration)
        {
            this._operation = operation;
        }

        public override void Handle(object sender, Query query)
        {
            if (query.StatType == StatType)
            {
                query.Value = _operation(query.Value);
            }
        }
    }
}