using Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs;
using Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs.StateSetConfigs;

namespace Assets.Code.GamePlay.Enemies.EnemyController.AttacksSet
{
    public class SimpleAttackStateOrderer:StateSetOrderer<SimpleAttackStateConfig,SimpleAttacksSetConfig>
    {
        public SimpleAttackStateOrderer(SimpleAttacksSetConfig setConfig) : base(setConfig)
        {
        }
    }
}