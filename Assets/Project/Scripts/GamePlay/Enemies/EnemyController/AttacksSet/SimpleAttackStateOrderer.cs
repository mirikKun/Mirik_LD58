using Project.Scripts.GamePlay.Enemies.EnemyController.StateConfigs;
using Project.Scripts.GamePlay.Enemies.EnemyController.StateConfigs.StateSetConfigs;

namespace Project.Scripts.GamePlay.Enemies.EnemyController.AttacksSet
{
    public class SimpleAttackStateOrderer:StateSetOrderer<SimpleAttackStateConfig,SimpleAttacksSetConfig>
    {
        public SimpleAttackStateOrderer(SimpleAttacksSetConfig setConfig) : base(setConfig)
        {
        }
    }
}