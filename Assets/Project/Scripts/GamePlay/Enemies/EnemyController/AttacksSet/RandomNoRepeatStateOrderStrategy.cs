using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController.AttacksSet
{
    public class RandomNoRepeatStateOrderStrategy:IStatesOrderStrategy
    {
        private int _statesCount;
        private int _currentState;
        public RandomNoRepeatStateOrderStrategy(int statesCount)
        {
            _statesCount = statesCount;
            _currentState = Random.Range(0, _statesCount);
        }
        public bool StateChosen(int stateIndex)
        {
            if(_statesCount == 1)
            {
                return true;
            }
            if(_currentState == stateIndex)
            {
                int newStateIndex= Random.Range(0, _statesCount);
                while(newStateIndex == _currentState)
                {
                    newStateIndex = Random.Range(0, _statesCount);
                }
                _currentState = newStateIndex;
                return true;
            }
            return false;

        }
    }
}