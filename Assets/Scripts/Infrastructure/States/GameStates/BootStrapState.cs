using Infrastructure.Services;
using Zenject;

namespace Infrastructure.States.GameStates
{
    public class BootStrapState : IState
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly SceneLoader sceneLoader;

        [Inject]
        public BootStrapState(GameStateMachine gameStateMachine,SceneLoader sceneLoader)
        {
            this.gameStateMachine = gameStateMachine;
            this.sceneLoader = sceneLoader;
        }

        public void Enter() => 
            sceneLoader.Load(1,LoadedScene);

        public void Exit()
        {
        }

        private void LoadedScene() => 
            gameStateMachine.Enter<MainMenuState>();
    }
}