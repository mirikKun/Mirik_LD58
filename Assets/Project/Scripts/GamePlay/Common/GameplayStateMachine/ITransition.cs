namespace Project.Scripts.GamePlay.Common.GameplayStateMachine {
    public interface ITransition {
        IState To { get; }
        IPredicate Condition { get; }
    }
}