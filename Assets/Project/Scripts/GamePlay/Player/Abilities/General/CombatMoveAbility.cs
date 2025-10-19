using Assets.Code.GamePlay.Common.Entity;
using Project.Scripts.GamePlay.Player.Abilities.Configs;

namespace Project.Scripts.GamePlay.Player.Abilities.General
{
    public class CombatMoveAbility : IAbility
    {
        private CombatMoveAbilityConfig _config;
        private ActorEntity _caster;

        public void SetConfig(CombatMoveAbilityConfig config)
        {
            _config = config;
        }

        public void Init(ActorEntity caster)
        {
            _caster = caster;
        }

        public void OnInput(bool pressed)
        {
            if (pressed)
            {
                Execute();
            }
        }

        public void Execute()
        {
        
           
        }

        public void Stop()
        {
            
        }
    }
}