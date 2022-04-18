using Infrastructure.Services;
using Zenject;

namespace Infrastructure.States.GameStates
{
    public class LoadingState : IState
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly SceneLoader sceneLoader;
        
        [Inject]
        public LoadingState(GameStateMachine gameStateMachine,SceneLoader sceneLoader)
        {
            this.gameStateMachine = gameStateMachine;
            this.sceneLoader = sceneLoader;
        }
        public void Enter() =>
            sceneLoader.Load(2,LoadedScene);

        public void Exit()
        {
           
        }

        private void LoadedScene() => 
            gameStateMachine.Enter<GameLoopState>();
    }
}