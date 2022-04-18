using Infrastructure.States.GameStates;

namespace Infrastructure.States.GameLoopStates
{
    public class GameLoopStateMachine
    {
        private IState activeState;
        public StatesContainer container;
        
        public GameLoopStateMachine(StatesContainer container) => 
            this.container = container;

        public void Enter<TState>() where TState : IState
        {
            activeState?.Exit();
            activeState = container.allStates[typeof(TState)];
            activeState.Enter();
        }
    }
}