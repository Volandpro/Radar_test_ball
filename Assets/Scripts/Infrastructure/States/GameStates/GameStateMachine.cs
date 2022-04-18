namespace Infrastructure.States.GameStates
{
    public class GameStateMachine
    {
        private IState activeState;
        public StatesContainer container;

        public GameStateMachine(StatesContainer container) => 
            this.container = container;

        public void Enter<TState>() where TState : IState
        {
            activeState?.Exit();
            activeState = container.allStates[typeof(TState)];
            activeState.Enter();
        }
    }
}
