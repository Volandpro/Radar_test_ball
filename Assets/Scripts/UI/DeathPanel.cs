using Infrastructure.ActualProviders;
using Infrastructure.States.GameLoopStates;
using Player;
using UnityEngine;
using Zenject;
using Zenject.SceneContexts;

namespace UI
{
    public class DeathPanel : MonoBehaviour
    {
        private GameLoopStateMachine stateMachine;
        [SerializeField] private GameObject deathPanel;
        
        [Inject]
        public void Construct(ActualGameLoopStateMachineProvider gameLoopStateMachineProvider,PlayerHealth playerHealth)
        {
            playerHealth.OnDead += ShowDeathPanel;
            gameLoopStateMachineProvider.OnStateMachineSet += SetStateMachine;
        }

        private void ShowDeathPanel() => 
            deathPanel.SetActive(true);

        private void HideDeathPanel() => 
            deathPanel.SetActive(false);

        private void SetStateMachine(GameLoopStateMachine stateMachine) => 
            this.stateMachine = stateMachine;

        public void Replay()
        {
            stateMachine.Enter<LevelCreationState>();
            HideDeathPanel();
        }

        public void Quit() => 
            Application.Quit();
    }
}
