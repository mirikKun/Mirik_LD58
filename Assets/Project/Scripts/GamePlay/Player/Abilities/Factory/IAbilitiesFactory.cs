using Assets.Code.GamePlay.Abilities.General;

namespace Assets.Code.GamePlay.Player.Abilities.Factory
{
    public interface IAbilitiesFactory
    {
        T CreateAbility<T>() where T:IAbility;
    }
}