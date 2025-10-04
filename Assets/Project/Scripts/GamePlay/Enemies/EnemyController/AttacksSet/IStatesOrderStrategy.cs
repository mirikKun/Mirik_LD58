namespace Assets.Code.GamePlay.Enemies.EnemyController.AttacksSet
{
    public interface IStatesOrderStrategy
    {
        bool StateChosen(int stateIndex);
    }
}