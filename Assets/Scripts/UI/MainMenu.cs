using Infrastructure.States.GameStates;
using UnityEngine;
using Zenject;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        private GameStateMachine stateMachine;
        
        [Inject]
        public void Construct(GameStateMachine stateMachine) => 
            this.stateMachine = stateMachine;

        public void Quit() => 
            Application.Quit();

        public void Play() => 
            stateMachine.Enter<LoadingState>();
    }
}
