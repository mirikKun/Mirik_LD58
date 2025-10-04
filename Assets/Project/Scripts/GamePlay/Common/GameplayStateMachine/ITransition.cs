namespace Assets.Code.GamePlay.GameplayStateMachine {
    public interface ITransition {
        IState To { get; }
        IPredicate Condition { get; }
    }
}