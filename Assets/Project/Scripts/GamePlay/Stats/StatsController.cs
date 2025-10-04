using Assets.Code.GamePlay.Common.Entity;
using UnityEngine;

namespace Assets.Code.GamePlay.Stats
{
    public class StatsController:EntityComponent
    {
        private readonly StatsMediator _mediator = new StatsMediator();
        [SerializeField]private BaseStatsConfig _baseStatsConfig;

        public StatsMediator Mediator => _mediator;

        public float this[StatType statType]
        {
            get 
            {
                var q = new Query(statType, _baseStatsConfig[statType]);
                _mediator.PerformQuery(this, q);
                return q.Value;
            }
        }
        // public StatsController(StatsMediator mediator, BaseStatsConfig baseStatsConfig)
        // {
        //     this._mediator = mediator;
        //     this._baseStatsConfig = baseStatsConfig;
        // }

    }
}