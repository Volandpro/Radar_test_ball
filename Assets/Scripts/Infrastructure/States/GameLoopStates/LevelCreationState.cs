using System.Collections.Generic;
using Infrastructure.ActualProviders;
using Infrastructure.Services;
using Player;
using Spawn;
using Spawn.Ground;
using Spawn.InteractableObjects;
using Spawn.Walls;
using UI;
using Zenject;

namespace Infrastructure.States.GameLoopStates
{
    public class LevelCreationState : IState
    {

        private readonly PlayerComponentsEnabler player;
        private readonly ActualSpawnersProvider spawnersProvider;
        private readonly GameLoopStateMachine gameLoopStateMachine;
        private readonly PlayerSpawnPoint spawnPoint;
        private readonly TimerForSpawnInteractableObjects timer;
        private readonly GroundTeleporter groundTeleporter;
        private readonly WallTeleporter wallTeleporter;
        private readonly Score score;
        private readonly LoadingCurtain loadingCurtain;
        private List<IInitialSpawner> allInitialSpawners = new List<IInitialSpawner>();
        private List<IInitialSpawner> activeInitialSpawners = new List<IInitialSpawner>();
        
        [Inject]
        public LevelCreationState(PlayerComponentsEnabler player,GameLoopStateMachine gameLoopStateMachine, 
            ActualSpawnersProvider spawnersProvider, TimerForSpawnInteractableObjects timer, 
            PlayerSpawnPoint spawnPoint,GroundTeleporter groundTeleporter, WallTeleporter wallTeleporter, Score score,LoadingCurtain loadingCurtain)
        {
            this.player = player;
            this.gameLoopStateMachine = gameLoopStateMachine;
            this.spawnersProvider = spawnersProvider;
            this.timer = timer;
            this.spawnPoint = spawnPoint;
            this.groundTeleporter = groundTeleporter;
            this.wallTeleporter = wallTeleporter;
            this.score = score;
            this.loadingCurtain = loadingCurtain;
            this.spawnersProvider.OnSpawnersSet += GetSpawners;
            this.spawnersProvider.InvokeAction();
        }
        public void Enter()
        {
            SetGameServicesToInitialState();
            SetUpPlayer();
            FillActiveSpawners();
            BeginToSpawn();
        }

        public void Exit()
        {
            
        }

        private void SetGameServicesToInitialState()
        {
            loadingCurtain.Show();
            timer.enabled = false;
            groundTeleporter.Disable();
            wallTeleporter.Disable();
            score.ResetScore();
        }

        private void BeginToSpawn()
        {
            for (int i = 0; i < allInitialSpawners.Count; i++)
            {
                allInitialSpawners[i].Spawn();
            }
        }
        private void SetUpPlayer()
        {
            player.transform.position = spawnPoint.transform.position;
            player.DisableComponents();
        }

        private void FillActiveSpawners()
        {
            activeInitialSpawners = new List<IInitialSpawner>();
            for (int i = 0; i < allInitialSpawners.Count; i++)
            {
                activeInitialSpawners.Add(allInitialSpawners[i]);
            }
        }
        private void GetSpawners(List<IInitialSpawner> allSpawners)
        {
            allInitialSpawners = allSpawners;
            UnsubscribeToSpawners();
            SubscribeToSpawners();
        }
        private void SubscribeToSpawners()
        {
            for (int i = 0; i < allInitialSpawners.Count; i++)
            {
                allInitialSpawners[i].OnFinishedSpawn += SpawnerFinishedSpawn;
            }
        }
        private void UnsubscribeToSpawners()
        {
            for (int i = 0; i < allInitialSpawners.Count; i++)
            {
                allInitialSpawners[i].OnFinishedSpawn -= SpawnerFinishedSpawn;
            }
        }
        private void SpawnerFinishedSpawn(IInitialSpawner spawner)
        {
            activeInitialSpawners.Remove(spawner);
            if (activeInitialSpawners.Count == 0)
                FinishedAllSpawns();
        }

        private void FinishedAllSpawns() => 
            gameLoopStateMachine.Enter<PlayingState>();
    }
}