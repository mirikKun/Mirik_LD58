namespace Project.Scripts.GamePlay.Player.Abilities.General
{
    public interface ITickableAbility:IAbility
    {
        void Tick(float deltaTime);
    }
}