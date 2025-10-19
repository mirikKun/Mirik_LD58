using Assets.Code.GamePlay.Common.Entity;
using Project.Scripts.GamePlay.Armaments;
using Project.Scripts.GamePlay.Common.Physic;
using Project.Scripts.GamePlay.Player.Abilities.General;
using Project.Scripts.GamePlay.Player.PlayerStateMachine.StateConfigs;

namespace Project.Scripts.GamePlay.Player.PlayerStateMachine.States
{
    public class DashAttackState : DashState
    {
        private DashAttackStateConfig _attackConfig;
        private Armament _armament;
        public DashAttackState(ActorEntity player, DashBaseStateConfig config, AbilityInstance abilitiesInstance) : base(player, config, abilitiesInstance){ }

        public DashAttackState(ActorEntity player, DashAttackStateConfig config, AbilityInstance abilitiesInstance) :
            base(player, config, abilitiesInstance)
        {
            _attackConfig = config;
        }


        public override void OnEnter()
        {
            base.OnEnter();
            _armament = _player.Get<ArmamentsHolder>().CreateArmament(_attackConfig.ArmamentConfig);
            _player.Get<LayerChanger>().ChangeLayerToIntangible();
        }

        public override void OnExit()
        {
            base.OnExit();
            _player.Get<ArmamentsHolder>().RemoveArmament(_armament);
            _player.Get<LayerChanger>().ChangeLayerToDefault();

        }
    }
}