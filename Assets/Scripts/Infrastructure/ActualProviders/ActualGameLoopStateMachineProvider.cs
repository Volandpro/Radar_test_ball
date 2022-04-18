using System;
using Infrastructure.States.GameLoopStates;

namespace Infrastructure.ActualProviders
{
    public class ActualGameLoopStateMachineProvider 
    {
        public Action<GameLoopStateMachine> OnStateMachineSet;
        public void SetStateMachine(GameLoopStateMachine gameLoopStateMachine) => 
            OnStateMachineSet?.Invoke(gameLoopStateMachine);
    }
}