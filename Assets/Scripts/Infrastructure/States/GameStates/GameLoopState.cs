using Infrastructure.ActualProviders;
using Infrastructure.States.GameLoopStates;
using Zenject;
using Zenject.SceneContexts;

namespace Infrastructure.States.GameStates
{
    public class GameLoopState : IState
    {
        private GameLoopStateMachine gameLoopStateMachine;

        [Inject]
        public GameLoopState(ActualGameLoopStateMachineProvider gameLoopStatesProvider) => 
            gameLoopStatesProvider.OnStateMachineSet += SetGameLoopStateMachine;

        private void SetGameLoopStateMachine(GameLoopStateMachine gameLoopStateMachine)
        {
            this.gameLoopStateMachine = gameLoopStateMachine;
            gameLoopStateMachine.Enter<LevelCreationState>();
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }
    }
}