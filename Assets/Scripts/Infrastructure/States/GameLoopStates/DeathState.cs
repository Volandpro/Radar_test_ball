using Player;
using Spawn.InteractableObjects;
using Zenject;

namespace Infrastructure.States.GameLoopStates
{
    public class DeathState : IState
    {
        private readonly PlayerComponentsEnabler player;
        private readonly TimerForSpawnInteractableObjects timer;

        [Inject]
        private DeathState(PlayerComponentsEnabler player, TimerForSpawnInteractableObjects timer)
        {
            this.player = player;
            this.timer = timer;
        }
        public void Enter()
        {
            timer.enabled = false;
            player.DisableComponents();
        }

        public void Exit()
        {
            
        }
    }
}