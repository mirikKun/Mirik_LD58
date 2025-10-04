namespace Assets.Code.GamePlay.Enemies.EnemyController.AttacksSet
{
    public class InOrderStaticStateOrderStrategy:IStatesOrderStrategy
    {
        private int _statesCount;
        private int _currentState;
        public InOrderStaticStateOrderStrategy(int statesCount)
        {
            _statesCount = statesCount;
            _currentState =0;
        }
        public bool StateChosen(int stateIndex)
        {
            if(_currentState == stateIndex)
            {
                _currentState++;
                if(_currentState >= _statesCount)
                {
                    _currentState = 0;
                }
                return true;
            }
            return false;

        }
    }
}