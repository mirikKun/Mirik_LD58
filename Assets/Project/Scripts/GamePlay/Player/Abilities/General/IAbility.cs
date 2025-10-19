using Assets.Code.GamePlay.Common.Entity;

namespace Project.Scripts.GamePlay.Player.Abilities.General
{
    public interface IAbility
    {
        void Init(ActorEntity caster);
        void OnInput(bool pressed);
        void Execute();
    }
}