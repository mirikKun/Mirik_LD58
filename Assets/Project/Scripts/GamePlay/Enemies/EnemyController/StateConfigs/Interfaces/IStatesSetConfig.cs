using Project.Scripts.GamePlay.Enemies.EnemyController.Enum;

namespace Project.Scripts.GamePlay.Enemies.EnemyController.StateConfigs.Interfaces
{
    public interface IStatesSetConfig<TState> where TState : IStateConfig
    {
        public StateSetOrderType StateSetOrderType { get; }
        public TState[] StateConfigs { get; }
    }
}