using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController.AttacksSet
{
    public class RandomStateOderStrategy:IStatesOrderStrategy
    {
        private int _statesCount;
        private int _currentState;
        public RandomStateOderStrategy(int statesCount)
        {
            _statesCount = statesCount;
            _currentState = Random.Range(0, _statesCount);
        }
        public bool StateChosen(int stateIndex)
        {
            if(_currentState == stateIndex)
            {
                _currentState= Random.Range(0, _statesCount);
                return true;
            }
            return false;

        }
    }
}