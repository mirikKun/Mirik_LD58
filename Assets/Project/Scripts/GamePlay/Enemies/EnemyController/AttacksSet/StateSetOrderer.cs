using System;
using Assets.Code.GamePlay.Enemies.EnemyController.Enum;
using Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs.Interfaces;

namespace Assets.Code.GamePlay.Enemies.EnemyController.AttacksSet
{
    public class StateSetOrderer<TConfig,TConfigsSet> where TConfig:IStateConfig where TConfigsSet:IStatesSetConfig<TConfig>
    {
        private readonly TConfig[] _attackStateConfigs;

        private IStatesOrderStrategy _statesOrderStrategy;
        
        public StateSetOrderer(TConfigsSet setConfig)
        {
            
            _attackStateConfigs = setConfig.StateConfigs;
            
            int statesCount = _attackStateConfigs.Length;
            switch (setConfig.StateSetOrderType)
            {
                
                case StateSetOrderType.Random:
                    _statesOrderStrategy = new RandomStateOderStrategy(statesCount);
                    break;
                case StateSetOrderType.RandomNoRepeat:
                    _statesOrderStrategy = new RandomNoRepeatStateOrderStrategy(statesCount);
                    break;

                case StateSetOrderType.InOrderStatic:
                    _statesOrderStrategy = new InOrderStaticStateOrderStrategy(statesCount);
                    break;
                case StateSetOrderType.InOrderRandom:
                    _statesOrderStrategy = new InOrderRandomStateOrderStrategy(statesCount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public bool AttackChosen(int stateIndex)
        {
            return _statesOrderStrategy.StateChosen(stateIndex);
        }
      
    }
}