namespace Project.Scripts.GamePlay.Common.GameplayStateMachine {
    public interface IState {
        void Update(float deltaTime) { }
        void FixedUpdate(float fixedDeltaTime) { }
        void OnEnter() { }
        void OnExit() { }
        void Dispose() { }
    }
}