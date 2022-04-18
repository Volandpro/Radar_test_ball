using System;
using Infrastructure.ActualProviders;
using Infrastructure.States.GameLoopStates;
using Zenject;
using Zenject.SceneContexts;

namespace Player
{
    public class PlayerHealth
    {
        private const int maxHealth = 3;
        public int MaxHealth { get { return maxHealth; } }
        public int currentHealth;

        public Action OnDead;
        public Action OnHealthChanged;
        
        private GameLoopStateMachine stateMachine;

        [Inject]
        public PlayerHealth(ActualGameLoopStateMachineProvider gameLoopStateMachineProvider)
        {
            gameLoopStateMachineProvider.OnStateMachineSet += SetStateMachine;
            OnDead += EnterDeathState;
        }

        private void SetStateMachine(GameLoopStateMachine stateMachine) => 
            this.stateMachine = stateMachine;

        public void RestoreInitialHealth()
        {
            currentHealth = maxHealth;
            OnHealthChanged?.Invoke();
        }

        public void MinusHealth(int value)
        {
            currentHealth -= value;
            OnHealthChanged?.Invoke();
            if(currentHealth==0)
                OnDead?.Invoke();
        }

        private void EnterDeathState() => 
            stateMachine.Enter<DeathState>();
    }
}