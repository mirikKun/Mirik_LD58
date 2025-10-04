using Assets.Code.GamePlay.Abilities.General;
using Assets.Code.GamePlay.Player.Abilities.Factory;

namespace Assets.Code.GamePlay.Player.Abilities.Configs
{
    public abstract class ActionAbilityConfig : BaseAbilityConfig
    {
        public abstract IAbility CreateAbility(IAbilitiesFactory abilitiesFactory);
    }
}