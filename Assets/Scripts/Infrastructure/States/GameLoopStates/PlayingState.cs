using Player;
using Spawn.Ground;
using Spawn.InteractableObjects;
using Spawn.Walls;
using UI;
using Zenject;

namespace Infrastructure.States.GameLoopStates
{
    public class PlayingState : IState
    {
        
        private readonly PlayerComponentsEnabler player;
        private readonly PlayerHealth playerHealth;
        private readonly TimerForSpawnInteractableObjects timer;
        private readonly GroundTeleporter groundTeleporter;
        private readonly WallTeleporter wallTeleporter;
        private readonly LoadingCurtain loadingCurtain;

        [Inject]
        public PlayingState(PlayerComponentsEnabler player, PlayerHealth playerHealth, 
            TimerForSpawnInteractableObjects timer, GroundTeleporter groundTeleporter, WallTeleporter wallTeleporter, 
            LoadingCurtain loadingCurtain)
        {
            this.player = player;
            this.playerHealth = playerHealth;
            this.timer = timer;
            this.groundTeleporter = groundTeleporter;
            this.wallTeleporter = wallTeleporter;
            this.loadingCurtain = loadingCurtain;
        }
        
        public void Enter()
        {
            timer.enabled = true;
            loadingCurtain.Hide();
            groundTeleporter.Enable();
            wallTeleporter.Enable();
            player.EnableComponents();
            playerHealth.RestoreInitialHealth();
        }

        public void Exit()
        {
            
        }
    }
}