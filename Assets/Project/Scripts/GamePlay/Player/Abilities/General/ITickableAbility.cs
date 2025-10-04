namespace Assets.Code.GamePlay.Abilities.General
{
    public interface ITickableAbility:IAbility
    {
        void Tick(float deltaTime);
    }
}