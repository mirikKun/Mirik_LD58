using System.Collections.Generic;

namespace Project.Scripts.GamePlay.Common.GameplayStateMachine
{
    public struct StateConfiguration
    {
        public IState State;
        public int Index;
        public List<TransitionConfiguration> Transitions;
    }


}