using Assets.Code.GamePlay.Enemies.EnemyController.Enum;

namespace Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs.Interfaces
{
    public interface IStatesSetConfig<TState> where TState : IStateConfig
    {
        public StateSetOrderType StateSetOrderType { get; }
        public TState[] StateConfigs { get; }
    }
}