using System.Collections.Generic;

namespace Assets.Code.GamePlay.GameplayStateMachine
{
    public struct StateConfiguration
    {
        public IState State;
        public int Index;
        public List<TransitionConfiguration> Transitions;
    }


}