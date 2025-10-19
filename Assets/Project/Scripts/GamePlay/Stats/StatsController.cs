using Assets.Code.GamePlay.Common.Entity;
using Project.Scripts.GamePlay.Stats.Configs;
using UnityEngine;

namespace Project.Scripts.GamePlay.Stats
{
    public class StatsController : EntityComponent
    {
        [SerializeField] private BaseStatsConfig _baseStatsConfig;
        private readonly StatsMediator _mediator = new StatsMediator();

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