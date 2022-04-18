using Infrastructure.Services;
using Infrastructure.States.GameStates;
using UnityEngine;
using Zenject;

namespace Infrastructure.Misc
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private GameStateMachine stateMachine;
        
        [Inject]
        public void Construct(GameStateMachine stateMachine) => 
            this.stateMachine = stateMachine;

        private void Awake() 
        {
            stateMachine.Enter<BootStrapState>();
            DontDestroyOnLoad(this);
        }
    }
}
