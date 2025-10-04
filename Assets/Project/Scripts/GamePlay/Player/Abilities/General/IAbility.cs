using Assets.Code.GamePlay.Common.Entity;

namespace Assets.Code.GamePlay.Abilities.General
{
    public interface IAbility
    {
        void Init(ActorEntity caster);
        void OnInput(bool pressed);
        void Execute();
    }
}