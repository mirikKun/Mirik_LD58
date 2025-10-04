using System.Collections.Generic;
using ImprovedTimers;
using Project.Scripts.Utils.Extensions;

namespace Assets.Code.GamePlay.Enemies.EnemyController.AttacksSet
{
    public class InOrderRandomStateOrderStrategy:IStatesOrderStrategy
    {
        private List<int> _statesOrder;
        private int _statesCount;
        private int _currentState;
        public InOrderRandomStateOrderStrategy(int statesCount)
        {
            _statesCount = statesCount;
            _statesOrder = new List<int>(statesCount);
            for (int i = 0; i < statesCount; i++)
            {
                _statesOrder.Add(i);
            }
            _statesOrder.Shuffle();
        }
        public bool StateChosen(int stateIndex)
        {
            if (stateIndex == _statesOrder[_currentState])
            {
                _currentState++;
                if (_currentState == _statesCount)
                {
                    _currentState = 0;
                }
                return true;
            }
            return false;
        }
    }
}